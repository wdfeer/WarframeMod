using Terraria.Localization;

namespace WarframeMod.Content.Items.Weapons;

internal class MK1Bo : CircularMelee
{
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DEFENSE_PENETRATION);
    public const int DEFENSE_PENETRATION = 6;
    public override string Texture => "WarframeMod/Content/Items/Weapons/Bo";
    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.ArmorPenetration = 6;
        Item.damage = 11;
        Item.crit = 6;
        Item.knockBack = 5f;
        Item.width = 57;
        Item.height = 60;
        Item.scale = 1.75f;
        Item.useTime = 29;
        Item.useAnimation = 29;
        Item.rare = 1;
        Item.value = Item.buyPrice(silver: 25);
    }
    public override float SizeMult => 1.4f;
}