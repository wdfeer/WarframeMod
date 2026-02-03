using Terraria.DataStructures;
using WarframeMod.Content.Projectiles;
using WarframeMod.Common.GlobalProjectiles;
using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Weapons;

public class ArcaScisco : ModItem
{
    public const int MAX_STACKS = 4;
    public override void SetDefaults()
    {
        Item.UseSound = new Terraria.Audio.SoundStyle("WarframeMod/Content/Sounds/ArcaSciscoSound").ModifySoundStyle(pitchVariance: 0.08f);
        Item.damage = 30;
        Item.crit = 14;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 5;
        Item.width = 32;
        Item.height = 20;
        Item.useTime = 13;
        Item.useAnimation = 13;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 0;
        Item.value = Item.buyPrice(gold: 3);
        Item.rare = 3;
        Item.autoReuse = false;
        Item.shoot = ModContent.ProjectileType<ArcaSciscoProjectile>();
        Item.shootSpeed = 16f;
    }
    
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-1.2f, 0);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.SpaceGun, 1);
        recipe.AddIngredient(ItemID.Bone, 6);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
    int GetStacks(Player player) => player.GetModPlayer<ArcaSciscoPlayer>().stacks;
    public override void ModifyWeaponCrit(Player player, ref float crit)
    {
        crit += 5 * GetStacks(player);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        int stacks = GetStacks(player);
        var proj = this.ShootWith(player, source, position, velocity, type, damage, knockback, spawnOffset: Item.width + 1);
        var modProj = proj.ModProjectile as ArcaSciscoProjectile;
        modProj.onHit = () =>
        {
            player.AddBuff(ModContent.BuffType<ArcaSciscoBuff>(), 180);
            player.GetModPlayer<ArcaSciscoPlayer>().stacks++;
        };
        var buffProj = proj.GetGlobalProjectile<BuffGlobalProjectile>();
        buffProj.AddBleed(stacks * 5 + 13);

        return false;
    }
}
class ArcaSciscoPlayer : ModPlayer
{
    public int stacks;
    public int maxStacks = ArcaScisco.MAX_STACKS;
    public override void ResetEffects()
    {
        if (!Player.HasBuff<ArcaSciscoBuff>())
            stacks = 0;
        else if (stacks > maxStacks)
            stacks = maxStacks;
        maxStacks = ArcaScisco.MAX_STACKS;
    }
}