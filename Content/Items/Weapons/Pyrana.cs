using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Items.Weapons;

public class Pyrana : ModItem
{
    public const int MULTISHOT = 5;
    public const int FALLOFF_START = 10;
    public const int FALLOFF_MAX = 30;
    public const float MAX_FALLOFF_DAMAGE_DECREASE = 0.7f;
    public override void SetDefaults()
    {
        Item.damage = 8;
        Item.crit = 16;
        Item.noMelee = true;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 32;
        Item.height = 16;
        Item.useTime = 15;
        Item.useAnimation = 15;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.knockBack = 1.2f;
        Item.value = Item.sellPrice(gold: 17);
        Item.rare = 3;
        Item.shootSpeed = 13;
        Item.autoReuse = true;
        Item.shoot = 10;
        Item.useAmmo = AmmoID.Bullet;
        Item.UseSound = new Terraria.Audio.SoundStyle("WarframeMod/Content/Sounds/PyranaPrimeSound").ModifySoundStyle(pitchVariance: 0.1f);
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        this.ModifyAmmoDamage(player, ref damage, 0.5f);
        for (int i = 0; i < MULTISHOT; i++)
        {
            Projectile proj = this.ShootWith(player, source, position, velocity, type, damage, knockback,
                MathF.PI * 20 / 180, Item.width);
            proj.GetGlobalProjectile<FalloffGlobalProjectile>()
                .SetFalloff(position, FALLOFF_START, FALLOFF_MAX, MAX_FALLOFF_DAMAGE_DECREASE);
        }

        return false;
    }
}

public class BeatQueenBeeCondition : IItemDropRuleCondition
{
    public string GetConditionDescription()
        => "Requires defeating Queen Bee.";

    public bool CanDrop(DropAttemptInfo info)
        => NPC.downedQueenBee;

    public bool CanShowItemDropInUI()
        => NPC.downedQueenBee;
}