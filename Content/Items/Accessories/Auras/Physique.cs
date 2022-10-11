using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories.Auras;
public class Physique : ModItem
{
    public const float EXTRA_LIFE = 0.1f;
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
    public int Count => Player.GetModPlayer<AuraPlayer>().CountAurasInMyTeam(x => x.physique);
    public override void PostUpdateEquips()
    {
        Player.statLifeMax2 += (int)(Player.statLifeMax * Physique.EXTRA_LIFE * Count);
    }
}