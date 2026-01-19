using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Localization;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Weapons;

public class Simulor : ModItem
{
    private readonly List<Projectile> activeProjectiles = new();

    public const int MERGE_DAMAGE_INCREASE_PERCENT = 20;
    public const int MERGE_DAMAGE_INCREASE_MAX_PERCENT = 300;

    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MERGE_DAMAGE_INCREASE_PERCENT, MERGE_DAMAGE_INCREASE_MAX_PERCENT);

    // public override void SetStaticDefaults() // TODO: put this in localization file
    // {
    //     Tooltip.SetDefault(
    //         "Launches orbs that can't hit enemies directly and bounce off tiles\n" +
    //         "Orbs attract and merge, creating an implosion that increases damage\n" +
    //         "Damage increases by 20% per merge up to 300%\n" +
    //         "Implosions electrify enemies\n" +
    //         "Right-click to force all active orbs to explode"
    //     );
    // }

    public override void SetDefaults()
    {
        Item.damage = 42;
        Item.crit = 8;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 10;
        Item.width = 36;
        Item.height = 15;
        Item.useTime = 24;
        Item.useAnimation = 24;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 6.9f;
        Item.value = Item.buyPrice(gold: 33);
        Item.rare = ItemRarityID.LightRed;
        Item.autoReuse = false;
        Item.shoot = ModContent.ProjectileType<SimulorProjectile>();
        Item.UseSound = new SoundStyle("WarframeMod/Content/Sounds/SynoidSimulorSound")
        {
            PitchVariance = 0.08f
        };;
        Item.shootSpeed = 16f;
    }

    public override bool AltFunctionUse(Player player) => true;

    public override bool CanUseItem(Player player)
    {
        if (player.altFunctionUse == 1)
        {
            for (int i = activeProjectiles.Count - 1; i >= 0; i--)
            {
                Projectile proj = activeProjectiles[i];

                if (proj.active && proj.ModProjectile is SimulorProjectile simulor)
                {
                    simulor.Explode();
                }
            }

            activeProjectiles.Clear();
            return false;
        }

        return base.CanUseItem(player);
    }

    public override bool Shoot(
        Player player,
        EntitySource_ItemUse_WithAmmo source,
        Vector2 position,
        Vector2 velocity,
        int type,
        int damage,
        float knockback)
    {
        WeaponCommon.ModifyProjectileSpawnPosition(
            ref position,
            velocity,
            Item.width * 0.75f
        );

        int projID = Projectile.NewProjectile(
            source,
            position,
            velocity,
            type,
            damage,
            knockback,
            player.whoAmI
        );

        activeProjectiles.Add(Main.projectile[projID]);

        return false;
    }
}

