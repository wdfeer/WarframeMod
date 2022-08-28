namespace WarframeMod.Content.Items.Accessories;
public class CorrosiveProjection : ModItem
{
    public const float IGNORE_DEFENSE = 0.18f;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"Weapons ignore {(int)(IGNORE_DEFENSE * 100)}% of enemy's Defense");
    }
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 32;
        Item.height = 32;
        Item.rare = 3;
        Item.value = Item.buyPrice(silver: 60);
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.JungleSpores, 6);
        recipe.AddIngredient(ItemID.HellstoneBar, 4);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual) 
        => player.GetModPlayer<CorrosiveProjectionPlayer>().enabled = true;
}
class CorrosiveProjectionPlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects() => enabled = false;
    void ModifyDamage(NPC target, ref int damage)
    {
        if (!enabled)
            return;
        damage += target.checkArmorPenetration((int)(target.defense * CorrosiveProjection.IGNORE_DEFENSE));
    }
    public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        => ModifyDamage(target, ref damage);
    public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        => ModifyDamage(target, ref damage);
}