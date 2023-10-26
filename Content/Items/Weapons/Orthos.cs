using WarframeMod.Common.GlobalItems;

namespace WarframeMod.Content.Items.Weapons;

internal class Orthos : ModItem
{
    protected virtual float BaseBleedChance => 0.15f;
    public override void SetDefaults()
    {
        Item.damage = 13;
        Item.crit = 2;
        Item.knockBack = 3.5f;
        Item.DamageType = DamageClass.Melee;
        Item.width = 47;
        Item.height = 48;
        Item.scale = 2.5f;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.UseSound = SoundID.Item1;
        Item.useTime = 24;
        Item.useAnimation = 24;
        Item.autoReuse = true;
        Item.rare = 1;
        Item.value = Item.sellPrice(silver: 50);
        Item.GetGlobalItem<CritGlobalItem>().critMultiplier = 0.75f;
        Item.GetGlobalItem<BleedingGlobalItem>().bleedingChance = BaseBleedChance;
    }
    public override float UseSpeedMultiplier(Player player)
    {
        return SuperSwing ? 0.8f : 1f;
    }
    public override void UseStyle(Player player, Rectangle heldItemFrame)
    {
        float rot = player.itemRotation + MathHelper.PiOver4;
        if (player.direction == 1)
            rot += MathHelper.PiOver2;
        Vector2 rotationDirection = rot.ToRotationVector2();
        player.itemLocation += rotationDirection * player.itemWidth * 2f;
    }
    public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
    {
        int radius = player.itemWidth * 2;
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
            Item.GetGlobalItem<BleedingGlobalItem>().bleedingChance = BaseBleedChance;
        return base.CanUseItem(player);
    }
    public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
    {
        if (SuperSwing)
        {
            modifiers.SourceDamage *= 2;
            modifiers.Knockback *= 2;
        }
    }
    public override void ModifyHitPvp(Player player, Player target, ref Player.HurtModifiers modifiers)
    {
        if (SuperSwing)
            modifiers.SourceDamage *= 2;
    }
    public override void AddRecipes()
        => CreateRecipe().AddRecipeGroup(RecipeGroupID.IronBar, 20)
                         .AddTile(TileID.Anvils)
                         .Register();
}