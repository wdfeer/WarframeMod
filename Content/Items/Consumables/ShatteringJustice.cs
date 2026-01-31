using Terraria.Localization;
using WarframeMod.Content.Buffs;
using WarframeMod.Content.Items.Weapons;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Consumables;

public class ShatteringJustice : ModItem
{
    public const int BASE_DAMAGE_INCREASE = 10;
    public const int EFFECT_EXPLOSION_RANGE_TILES = 40;
    public const int EFFECT_EXPLOSION_DAMAGE = 100;
    public const int EFFECT_HEAL = 50;
    public const int EFFECT_DEFENSE_INCREASE = 10;
    public const int EFFECT_DEFENSE_INCREASE_DURATION_SECONDS = 30;

    public override LocalizedText Tooltip =>
        base.Tooltip.WithFormatArgs(BASE_DAMAGE_INCREASE,
            EFFECT_EXPLOSION_RANGE_TILES,
            EFFECT_EXPLOSION_DAMAGE,
            EFFECT_HEAL,
            EFFECT_DEFENSE_INCREASE,
            EFFECT_DEFENSE_INCREASE_DURATION_SECONDS);

    public override void SetDefaults()
    {
        Item.width = 44;
        Item.height = 64;
        Item.consumable = true;
        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.UseSound = SoundID.Item4;
    }

    public override bool? UseItem(Player player)
    {
        foreach (Item item in player.inventory)
        {
            if (item.ModItem is Sobek { shatteringJustice: false } sobek)
            {
                sobek.shatteringJustice = true;
                return true;
            }
        }

        return false;
    }

    public static void ProcJustice(Player player, Sobek sobek)
    {
        player.Heal(EFFECT_HEAL);
        player.AddBuff(ModContent.BuffType<JusticeBuff>(), EFFECT_DEFENSE_INCREASE_DURATION_SECONDS * 60);

        foreach (NPC enemy in Main.npc.Where(it =>
                     it.active && !it.friendly && it.Distance(player.position) < EFFECT_EXPLOSION_RANGE_TILES * 16 &&
                     it.CanBeChasedBy()))
        {
            Projectile proj = Projectile.NewProjectileDirect(player.GetSource_ItemUse(sobek.Item), enemy.Center, Vector2.Zero,
                ModContent.ProjectileType<JusticeExplosion>(), EFFECT_EXPLOSION_DAMAGE, 1f, owner: player.whoAmI);
            proj.DamageType = DamageClass.Ranged;
        }
    }
}

class ShatteringJusticeGlobalProjectile : GlobalProjectile
{
    public override bool InstancePerEntity => true;
    public bool active;
    public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
    {
        target.GetGlobalNPC<ShatteringJusticeGlobalNPC>().sobekPlayer = projectile.owner;
    }
}

class ShatteringJusticeGlobalNPC : GlobalNPC
{
    public override bool InstancePerEntity => true;
    public int sobekPlayer = -1;
    public override void OnKill(NPC npc)
    {
        if (sobekPlayer == -1) return;

        var player = Main.player[sobekPlayer];
        if (!player.active || player.dead) return;

        Item item = player.inventory.FirstOrDefault(it => it.type == ModContent.ItemType<Sobek>());
        if (item != null && item.ModItem is Sobek sobek && sobek.shatteringJustice)
        {
            sobek.justiceCharge++;
            if (sobek.justiceCharge > 9 && !player.HasBuff<JusticeBuff>())
            {
                sobek.justiceCharge = 0;
                ShatteringJustice.ProcJustice(player, sobek);
            }
        }
    }
}