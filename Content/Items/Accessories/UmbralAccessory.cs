using Terraria.DataStructures;

namespace WarframeMod.Content.Items.Accessories;

public abstract class UmbralAccessory : ModItem
{
    public static readonly int[] PERCENT_DAMAGE_REDUCTION_WHILE_NOBOSS = new int[] { 8, 10, 12 };
    int umbraPower = 0;
    public abstract string UniqueTooltipDefault { get; }
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault(UniqueTooltipDefault + "\n+PERCENT_DAMAGE_REDUCTION_WHILE_NOBOSS% damage reduction while no boss is alive\nEnhance accessories in this set");
    }
    public abstract string GetCurrentUniqueTooltipValue(int umbraCount);
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        tooltips.Find(t => t.Name == "Tooltip0").Text = GetCurrentUniqueTooltipValue(umbraPower);
        tooltips.Find(t => t.Name == "Tooltip1").Text = $"+{PERCENT_DAMAGE_REDUCTION_WHILE_NOBOSS[umbraPower]}% damage reduction while no boss is alive";
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
    public int oldUmbraCount = 0;
    public int umbraCount = 0;
    public override void ResetEffects()
    {
        oldUmbraCount = umbraCount;
        umbraCount = 0;
    }
    public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource, ref int cooldownCounter)
    {
        if (umbraCount <= 0)
            return true;
        float damageMult = 1;
        damageMult -= UmbralAccessory.PERCENT_DAMAGE_REDUCTION_WHILE_NOBOSS[umbraCount--] * umbraCount / 100f;
        damage = (int)(damage * damageMult);
        return true;
    }
}