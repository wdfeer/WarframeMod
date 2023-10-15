using WarframeMod.Common.GlobalNPCs;

namespace WarframeMod.Content.Items.Accessories;
public class InfectedClip : ModItem
{
    public const float POISON_CHANCE = 0.25f;
    public const int POISON_DURATION = 360;
    public const int EXTRA_POISON_DPS = 10;
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.rare = 1;
        Item.value = Item.sellPrice(silver: 50);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<InfectedClipPlayer>().enabled = true;
    }
}
class InfectedClipPlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects()
        => enabled = false;
    void OnHitNPCWithSomething(NPC target)
    {
        if (!enabled)
            return;
        if (Main.rand.NextFloat() < InfectedClip.POISON_CHANCE)
        {
            target.AddBuff(BuffID.Poisoned, InfectedClip.POISON_DURATION);
            target.GetGlobalNPC<DebuffDamageGlobalNPC>().AddBuffDamage(DebuffDamageGlobalNPC.SourceId.InfectedClip, BuffID.Poisoned, InfectedClip.EXTRA_POISON_DPS);
        }
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