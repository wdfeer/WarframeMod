using WarframeMod.Common.GlobalNPCs;

namespace WarframeMod.Content.Items.Accessories;
public class Hellfire : ModItem
{
    public const int EXTRA_FIRE_DPS = 12;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"+{EXTRA_FIRE_DPS} damage per second on burning enemies");
    }
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.rare = 1;
        Item.value = Item.sellPrice(silver: 12);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<HellfirePlayer>().enabled = true;
    }
    public override void AddRecipes()
        => CreateRecipe().AddIngredient(ItemID.Gel, 50)
                         .AddRecipeGroup(RecipeGroupID.IronBar, 4)
                         .AddTile(TileID.Anvils)
                         .Register();
}
class HellfirePlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects()
        => enabled = false;
    void OnHitNPCWithSomething(NPC target)
    {
        if (!enabled)
            return;
        target.GetGlobalNPC<DebuffDamageGlobalNPC>().AddBuffDamage(DebuffDamageGlobalNPC.SourceId.Hellfire, BuffID.OnFire, Hellfire.EXTRA_FIRE_DPS);
    }
    public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
    {
        OnHitNPCWithSomething(target);
    }
    public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
    {
        OnHitNPCWithSomething(target);
    }
}