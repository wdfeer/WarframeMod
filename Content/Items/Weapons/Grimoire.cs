using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.Localization;
using Terraria.ModLoader.IO;
using WarframeMod.Common;
using WarframeMod.Content.Items.Consumables;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Weapons;

public class Grimoire : ModItem
{
    public const int ELECTRO_CHANCE = 20;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ELECTRO_CHANCE);
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        string text = Mod.GetLocalization("Items.Grimoire.FullUpgraded").Value;
        if (!upgrades.Contains(GrimoireUpgradeType.JahuCanticle))
            text = Mod.GetLocalization("Items.Grimoire.JahuNext").Value;
        else if (!upgrades.Contains(GrimoireUpgradeType.VomeInvocation))
            text = Mod.GetLocalization("Items.Grimoire.VomeNext").Value;
        else if (!upgrades.Contains(GrimoireUpgradeType.LohkCanticle))
            text = Mod.GetLocalization("Items.Grimoire.LohkNext").Value;
        else if (!upgrades.Contains(GrimoireUpgradeType.XataInvocation))
            text = Mod.GetLocalization("Items.Grimoire.XataNext").Value;
        else if (!upgrades.Contains(GrimoireUpgradeType.RisInvocation))
            text = Mod.GetLocalization("Items.Grimoire.RisNext").Value;
        
        TooltipHelper.InsertTooltipLine(Mod, tooltips, text);
    }

    public List<GrimoireUpgradeType> upgrades = [];
    public bool HasUpgrade(GrimoireUpgradeType type) => upgrades.Contains(type);

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

    public override void UpdateInventory(Player player)
    {
        Item.rare = 3 + upgrades.Count;
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

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type,
        ref int damage,
        ref float knockback)
    {
        if (player.altFunctionUse == 2)
        {
            SoundEngine.PlaySound(SoundID.Item117, position);
            type = ModContent.ProjectileType<GrimoireAltProjectile>();
            knockback += 5f;
            velocity /= 3;
        }
    }

    public override void LoadData(TagCompound tag)
    {
        foreach (GrimoireUpgradeType type in GrimoireUpgradeType.GetValuesAsUnderlyingType(typeof(GrimoireUpgradeType)))
        {
            string id = type.ToString();
            if (tag.ContainsKey(id) && !upgrades.Contains((GrimoireUpgradeType)type))
            {
                upgrades.Add((GrimoireUpgradeType)type);
            }
        }
    }

    public override void SaveData(TagCompound tag)
    {
        foreach (var id in upgrades)
        {
            tag[id.ToString()] = true;
        }
    }

    public static Grimoire GetPlayerGrimoire(Player player)
    {
        var item = player.inventory.FirstOrDefault(item => item.type == ModContent.ItemType<Grimoire>(), null);
        if (item != null)
        {
            return item.ModItem as Grimoire;
        }

        return null;
    }
}

public class GrimoireDropCondition : IItemDropRuleCondition
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

public class GrimoireUpgradeDropCondition : IItemDropRuleCondition
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