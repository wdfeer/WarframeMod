using WarframeMod.Content.Buffs;
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
        Item.useTime = 45;
        Item.useAnimation = 45;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.UseSound = SoundID.Item4;
    }
    public override bool? UseItem(Player player)
    {
        var grimoire = Grimoire.GetPlayerGrimoire(player);
        if (grimoire.vomeInvocationActive)
            return false;

        grimoire.vomeInvocationActive = true;
        return true;
    }
}

class VomeInvocationPlayer : ModPlayer
{
    public int stacks;
    public override void ResetEffects()
    {
        if (!Player.HasBuff<VomeInvocationBuff>())
            stacks = 0;
        else if (stacks > 15)
            stacks = 15;
    }
    public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
    {
        if (item.ModItem is Grimoire grimoire && grimoire.vomeInvocationActive)
        {
            damage.Base += 20;
            damage += 0.04f * stacks;
        }
    }
}