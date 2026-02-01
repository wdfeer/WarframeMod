using WarframeMod.Content.Projectiles;


namespace WarframeMod.Content.Items.Weapons;

internal class WolfSledge : CircularMelee
{
    public override void SetDefaults()
    {
        Item.damage = 130;
        Item.crit = 13;
        Item.knockBack = 12f;
        Item.DamageType = DamageClass.Melee;
        Item.width = 56;
        Item.height = 56;
        Item.scale = 2.5f;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.UseSound = SoundID.Item1;
        Item.useTime = 60;
        Item.useAnimation = 60;
        Item.autoReuse = true;
        Item.rare = ItemRarityID.Yellow;
        Item.value = Item.sellPrice(gold: 16);
        Item.shootSpeed = 20f;
        Item.shoot = ModContent.ProjectileType<WolfSledgeProjectile>();
    }

    public override void AddRecipes()
        => CreateRecipe()
            .AddIngredient(ItemID.ChlorophyteWarhammer)
            .AddIngredient(ItemID.TurtleShell)
            .AddIngredient(ItemID.BrokenHeroSword)
            .AddTile(TileID.MythrilAnvil)
            .Register();

    public override float SizeMult => 2f;
    
    public override float UseSpeedMultiplier(Player player)
    {
        return player.GetAttackSpeed(Item.DamageType);
    }
}