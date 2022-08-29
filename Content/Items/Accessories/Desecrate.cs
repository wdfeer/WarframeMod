namespace WarframeMod.Content.Items.Accessories;

public class Desecrate : ModItem
{
    public const int LIFE_DRAIN = 7;
    public const float MAX_DISTANCE = 800;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"Whenever an enemy dies nearby, consume {LIFE_DRAIN} life and double the loot");
    }
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 32;
        Item.height = 32;
        Item.rare = 5;
        Item.value = Item.sellPrice(gold: 9);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<DesecratePlayer>().desecrate = true;
    }
}
internal class DesecratePlayer : ModPlayer
{
    public bool desecrate = false;
    public override void ResetEffects() => desecrate = false;
}
internal class DesecrateNPC : GlobalNPC
{
    public override bool InstancePerEntity => true;
    bool desecrated = false;
    public override void OnKill(NPC npc)
    {
        if (!desecrated && !npc.boss)
        {
            Player player = NearbyPlayerWithDesecrate(npc);
            if (player == null)
                return;

            desecrated = true;
            npc.NPCLoot();
            LifeDrainEffect(player);
        }
    }
    private Player NearbyPlayerWithDesecrate(NPC npc)
    {
        return Array.Find(Main.player, player =>
                                    player.active
                                    && player.GetModPlayer<DesecratePlayer>().desecrate
                                    && player.position.Distance(npc.position) < Desecrate.MAX_DISTANCE
        );
    }
    private void LifeDrainEffect(Player player)
    {
        Terraria.DataStructures.PlayerDeathReason reason = new Terraria.DataStructures.PlayerDeathReason() { SourceCustomReason = player.name + " was desecrated" };
        int oldDef = player.statDefense;
        player.statDefense = 0;
        player.Hurt(reason, Desecrate.LIFE_DRAIN, 0, cooldownCounter: -2);
        player.statDefense = oldDef;
        player.netLife = true;
    }
}
