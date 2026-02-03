using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Localization;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Items.Weapons;

public class AkjagaraPrime : ModItem
{
    private const int BASE_BLEED_CHANCE = 30;
    private int bleedChance = BASE_BLEED_CHANCE;
    private const int CRIT_DAMAGE_INCREASE = 10;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(BASE_BLEED_CHANCE, $"+{CRIT_DAMAGE_INCREASE}%");
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        UpdateBleedChance(Main.LocalPlayer);
        var t1 = tooltips.Find(t => t.Name == "Tooltip1");
        if (t1 != null)
        {
            string[] splits = t1.Text.Split("%");
            t1.Text = bleedChance + "%" + String.Concat(splits.Skip(1));
        }
    }

    public override void SetDefaults()
    {
        Item.damage = 57;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 32;
        Item.height = 24;
        Item.useTime = 24;
        Item.useAnimation = 24;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 2.2f;
        Item.value = Item.sellPrice(gold: 44);
        Item.rare = 6;
        Item.autoReuse = false;
        Item.shoot = 10;
        Item.shootSpeed = 16f;
        Item.useAmmo = AmmoID.Bullet;
        Item.UseSound = new SoundStyle("WarframeMod/Content/Sounds/AkjagaraPrimeSound").ModifySoundStyle(pitchVariance: 0.1f);
    }

    public override void AddRecipes()
        => CreateRecipe().AddIngredient(ItemID.HallowedBar, 12).AddIngredient(ItemID.DungeonDesertKey).AddTile(TileID.MythrilAnvil)
            .Register();


    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
        int type, int damage, float knockback)
    {
        SoundEngine.PlaySound(SoundID.Item11.WithVolumeScale(0.9f), position);
        
        UpdateBleedChance(player);

        for (int i = 0; i < 2; i++)
        {
            if (i == 1) velocity *= 0.95f;
            
            var proj = this.ShootWith(player, source, position, velocity, type, damage, knockback, 0.004f, Item.width);
            proj.GetGlobalProjectile<BuffGlobalProjectile>().AddBleed(bleedChance);
            proj.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier += CRIT_DAMAGE_INCREASE / 100f;
            proj.usesLocalNPCImmunity = true;
            proj.localNPCHitCooldown = -1;
        }
        
        return false;
    }

    private void UpdateBleedChance(Player player)
    {
        var rangedClass = player.GetTotalDamage<RangedDamageClass>();
        bleedChance = (int)(BASE_BLEED_CHANCE * rangedClass.Additive * rangedClass.Multiplicative);
    }
}