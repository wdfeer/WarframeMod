using Terraria.DataStructures;
using Terraria.Localization;
using WarframeMod.Content.Projectiles;
using WarframeMod.Common.GlobalProjectiles;
using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Weapons;

public class TenetArcaScisco : ModItem
{
    public const int ELECTRO_CHANCE = 20;
    public const int MAX_STACKS = 6;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ELECTRO_CHANCE);
    public override void SetDefaults()
    {
        Item.UseSound = new Terraria.Audio.SoundStyle("WarframeMod/Content/Sounds/ArcaSciscoSound").ModifySoundStyle(pitchVariance: 0.08f);
        Item.damage = 70;
        Item.crit = 18;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 10;
        Item.width = 32;
        Item.height = 20;
        Item.useTime = 13;
        Item.useAnimation = 13;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 1.6f;
        Item.value = Item.buyPrice(gold: 4);
        Item.rare = 9;
        Item.autoReuse = false;
        Item.shoot = ModContent.ProjectileType<TenetArcaSciscoProjectile>();
        Item.shootSpeed = 16f;
    }
    
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-1.2f, 0);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<ArcaScisco>();
        recipe.AddIngredient<Fieldron>();
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }

    public override void HoldItem(Player player)
    {
        player.GetModPlayer<ArcaSciscoPlayer>().maxStacks = MAX_STACKS;
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
        var modProj = proj.ModProjectile as TenetArcaSciscoProjectile;
        modProj.onHit = () =>
        {
            player.AddBuff(ModContent.BuffType<ArcaSciscoBuff>(), 180);
            player.GetModPlayer<ArcaSciscoPlayer>().stacks++;
        };
        var buffProj = proj.GetGlobalProjectile<BuffGlobalProjectile>();
        buffProj.AddElectro(ELECTRO_CHANCE);
        buffProj.AddBleed(stacks * 5);

        return false;
    }
}