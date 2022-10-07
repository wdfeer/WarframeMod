using Terraria.DataStructures;
using WarframeMod.Common;
using WarframeMod.Common.GlobalNPCs;
using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;

public class SerratedRounds : ModItem
{
    public const float BLEED_CONVERSION = 0.4f;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"{(int)(BLEED_CONVERSION * 100f)}% of dealt damage is converted into bleeding");
    }
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.rare = 7;
        Item.value = Item.buyPrice(gold: 7);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<SerratedPlayer>().enabled = true;
    }
}
class SerratedPlayer : CritPlayerHooks
{
    public bool enabled;
    public override void ResetEffects()
        => enabled = false;
    void ConvertDamage(NPC target, ref int damagePreCrit, int critLvl, float critMult)
    {
        if (!enabled)
            return;
        int damagePostCrit = CritPlayer.GetPostCritDamage(damagePreCrit, critLvl, critMult);
        float bleedDmg = damagePostCrit * SerratedRounds.BLEED_CONVERSION;
        damagePreCrit -= (int)(damagePreCrit * SerratedRounds.BLEED_CONVERSION);
        BleedingBuff.Create(bleedDmg, target);
    }
    public override void ModifyHitNPCPreCrit(Item item, NPC target, ref int damage, ref float knockback, ref bool crit, ref float critMult, ref int critLvl)
    {
        ConvertDamage(target, ref damage, critLvl, critMult);
    }
    public override void ModifyHitNPCWithProjPreCrit(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref float critMult, ref int critLvl, ref int hitDirection)
    {
        ConvertDamage(target, ref damage, critLvl, critMult);
    }
}