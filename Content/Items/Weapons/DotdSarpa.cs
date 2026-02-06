namespace WarframeMod.Content.Items.Weapons;
public class DotdSarpa : Sarpa
{
    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.damage = 40;
        Item.width = 38;
        Item.height = 15;
        Item.rare = ItemRarityID.Lime;
        Item.shoot = ProjectileID.IchorBullet;
        Item.value = Item.sellPrice(gold: 9);
    }
    
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-1, 2);
    }
    
    public override void AddRecipes()
    { }
}