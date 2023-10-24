
namespace WarframeMod.Common.GlobalItems;

public class AmmoGlobalItem : GlobalItem
{
    public override void SetDefaults(Item entity)
    {
        switch (entity.type)
        {
            case ItemID.Grenade:
                entity.ammo = entity.type;
                break;
            default:
                return;
        }
    }
}