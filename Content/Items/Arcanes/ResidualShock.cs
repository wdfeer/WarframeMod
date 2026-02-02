using Terraria.DataStructures;
using Terraria.Localization;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Arcanes;

public class ResidualShock : Arcane
{
    public const int BASE_DAMAGE = 45;
    public const int RANGE = 50 * 16;
    public const int DURATION = 10 * 60;

    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RANGE / 16, DURATION / 60);

    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.damage = BASE_DAMAGE;
        Item.DamageType = DamageClass.Summon;
    }

    public override void UpdateArcane(Player player)
    {
        player.GetModPlayer<ResidualShockPlayer>().arcane = Item;
    }
}

class ResidualShockPlayer : ModPlayer
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
            var globalNPC = target.GetGlobalNPC<ResidualShockGlobalNPC>();
            globalNPC.marked = true;
            globalNPC.source = arcane.GetSource_FromThis();
            globalNPC.damage = Player.GetWeaponDamage(arcane);
            globalNPC.player = Player.whoAmI;
        }
    }
}

class ResidualShockGlobalNPC : GlobalNPC
{
    public override bool InstancePerEntity => true;

    public bool marked;
    public int player;
    public IEntitySource source;
    public int damage;

    public override bool PreKill(NPC npc)
    {
        if (marked && !Main.projectile.Any(it =>
                it.active && it.type == ModContent.ProjectileType<ResidualShockSpawner>() && it.owner == player))
        {
            Projectile.NewProjectileDirect(source, npc.Center, Vector2.Zero,
                ModContent.ProjectileType<ResidualShockSpawner>(), damage, 0f, owner: player);
        }

        return false;
    }
}