using WarframeMod.Common;

namespace WarframeMod.Content.Items.Accessories;

public class AmarContempt : AmarAccessory
{
    private const int MELEE_DAMAGE_INCREASE_PERCENT = 12;
    private const int TRUE_MELEE_BLEED_CHANCE = 20;
    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.rare = ItemRarityID.LightRed;
        Item.value = Item.sellPrice(gold: 2);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        base.UpdateAccessory(player, hideVisual);
        player.GetDamage(DamageClass.Melee) += MELEE_DAMAGE_INCREASE_PERCENT / 100f;
        player.GetModPlayer<AmarContemptPlayer>().trueMeleeBleedChance += TRUE_MELEE_BLEED_CHANCE;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.WarriorEmblem);
        recipe.AddIngredient(ItemID.HallowedBar, 6);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
}

class AmarContemptPlayer : ModPlayer
{
    public int trueMeleeBleedChance = 0;
    override public void ResetEffects()
    {
        trueMeleeBleedChance = 0;
    }

    public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (Random.Shared.Next(0, 100) <= trueMeleeBleedChance)
        {
            BleedingBuff.Create(damageDone, target);
        }
    }
}