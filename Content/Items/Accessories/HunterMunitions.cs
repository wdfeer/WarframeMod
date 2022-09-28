using WarframeMod.Common.GlobalNPCs;
using WarframeMod.Common.Players;
using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Accessories;

public class HunterMunitions : HunterAccessory
{
    public const int bleedChance = 30;
    public override string DefaultTooltip => $"{bleedChance}% bleeding chance on critical hits";
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
        if (Main.rand.NextFloat() < HunterMunitions.bleedChance / 100f)
        {
            BleedingBuff.CreateBleed(damageAfterCrit, target);
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
