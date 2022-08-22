namespace WarframeMod.Content.Items.Accessories;

public class Vaporize : ModItem
{
    public const int DAMAGE = 100;
    public const float KNOCKBACK = 4.5f;
    public const int COOLDOWN = 480;
    public const int MAX_ATTACK_DISTANCE = 750;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"All minions will deal {DAMAGE} damage to nearby enemies every {COOLDOWN / 60} seconds");
    }
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 44;
        Item.height = 64;
        Item.rare = 5;
        Item.value = Item.sellPrice(gold: 2);
    }
    int timer = COOLDOWN;
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        timer++;
        if (timer >= COOLDOWN && player.numMinions > 0)
        {
            NPC[] targets = FindTargets(player);
            if (targets.Length > 0)
            {
                Fire(player, targets);
                timer = 0;
            }
        }
    }
    NPC[] FindTargets(Player player)
    {
        return Main.npc.Where(npc => npc.active && !npc.friendly && npc.CanBeChasedBy() && npc.Center.Distance(player.Center) < MAX_ATTACK_DISTANCE).ToArray();
    }
    void Fire(Player player, IList<NPC> targets)
    {
        IEnumerable<Projectile> minions = Main.projectile.Where(proj => proj.active && proj.friendly && proj.owner == player.whoAmI && proj.minionSlots > 0);
        foreach (Projectile projectile in minions)
        {
            NPC target = targets[Main.rand.Next(0,targets.Count)];
            Vector2 from = projectile.Center;
            Vector2 to = target.Center;
            Vector2 velocity = Vector2.Normalize(to - from) * 16f;
            Projectile shot = Projectile.NewProjectileDirect(Item.GetSource_FromThis(), from, velocity, ProjectileID.DiamondBolt, DAMAGE, KNOCKBACK, player.whoAmI);
            shot.tileCollide = false;
            shot.penetrate = -1;
            shot.usesLocalNPCImmunity = true;
            shot.localNPCHitCooldown = -1;
            shot.extraUpdates = 6;
            shot.DamageType = DamageClass.Summon;
        }
    }
}
