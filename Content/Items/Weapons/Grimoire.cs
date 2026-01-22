using Terraria.GameContent.ItemDropRules;
using Terraria.Localization;
using Terraria.ModLoader.IO;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Weapons;
public class Grimoire : ModItem
{
    public const int ELECTRO_CHANCE = 20;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ELECTRO_CHANCE);
    public bool vomeInvocationActive;
    public override void SetDefaults()
    {
        Item.damage = 28;
        Item.crit = 16;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 6;
        Item.width = 31;
        Item.height = 31;
        Item.useTime = 45;
        Item.useAnimation = 45;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 2f;
        Item.value = Item.sellPrice(gold: 3);
        Item.rare = 3;
        Item.shoot = ModContent.ProjectileType<GrimoireProjectile>();
        Item.shootSpeed = 16f;
    }

    public override bool AltFunctionUse(Player player)
        => true;
    public override void ModifyManaCost(Player player, ref float reduce, ref float mult)
    {
        if (player.altFunctionUse == 2) mult = 3f;
    }
    public override float UseSpeedMultiplier(Player player)
    {
        if (player.altFunctionUse == 2)
            return 0.5f;

        return 1f;
    }
    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage,
        ref float knockback)
    {
        if (player.altFunctionUse == 2)
        {
            type = ModContent.ProjectileType<GrimoireAltProjectile>();
            knockback += 5f;
            velocity /= 3;
        }
    }

    public override void LoadData(TagCompound tag)
    {
        if (tag.ContainsKey("vomeInvocationActive"))
        {
            vomeInvocationActive = tag.GetBool("vomeInvocationActive");
        }
    }
    public override void SaveData(TagCompound tag)
    {
        tag["vomeInvocationActive"] = vomeInvocationActive;
    }
    
    public static Grimoire GetPlayerGrimoire(Player player)
        => player.inventory.First(item => item.type == ModContent.ItemType<Grimoire>()).ModItem as Grimoire;
}
class GrimoireDropCondition : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return info.player.statManaMax2 > 200;
    }
    public bool CanShowItemDropInUI()
        => false;
    public string GetConditionDescription()
        => "More mana required.";
}
class GrimoireUpgradeDropCondition : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return info.player.HeldItem.type == ModContent.ItemType<Grimoire>();
    }
    public bool CanShowItemDropInUI()
        => false;
    public string GetConditionDescription()
        => "Requires holding a Grimoire.";
}
