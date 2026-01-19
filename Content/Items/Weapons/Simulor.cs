using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Localization;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Weapons;

public class Simulor : ModItem
{
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
        Item.shootSpeed = 16f;
    }

    public override bool AltFunctionUse(Player player) => true;
    private readonly List<int> activeProjectileIDs = [];

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
                {
                    simulor.TryExplode();
                }
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

            SoundEngine.PlaySound(new SoundStyle("WarframeMod/Content/Sounds/SynoidSimulorSound")
            {
                PitchVariance = 0.08f
            }, position);

            return false;
        }
    }
}

