using WarframeMod.Common;
using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;

public class HunterMunitions : HunterAccessory
{
    public const int BLEED_CHANCE_PERCENT = 30;
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 44;
        Item.height = 64;
        Item.rare = 2;
        Item.value = Item.sellPrice(silver: 75);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        base.UpdateAccessory(player, hideVisual);
        player.GetModPlayer<HunterMunitionsPlayer>().enabled = true;
    }
}
internal class HunterMunitionsPlayer : CritPlayerHooks
{
    public bool enabled = false;
    public override void ResetEffects()
    {
        enabled = false;
    }
    public void TryBleed(NPC target, int damageAfterCrit)
    {
        if (Main.rand.NextFloat() < HunterMunitions.BLEED_CHANCE_PERCENT / 100f)
        {
            BleedingBuff.Create(damageAfterCrit, target);
        }
    }
    public override void OnHitNPCPostCrit(Item item, NPC target, int damage, float knockback, bool crit, float critMult, int critLvl, int damagePostCrit)
    {
        if (enabled && crit)
            TryBleed(target, damagePostCrit);
    }
    public override void OnHitNPCWithProjPostCrit(Projectile proj, NPC target, int damage, float knockback, bool crit, float critMult, int critLvl, int damagePostCrit)
    {
        if (enabled && crit)
            TryBleed(target, damagePostCrit);
    }
}
