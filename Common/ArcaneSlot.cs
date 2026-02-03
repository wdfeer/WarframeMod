using WarframeMod.Common.Configs;
using WarframeMod.Content.Items.Arcanes;

namespace WarframeMod.Common;

public class ArcaneSlot : ModAccessorySlot
{
    public override bool IsEnabled()
        => Main.expertMode && ModContent.GetInstance<WarframeServerConfig>().enableArcaneSlot;
    public override bool CanAcceptItem(Item checkItem, AccessorySlotType context)
    {
        return checkItem.ModItem is Arcane;
    }
    public override bool ModifyDefaultSwapSlot(Item item, int accSlotToSwapTo)
    {
        return item.ModItem is Arcane;
    }
    public override bool DrawVanitySlot => false;
    public override bool DrawDyeSlot => false;
    public override string FunctionalTexture => "WarframeMod/Assets/ArcaneSlot";
    public override void OnMouseHover(AccessorySlotType context)
    {
        switch (context)
        {
            case AccessorySlotType.FunctionalSlot:
                Main.hoverItemName = "Arcane";
                break;
        }
    }
}
