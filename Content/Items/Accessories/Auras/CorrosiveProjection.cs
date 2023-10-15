using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories.Auras;
public class CorrosiveProjection : ModItem
{
    public const float IGNORE_DEFENSE = 0.18f;
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 32;
        Item.height = 32;
        Item.rare = 2;
        Item.value = Item.sellPrice(silver: 45);
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.JungleSpores, 9);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AuraPlayer>().myAuras.corrosiveProjection = true;
    }
}
class CorrosiveProjectionPlayer : ModPlayer
{
    public bool Enabled => Player.GetModPlayer<AuraPlayer>().AnyPlayerInMyTeam(x => x.corrosiveProjection);
    void ModifyDamage(NPC target, ref int damage)
    {
        if (Enabled)
            damage += target.checkArmorPenetration((int)(target.defense * CorrosiveProjection.IGNORE_DEFENSE));
    }
    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        => ModifyDamage(target, ref damage);
    public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        => ModifyDamage(target, ref damage);
}