using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.Audio;
using WarframeMod.Global;

namespace WarframeMod.Items.Weapons
{
    internal class TiberonPrime : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Right Click to switch between Auto, Burst and Semi-auto fire modes\n+40%, 50% or 70% Critical damage in Auto, Burst and Semi\n75% Chance not to consume ammo in Auto");
        }
        FireMode mode = FireMode.Burst;
        public enum FireMode
        {
            Auto,
            Burst,
            Semi
        }
        public FireMode Mode
        {
            get => mode;
            set
            {
                if ((int)value > 2) value = 0;
                mode = value;
                UpdateStatsByFireMode();
            }
        }
        void UpdateStatsByFireMode()
        {
            switch (Mode)
            {
                case FireMode.Auto:
                    Item.crit = 12;
                    Item.useTime = 7;
                    Item.useAnimation = 7;
                    Item.autoReuse = true;
                    break;
                case FireMode.Burst:
                    Item.crit = 24;
                    Item.useTime = 4;
                    Item.useLimitPerAnimation = 3;
                    Item.useAnimation = 20;
                    Item.autoReuse = false;
                    break;
                default:
                    Item.crit = 26;
                    Item.useTime = 10;
                    Item.useAnimation = 10;
                    Item.autoReuse = false;
                    break;
            }
        }
        public override void SetDefaults()
        {
            Item.damage = 22;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.width = 39;
            Item.height = 9;
            Item.scale = 1.1f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 3;
            Item.value = Item.buyPrice(gold: 8);
            Item.rare = 5;
            Item.shoot = 10;
            Item.shootSpeed = 17f;
            Item.useAmmo = AmmoID.Bullet;
            UpdateStatsByFireMode();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HallowedBar, 16);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (Mode == 0 && Main.rand.Next(0, 100) < 75) return false;
            return base.CanConsumeAmmo(ammo, player);
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        double lastModeChange = 0;
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                if (Main.time - 30 > lastModeChange)
                {
                    Mode++;
                    lastModeChange = Main.time;
                    SoundEngine.PlaySound(SoundID.Unlock);
                }
                return false;
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            SoundEngine.PlaySound(SoundID.Item11, position);

            WeaponCommon.ModifyProjectileSpawnPosition(ref position, velocity, Item.width);
            Projectile projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
            projectile.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier = Mode == FireMode.Auto ? 1.4f : (Mode == FireMode.Burst ? 1.5f : 1.7f);
            return false;
        }
    }
}