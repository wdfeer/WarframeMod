namespace WarframeMod.Common.GlobalNPCs;

/// <summary>
/// Allows creating on-kill effects easily
/// </summary>
public class OnKillGlobalNPC : GlobalNPC
{
    /// <summary>
    /// Registers the action to happen if a kill under certain conditions happens
    /// Call in SetStaticDefaults or similar
    /// </summary>
    public static void RegisterOnKillEvent(Predicate<KillInfo> predicate, Action<KillInfo> action)
    {
        events.Add(hit =>
        {
            if (predicate(hit)) action(hit);
        });
    }

    public static List<Action<KillInfo>> events = [];

    public struct KillInfo
    {
        public NPC target;
        public Player player;
        public int itemType = -1;
        public int projectileType = -1;

        public KillInfo(NPC npc, Projectile proj, Player player)
        {
            target = npc;
            this.player = player;
            projectileType = proj.type;
        }

        public KillInfo(NPC npc, Player player, Item trueMeleeWeapon)
        {
            target = npc;
            this.player = player;
            itemType = trueMeleeWeapon.type;
        }
    }

    public KillInfo kill;

    public override bool InstancePerEntity => true;

    public override void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
    {
        kill = new KillInfo(npc, player, item);
    }

    public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
    {
        var player = projectile.whoAmI;
        if (Main.player[player] is { active: true } pl)
            kill = new KillInfo(npc, projectile, pl);
        // No NPC -> NPC kill support
    }

    public override bool PreKill(NPC npc)
    {
        foreach (var action in events)
        {
            action(kill);
        }

        return base.PreKill(npc);
    }
}