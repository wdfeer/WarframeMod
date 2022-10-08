using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;
public class Physique : ModItem
{
    public const float EXTRA_LIFE = 1f;//0.1f; DEBUG
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"+{(int)(EXTRA_LIFE * 100)}% max life to players on your team");
    }
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 32;
        Item.height = 32;
        Item.rare = 1;
        Item.value = Item.sellPrice(silver: 35);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AuraPlayer>().myAuras.physique = true;
    }
}
class PhysiquePlayer : ModPlayer
{
    public bool Enabled => Player.GetModPlayer<AuraPlayer>().AnyPlayerInMyTeam(x => x.physique);
    public override void PostUpdateEquips()
    {
        if (Enabled)
            Player.statLifeMax2 += (int)(Player.statLifeMax * Physique.EXTRA_LIFE);
    }
}