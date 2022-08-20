namespace WarframeMod.Content.Items.Accessories;

public class Aviator : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("+20% Damage Resistance while airborne");
    }

    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.rare = ItemRarityID.Orange;
        Item.value = Item.buyPrice(gold: 1);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.WormScarf, 1);
        recipe.AddIngredient(ItemID.CloudinaBottle, 1);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AviatorPlayer>().enabled = true;
    }
}
class AviatorPlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects()
        => enabled = false;
    void ModifyHit(ref int damage)
    {
        if (!enabled)
            return;
        Player.UpdateTouchingTiles();
        bool touchingTiles = Player.TouchedTiles.Any();
        if (!touchingTiles)
            damage = (int)(damage * 0.8f);
    }

    public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        => ModifyHit(ref damage);
    public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        => ModifyHit(ref damage);
}