using Terraria.DataStructures;

namespace WarframeMod.Content.Items.Accessories;

public class SplitChamber : ModItem
{
    public const float MULTISHOT = 0.166f;
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 44;
        Item.height = 64;
        Item.rare = 5;
        Item.value = Item.sellPrice(gold: 5);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<MultishotPlayer>().extraMultishot += MULTISHOT;
    }
}
class MultishotPlayer : ModPlayer
{
    public float extraMultishot = 0;
    public override void ResetEffects()
        => extraMultishot = 0;
    int GetExtraProjectileCount()
    {
        float extra = extraMultishot;
        int count = 0;
        while (extra > 0)
        {
            if (Main.rand.NextFloat() < extra)
                count++;
            extra--;
        }
        return count;
    }
    bool ValidItemDamageType(Item item) => item.DamageType == DamageClass.Magic
                                                            || item.DamageType == DamageClass.Ranged
                                                            || item.DamageType == DamageClass.Throwing;
    int GetProjectileType(DamageClass damageClass)
    {
        if (damageClass == DamageClass.Magic)
            return ProjectileID.DiamondBolt;
        if (damageClass == DamageClass.Ranged)
            return ProjectileID.ExplosiveBullet;
        if (damageClass == DamageClass.Throwing)
            return ProjectileID.BoneDagger;
        throw new ArgumentException("damageClass has to be either Magic, Ranged or Throwing");
    }
    public override bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (!ValidItemDamageType(item) || item.channel)
            return true;
        int projCount = GetExtraProjectileCount();
        for (int i = 0; i < projCount; i++)
        {
            int projType = GetProjectileType(item.DamageType);
            Projectile proj = Projectile.NewProjectileDirect(item.GetSource_FromThis(), position, velocity.RotatedByRandom(0.03), projType, damage, knockback, Player.whoAmI);
            proj.CritChance = (int)Player.GetTotalCritChance(proj.DamageType) + item.crit;
            proj.noDropItem = true;
            proj.DamageType = item.DamageType;
            proj.usesLocalNPCImmunity = true;
            proj.localNPCHitCooldown = -1;
        }
        return true;
    }
    public override void ModifyShootStats(Item item, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        if (item.channel)
            damage += (int)(damage * extraMultishot);
    }
}
