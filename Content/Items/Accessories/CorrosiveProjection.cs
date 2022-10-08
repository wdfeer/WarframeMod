namespace WarframeMod.Content.Items.Accessories;
public class CorrosiveProjection : ModItem
{
    public const float IGNORE_DEFENSE = 0.18f;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"Weapons of players on your team ignore {(int)(IGNORE_DEFENSE * 100)}% of enemy's Defense");
    }
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 32;
        Item.height = 32;
        Item.rare = 2;
        Item.value = Item.sellPrice(silver: 45);
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.JungleSpores, 9);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        if (!CanUpdate())
            return;
        player.GetModPlayer<CorrosiveProjectionPlayer>().enabled = true;
        if (Main.netMode != NetmodeID.SinglePlayer)
            (Mod as WarframeMod).SendCorrosiveProjectionPacket((byte)player.team);
    }
    public static bool CanUpdate() => (int)Main.time % 100 == 0;
}
class CorrosiveProjectionPlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects()
    {
        if (CorrosiveProjection.CanUpdate())
            enabled = false;
    }
    void ModifyDamage(NPC target, ref int damage)
    {
        if (!enabled)
            return;
        damage += target.checkArmorPenetration((int)(target.defense * CorrosiveProjection.IGNORE_DEFENSE));
    }
    public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        => ModifyDamage(target, ref damage);
    public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        => ModifyDamage(target, ref damage);
}