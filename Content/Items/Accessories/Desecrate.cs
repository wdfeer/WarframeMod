using Terraria.Localization;

namespace WarframeMod.Content.Items.Accessories;

public class Desecrate : ModItem
{
    public const int LIFE_DRAIN = 7;
    public const float MAX_DISTANCE = 800;

    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 32;
        Item.height = 32;
        Item.rare = 5;
        Item.value = Item.sellPrice(gold: 18);
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<DesecratePlayer>().enabled = true;
    }
}

internal class DesecratePlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects() => enabled = false;
}

internal class DesecrateGlobalNPC : GlobalNPC
{
    public override bool InstancePerEntity => true;
    bool desecrated;

    public override bool PreKill(NPC npc)
    {
        if (CanBeDesecrated(npc))
        {
            Player player = NearbyPlayerWithDesecrate(npc);
            if (player != null)
            {
                desecrated = true;
                npc.NPCLoot();
                LifeDrainEffect(player);
            }
        }

        return base.PreKill(npc);
    }

    public bool CanBeDesecrated(NPC npc)
        => !desecrated && !npc.boss && npc.CanBeChasedBy();

    private Player NearbyPlayerWithDesecrate(NPC npc)
    {
        return Array.Find(Main.player, player =>
            player.active
            && player.GetModPlayer<DesecratePlayer>().enabled
            && player.position.Distance(npc.position) < Desecrate.MAX_DISTANCE
        );
    }

    private void LifeDrainEffect(Player player)
    {
        Terraria.DataStructures.PlayerDeathReason reason = new()
            { CustomReason = NetworkText.From(player.name + " was desecrated") };
        player.Hurt(reason, Desecrate.LIFE_DRAIN, 0, dodgeable: false, scalingArmorPenetration: 1f,
            cooldownCounter: 2);
        player.netLife = true;
    }
}