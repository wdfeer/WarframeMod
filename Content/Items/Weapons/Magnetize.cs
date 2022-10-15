using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Weapons;

public class Magnetize : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Creates a magnetizing sphere around the enemy\nAll friendly projectiles are accelerated towards the center of the sphere");
    }
    public override void SetDefaults()
    {
        Item.mana = 32;
        Item.noUseGraphic = true;
        Item.useTime = 45;
        Item.useAnimation = 45;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.value = Item.buyPrice(gold: 16);
        Item.rare = 5;
        Item.UseSound = SoundID.Item43;
        Item.autoReuse = false;
    }
    public override bool? UseItem(Player player)
    {
        NPC target = FindTarget(Main.MouseWorld);
        if (target == null)
            return false;
        Projectile projectile = Projectile.NewProjectileDirect(Item.GetSource_ItemUse(Item), target.Center, Vector2.Zero, ModContent.ProjectileType<MagnetizeProjectile>(), 0, 0, player.whoAmI);
        MagnetizeProjectile modProj = projectile.ModProjectile as MagnetizeProjectile;
        modProj.Target = target;
        return true;
    }
    private NPC FindTarget(Vector2 mousePosition)
    {
        NPC potentialTarget = Main.npc.MinBy(x => x.Center.Distance(mousePosition));
        if (!Main.projectile.Any(p => p.active
                                      && p.ModProjectile is MagnetizeProjectile
                                      && (p.ModProjectile as MagnetizeProjectile).Target == potentialTarget)
                                      && potentialTarget.CanBeChasedBy()
                                      && potentialTarget.Center.Distance(mousePosition) < 80f)
            return potentialTarget;
        return null;
    }
}