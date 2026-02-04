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

    private void UpdateDamage()
    {
        Item.damage = BASE_DAMAGE;
        if (NPC.downedPlantBoss)
            Item.damage += BASE_DAMAGE / 5;
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
        if (arcane != null && target.CanBeChasedBy())
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

        return base.PreKill(npc);
    }
}