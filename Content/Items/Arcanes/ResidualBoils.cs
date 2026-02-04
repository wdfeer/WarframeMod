using Terraria.DataStructures;
using Terraria.Localization;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Arcanes;

public class ResidualBoils : Arcane
{
    public const int BASE_DAMAGE = 30;
    public const int AOE_RADIUS = 15 * 16;
    public const int DURATION = 10 * 60;

    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AOE_RADIUS / 16, DURATION / 60);

    private void UpdateDamage()
    {
        Item.damage = BASE_DAMAGE;
        if (NPC.downedPlantBoss)
            Item.damage += BASE_DAMAGE / 4;
        if (NPC.downedGolemBoss)
            Item.damage += BASE_DAMAGE / 3;
        if (NPC.downedMoonlord)
            Item.damage += BASE_DAMAGE;
    }

    public override void SetDefaults()
    {
        base.SetDefaults();
        UpdateDamage();
        Item.DamageType = DamageClass.Summon;
    }

    // to update the damage if the item is in a chest etc
    public override void ModifyTooltips(List<TooltipLine> tooltips)
        => UpdateDamage();

    public override void UpdateInventory(Player player)
        => UpdateDamage();

    public override void UpdateArcane(Player player)
    {
        UpdateDamage();
        player.GetModPlayer<ResidualBoilsPlayer>().arcane = Item;
    }
}

class ResidualBoilsPlayer : ModPlayer
{
    public Item arcane;

    public override void ResetEffects()
    {
        arcane = null;
    }

    public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
    {
        if (arcane != null)
        {
            var globalNPC = target.GetGlobalNPC<ResidualBoilsGlobalNPC>();
            globalNPC.marked = true;
            globalNPC.source = arcane.GetSource_FromThis();
            globalNPC.damage = Player.GetWeaponDamage(arcane);
            globalNPC.player = Player.whoAmI;
        }
    }
}

class ResidualBoilsGlobalNPC : GlobalNPC
{
    public override bool InstancePerEntity => true;

    public bool marked;
    public int player;
    public IEntitySource source;
    public int damage;

    public override bool PreKill(NPC npc)
    {
        if (marked && !Main.projectile.Any(it =>
                it.active && it.type == ModContent.ProjectileType<ResidualBoilsProjectile>() && it.owner == player))
        {
            Projectile.NewProjectileDirect(source, npc.Center, Vector2.Zero,
                ModContent.ProjectileType<ResidualBoilsProjectile>(), damage, 0f, owner: player);
        }

        return base.PreKill(npc);
    }
}