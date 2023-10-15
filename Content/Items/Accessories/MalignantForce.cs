using WarframeMod.Common.GlobalNPCs;

namespace WarframeMod.Content.Items.Accessories;
public class MalignantForce : ModItem
{
    public const int POISON_DURATION = 600;
    public const int EXTRA_POISON_DPS = 25;
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.rare = 4;
        Item.value = Item.sellPrice(gold: 1);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<MalignantForcePlayer>().enabled = true;
    }
    public override void AddRecipes()
        => CreateRecipe().AddIngredient(ModContent.ItemType<InfectedClip>())
            .AddIngredient(ItemID.SoulofNight, 10)
            .AddTile(TileID.Anvils)
            .Register();
}
class MalignantForcePlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects()
        => enabled = false;
    void OnHitNPCWithSomething(NPC target)
    {
        if (!enabled)
            return;
        target.AddBuff(BuffID.Poisoned, MalignantForce.POISON_DURATION);
        target.GetGlobalNPC<DebuffDamageGlobalNPC>().AddBuffDamage(DebuffDamageGlobalNPC.SourceId.MalignantForce, BuffID.Poisoned, MalignantForce.EXTRA_POISON_DPS);
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