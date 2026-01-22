using Terraria.Localization;

namespace WarframeMod.Content.Items.Accessories;

public class EnergyConversion : ModItem
{
    public const int NEXT_ATTACK_DAMAGE_INCREASE_PERCENT = 50;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(NEXT_ATTACK_DAMAGE_INCREASE_PERCENT);
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 44;
        Item.height = 64;
        Item.rare = ItemRarityID.Pink;
        Item.value = Item.sellPrice(gold: 3);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        EnergyConversionPlayer modPlayer = player.GetModPlayer<EnergyConversionPlayer>();
        modPlayer.active = true;
        if (modPlayer.buffed)
        {
            player.GetDamage(DamageClass.Magic) += NEXT_ATTACK_DAMAGE_INCREASE_PERCENT / 100f;
        }
    }
}

class EnergyConversionPlayer : ModPlayer
{
    public bool active;
    public bool buffed;
    public override void ResetEffects()
    {
        if (!active)
            buffed = false;
        active = false;
    }

    public readonly int[] starTypes = [ItemID.Star, ItemID.SoulCake, ItemID.SugarPlum];
    public override bool OnPickup(Item item)
    {
        if (active && starTypes.Contains(item.type))
        {
            buffed = true;
        }
        return base.OnPickup(item);
    }

    public override void OnHitAnything(float x, float y, Entity victim)
    {
        buffed = false;
        base.OnHitAnything(x, y, victim);
    }
}
