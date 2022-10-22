namespace WarframeMod.Content.Items.Arcanes;
public abstract class Arcane : ModItem
{
    public static readonly Func<int>[] arcaneTypeGetters = new Func<int>[]
    {
        () => ModContent.ItemType<ArcaneAvenger>(),
        () => ModContent.ItemType<ArcaneGuardian>(),
        () => ModContent.ItemType<VirtuosStrike>(),
        () => ModContent.ItemType<ArcaneStrike>(),
        () => ModContent.ItemType<ArcanePrecision>(),
        () => ModContent.ItemType<ArcaneFury>(),
        () => ModContent.ItemType<ArcaneBodyguard>(),
        () => ModContent.ItemType<ArcaneArachne>(),
        () => ModContent.ItemType<ArcaneVictory>(),
        () => ModContent.ItemType<MoltAugmented>(),
        () => ModContent.ItemType<EternalOnslaught>(),
        () => ModContent.ItemType<ArcaneConsequence>(),
        () => ModContent.ItemType<ArcaneGrace>(),
        () => ModContent.ItemType<EmergenceSavior>(),
        () => ModContent.ItemType<ArcanePistoleer>()
    };
    public static int[] GetArcaneTypes()
        => arcaneTypeGetters.Select(x => x()).ToArray();
    public static int GetArcaneIndex(int type)
        => Array.IndexOf(GetArcaneTypes(), type);
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.rare = -12;
        Item.expert = true;
        Item.value = Item.sellPrice(gold: 4);
    }
    public abstract void UpdateArcane(Player player);
    public override void UpdateAccessory(Player player, bool hideVisual)
        => UpdateArcane(player);
    public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
    {
        return equippedItem.ModItem is not Arcane || incomingItem.ModItem is not Arcane;
    }
}
