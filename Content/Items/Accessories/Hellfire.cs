using WarframeMod.Common.GlobalNPCs;

namespace WarframeMod.Content.Items.Accessories;
public class Hellfire : ModItem
{
    public const int EXTRA_FIRE_DPS = 15;
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.rare = 1;
        Item.value = Item.sellPrice(silver: 15);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<HellfirePlayer>().enabled = true;
    }
    public override void AddRecipes()
        => CreateRecipe().AddRecipeGroup(RecipeGroupID.IronBar, 3)
                         .AddIngredient(ItemID.Gel, 50)
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
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        OnHitNPCWithSomething(target);
    }
    public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
    {
        OnHitNPCWithSomething(target);
    }
}