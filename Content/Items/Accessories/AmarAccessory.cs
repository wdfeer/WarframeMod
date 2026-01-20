namespace WarframeMod.Content.Items.Accessories;

public abstract class AmarAccessory : ModItem
{
    public const int TELEPORT_RANGE = 8 * 16;
    public const int ENEMY_DESIRED_DISTANCE = 2 * 16;
    public const float MAX_ENEMY_ANGLE_FROM_CURSOR = 15 * (MathF.PI / 180f);
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
        if (item.DamageType != DamageClass.Melee || player.GetModPlayer<AmarPlayer>().teleportRange <= 0) return null;

        float maxRange = player.GetModPlayer<AmarPlayer>().teleportRange;

        Vector2 lookDirection = Vector2.Normalize(Main.MouseWorld - player.Center);

        float minDotProduct = MathF.Cos(AmarAccessory.MAX_ENEMY_ANGLE_FROM_CURSOR);

        var candidates = Main.npc
            .Where(npc => npc.active && !npc.friendly)
            .Select(npc =>
            {
                var dist = npc.Hitbox.Distance(player.Center);
                var diff = npc.Center - player.Center;
                if (diff == Vector2.Zero)
                    return (npc, dot: -1f, dist);

                var dir = Vector2.Normalize(diff);
                var dot = Vector2.Dot(lookDirection, dir);
                return (npc, dot, dist);
            })
            .Where(e =>
                e.dot >= minDotProduct &&
                e.dist > AmarAccessory.ENEMY_DESIRED_DISTANCE &&
                e.dist < maxRange
            );

        var best = candidates
            .OrderByDescending(e => e.dot / e.dist)
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
