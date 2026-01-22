namespace WarframeMod.Content.Items.Accessories;

public abstract class UmbralAccessory : ModItem
{
    public static readonly int[] PERCENT_DAMAGE_REDUCTION_NOBOSS = [8, 10, 12];
    public static readonly float[] TOTAL_DAMAGE_REDUCTION_NOBOSS = [0.08f, 0.2f, 0.36f];
    int umbraPower = 0;
    public abstract string UniqueTooltipDefault { get; }
    public abstract string GetCurrentUniqueTooltipValue(int umbraCount);
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var t0 = tooltips.Find(t => t.Name == "Tooltip0");
        if (t0 == null)
            return;
        t0.Text = GetCurrentUniqueTooltipValue(umbraPower);
        var t1 = tooltips.Find(t => t.Name == "Tooltip1");
        if (t1 != null)
            t1.Text = $"+{PERCENT_DAMAGE_REDUCTION_NOBOSS[umbraPower]}% damage reduction while no boss is alive";
    }
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.rare = 5;
        Item.width = 32;
        Item.height = 32;
        Item.value = Item.buyPrice(gold: 2);
    }
    public override void UpdateInventory(Player player)
    {
        umbraPower = 0;
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<UmbralAccessoryPlayer>().umbraCount++;
        umbraPower = GetUmbralAccessoryPower(player);
        UpdateUmbralAccessory(player, umbraPower);
    }
    public abstract void UpdateUmbralAccessory(Player player, int umbraCount);
    protected int GetUmbralAccessoryPower(Player player)
    {
        int oldUmbraCount = player.GetModPlayer<UmbralAccessoryPlayer>().oldUmbraCount;
        if (oldUmbraCount > 0)
            oldUmbraCount--;
        return oldUmbraCount;
    }
    public abstract int NonUmbralItemType { get; }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(NonUmbralItemType);
        recipe.AddIngredient(ItemID.SoulofNight, 15);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
    public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
    {
        if (equippedItem.type == NonUmbralItemType || incomingItem.type == NonUmbralItemType)
            return false;
        return base.CanAccessoryBeEquippedWith(equippedItem, incomingItem, player);
    }
}
class UmbralAccessoryPlayer : ModPlayer
{
    public ushort oldUmbraCount = 0;
    public ushort umbraCount = 0;
    public override void ResetEffects()
    {
        oldUmbraCount = umbraCount;
        umbraCount = 0;
    }
    public override void ModifyHurt(ref Player.HurtModifiers modifiers)
    {
        if (umbraCount <= 0 || WarframeMod.IsBossAlive())
            return;
        float damageMult = 1f - UmbralAccessory.TOTAL_DAMAGE_REDUCTION_NOBOSS[umbraCount - 1];
        modifiers.SourceDamage *= damageMult;
    }
}