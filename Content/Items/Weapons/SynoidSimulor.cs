using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Localization;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Weapons;

public class SynoidSimulor : ModItem
{
    public const int MERGE_DAMAGE_INCREASE_PERCENT = 40;
    public const int MERGE_DAMAGE_INCREASE_MAX_PERCENT = 400;

    public override void SetDefaults()
    {
        Item.damage = 77;
        Item.crit = 10;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 15;
        Item.width = 31;
        Item.height = 15;
        Item.useTime = 24;
        Item.useAnimation = 24;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 6.9f;
        Item.value = Item.sellPrice(gold: 10);
        Item.rare = ItemRarityID.LightPurple;
        Item.autoReuse = false;
        Item.shoot = ModContent.ProjectileType<SimulorProjectile>();
        Item.shootSpeed = 16f;
    }

    public override bool AltFunctionUse(Player player) => activeProjectileIDs.Count > 0;
    private readonly List<int> activeProjectileIDs = [];

    public override void ModifyManaCost(Player player, ref float reduce, ref float mult)
    {
        if (player.altFunctionUse == 2 && activeProjectileIDs.Count > 0)
        {
            mult = 0;
        }
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
        if (player.altFunctionUse == 2)
        {
            for (int i = 0; i < activeProjectileIDs.Count; i++)
            {
                int id = activeProjectileIDs[i];
                Projectile proj = Main.projectile[id];

                if (proj.active && proj.ModProjectile is SimulorProjectile simulor)
                    simulor.TryExplode((int)(simulor.ExplosionWidth * simulor.DamageMult));
            }

            activeProjectileIDs.Clear();

            return false;
        }
        else
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
            activeProjectileIDs.Add(projID);
            (Main.projectile[projID].ModProjectile as SimulorProjectile).explosionWidth += 50;

            SoundEngine.PlaySound(new SoundStyle("WarframeMod/Content/Sounds/SynoidSimulorSound")
            {
                PitchVariance = 0.08f
            }, position);

            return false;
        }
    }
}