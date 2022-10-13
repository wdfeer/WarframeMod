using System;
using Terraria.DataStructures;
using WarframeMod.Common.GlobalItems;

namespace WarframeMod.Content.Items.Weapons;

internal class Cassowar : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"Every third swing has doubled damage and guaranteed Bleeding\n20% Bleeding chance\n-30% Critical Damage\nDoubled benefit from Attack Speed");
    }
    public override void SetDefaults()
    {
        Item.damage = 20;
        Item.crit = 2;
        Item.knockBack = 4.5f;
        Item.DamageType = DamageClass.Melee;
        Item.width = 60;
        Item.height = 60;
        Item.scale = 2.15f;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.UseSound = SoundID.Item1;
        Item.useTime = 45;
        Item.useAnimation = 45;
        Item.autoReuse = true;
        Item.rare = 2;
        Item.value = Item.sellPrice(gold: 1);
        Item.GetGlobalItem<CritGlobalItem>().critMultiplier = 0.7f;
    }
    public override float UseSpeedMultiplier(Player player)
    {
        return player.GetAttackSpeed(Item.DamageType) * (SuperSwing ? 0.75f : 1f);
    }
    static float HoldingPointMult => 1.6f;
    public override void UseStyle(Player player, Rectangle heldItemFrame)
    {
        float rot = player.itemRotation + MathHelper.PiOver4;
        if (player.direction == 1)
            rot += MathHelper.PiOver2;
        Vector2 rotationDirection = rot.ToRotationVector2();
        player.itemLocation += rotationDirection * player.itemWidth * HoldingPointMult;
    }
    public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
    {
        int radius = (int)(player.itemWidth * HoldingPointMult);
        Point pos = (player.Center - new Vector2(radius, radius)).ToPoint();
        hitbox = new Rectangle(pos.X, pos.Y, radius * 2, radius * 2);
        hitbox.X += player.direction;
    }
    uint swings = 0;
    bool SuperSwing => swings % 3 == 0;
    public override bool CanUseItem(Player player)
    {
        swings++;
        if (SuperSwing)
            Item.GetGlobalItem<BleedingGlobalItem>().bleedingChance = 1f;
        else
            Item.GetGlobalItem<BleedingGlobalItem>().bleedingChance = 0.2f;
        return base.CanUseItem(player);
    }
    public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
    {
        if (SuperSwing)
        {
            damage *= 2;
            knockBack *= 1.75f;
        }
    }
    public override void ModifyHitPvp(Player player, Player target, ref int damage, ref bool crit)
    {
        if (SuperSwing)
            damage *= 2;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.DemoniteBar, 10);
        recipe.AddIngredient(ItemID.ShadowScale, 5);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();

        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.CrimtaneBar, 10);
        recipe.AddIngredient(ItemID.TissueSample, 5);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}