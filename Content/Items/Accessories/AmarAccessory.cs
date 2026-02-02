using Terraria.Localization;

namespace WarframeMod.Content.Items.Accessories;

public abstract class AmarAccessory : ModItem
{
    const int TELEPORT_RANGE_TILES = 10;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(TELEPORT_RANGE_TILES);
    public const int TELEPORT_RANGE = TELEPORT_RANGE_TILES * 16;
    public const int ENEMY_DESIRED_DISTANCE = 2 * 16;
    public const float MAX_ENEMY_DISTANCE_FROM_CURSOR = 5 * 16;
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 44;
        Item.height = 64;
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AmarPlayer>().teleportRange += TELEPORT_RANGE;
    }
}

class AmarPlayer : ModPlayer
{
    public int teleportRange = 0;
    public override void ResetEffects()
    {
        teleportRange = 0;
    }
}

class AmarGlobalItem : GlobalItem
{
    public override bool? UseItem(Item item, Player player)
    {
        if (player.whoAmI != Main.myPlayer) return null;
        if (player.itemTime > 0) return null;
        if (!item.DamageType.CountsAsClass(DamageClass.Melee) || player.GetModPlayer<AmarPlayer>().teleportRange <= 0) return null;

        float maxRange = player.GetModPlayer<AmarPlayer>().teleportRange;

        var candidates = Main.npc
            .Where(npc => npc.active && !npc.friendly)
            .Select(npc =>
            {
                var dist = npc.Hitbox.Distance(player.Center);
                var diff = npc.Center - player.Center;
                if (diff == Vector2.Zero) return (npc, dist: 9999, distToCursor: 9999);
                
                var distToCursor = Main.MouseWorld.Distance(npc.Center);

                return (npc, dist, distToCursor);
            })
            .Where(e =>
                e.dist > AmarAccessory.ENEMY_DESIRED_DISTANCE &&
                e.dist < maxRange &&
                e.distToCursor < AmarAccessory.MAX_ENEMY_DISTANCE_FROM_CURSOR
            );

        var best = candidates
            .OrderByDescending(e => e.distToCursor)
            .FirstOrDefault();

        if (best.npc == null) return null;

        NPC target = best.npc;
        var tpDistance = Math.Min(maxRange,
            target.Hitbox.Distance(player.Center) - AmarAccessory.ENEMY_DESIRED_DISTANCE);
        var direction = Vector2.Normalize(target.Center - player.Center);
        var tpPos = player.position + direction * tpDistance;
        player.Teleport(tpPos, TeleportationStyleID.RodOfDiscord);
        return null;
    }
}
