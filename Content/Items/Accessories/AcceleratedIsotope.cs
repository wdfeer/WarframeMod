using WarframeMod.Common;
using WarframeMod.Common.Players;
using Terraria.Localization;


namespace WarframeMod.Content.Items.Accessories;

public class AcceleratedIsotope : ModItem
{
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs($"+{DAMAGE_INCREASE}%", CONFUSED_CHANCE, USE_SPEED_INCREASE);
    public const int DAMAGE_INCREASE = 7;
    public const int CONFUSED_CHANCE = 20;
    public const int USE_SPEED_INCREASE = 7;
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
        player.GetDamage(DamageClass.Generic) += DAMAGE_INCREASE / 100f;
        player.GetAttackSpeed(DamageClass.Generic) += USE_SPEED_INCREASE / 100f;

        BuffPlayer buffman = player.GetModPlayer<BuffPlayer>();
        buffman.buffsOnHitNPC.Add(new BuffChance(BuffID.Confused, 300, CONFUSED_CHANCE / 100f));
    }
}
