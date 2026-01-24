using WarframeMod.Common.GlobalNPCs;

namespace WarframeMod.Content.Items.Accessories;

public class HealingReturn : ModItem
{
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 44;
        Item.height = 64;
        Item.rare = 5;
        Item.value = Item.sellPrice(gold: 4);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        var modPlayer = player.GetModPlayer<HealingReturnPlayer>();
        modPlayer.active = true;
        modPlayer.lastHealTimer++;
    }
}

class HealingReturnPlayer : ModPlayer
{
    public bool active;
    public int lastHealTimer;
    public override void ResetEffects()
    {
        active = false;
    }
    public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (active && !item.noMelee && item.DamageType == DamageClass.Melee && lastHealTimer >= 60)
        {
            int buffs = target.buffTime.Count(time => time > 0);
            buffs += target.GetGlobalNPC<StackableDebuffNPC>().bleeds.Count == 0 ? 0 : 1;
            buffs += target.GetGlobalNPC<StackableDebuffNPC>().electricity.Count == 0 ? 0 : 1;
            if (buffs > 0)
            {
                Player.Heal(buffs);
                lastHealTimer = 0;
            }
        }
    }
}
