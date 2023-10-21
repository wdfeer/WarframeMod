using WarframeMod.Common;
using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;

public class SerratedRounds : ModItem
{
    public const float BLEED_CONVERSION = 0.4f;
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
    void ConvertDamage(NPC target, int baseDamage, ref NPC.HitModifiers modifiersPreCrit, int critLvl, float critMult)
    {
        if (!enabled)
            return;
        int damagePostCrit = (int)(CritPlayer.GetTotalCritMult(critLvl, critMult) * baseDamage);
        float bleedDmg = damagePostCrit * SerratedRounds.BLEED_CONVERSION;
        modifiersPreCrit.SourceDamage *= 1 - SerratedRounds.BLEED_CONVERSION;
        BleedingBuff.Create(bleedDmg, target);
    }
    public override void ModifyHitNPCPreCrit(Item item, NPC target, ref NPC.HitModifiers modifiers, ref bool crit, ref float critMult, ref int critLvl)
    {
        ConvertDamage(target, item.damage, ref modifiers, critLvl, critMult);
    }
    public override void ModifyHitNPCWithProjPreCrit(Projectile proj, NPC target, ref NPC.HitModifiers modifiers, ref bool crit, ref float critMult, ref int critLvl)
    {
        ConvertDamage(target, proj.damage, ref modifiers, critLvl, critMult);
    }
}