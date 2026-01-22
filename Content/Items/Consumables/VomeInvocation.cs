using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Items.Consumables;

public class VomeInvocation : ModItem
{
    public override void SetDefaults()
    {
        Item.shootSpeed = 16f;
        Item.width = 44;
        Item.height = 64;
        Item.rare = 4;
        Item.value = Item.sellPrice(gold: 2);
        Item.consumable = true;
    }

    public override bool? UseItem(Player player)
    {
        if (Grimoire.vomeInvocationActive)
            return false;

        Grimoire.vomeInvocationActive = true;
        return true;
    }
}