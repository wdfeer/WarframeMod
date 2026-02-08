namespace WarframeMod.Content.Items.Weapons;

public class MK1Kunai : Kunai
{
    public override string Texture => "WarframeMod/Content/Items/Weapons/Kunai";

    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.damage = 10;
        Item.crit = -2;
        Item.rare = 1;
        Item.value = Item.buyPrice(silver: 50);
        Item.shootSpeed = 19f;
    }

    public override void AddRecipes()
    {
    }
}