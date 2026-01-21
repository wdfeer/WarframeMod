using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.Localization;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Items.Weapons;

internal class Dread : ModItem
{
    public const int BLEED_CHANCE = 20;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(BLEED_CHANCE);
    public override void SetDefaults()
    {
        Item.damage = 80;
        Item.crit = 46;
        Item.knockBack = 2;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.width = 16;
        Item.height = 50;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.UseSound = SoundID.Item5;
        Item.useTime = 60;
        Item.useAnimation = 60;
        Item.rare = 5;
        Item.value = Item.sellPrice(gold: 3);
        Item.shoot = ProjectileID.WoodenArrowFriendly;
        Item.shootSpeed = 16f;
        Item.useAmmo = AmmoID.Arrow;
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-1, 0);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        WeaponCommon.ModifyProjectileSpawnPosition(ref position, velocity, Item.width);
        int projectileID = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
        Projectile projectile = Main.projectile[projectileID];
        projectile.usesLocalNPCImmunity = true;
        projectile.localNPCHitCooldown = -1;
        projectile.GetGlobalProjectile<BuffGlobalProjectile>().AddBleed(BLEED_CHANCE);
        (Mod as WarframeMod).SetProjectileExtraUpdatesNetSafe(projectileID, projectile.extraUpdates + 2);
        return false;
    }
}

class DreadDropCondition : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info)
    {
        int[] necroArmor = [ItemID.NecroHelmet, ItemID.NecroBreastplate, ItemID.NecroGreaves];
        int[] playerArmor = info.player.armor.Select(item => item.type).ToArray();
        return necroArmor.All(type => playerArmor.Contains(type));
    }
    public bool CanShowItemDropInUI()
        => false;
    public string GetConditionDescription()
        => "Necro armor required.";
}
