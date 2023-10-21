namespace WarframeMod.Common.GlobalItems;

public class CritGlobalItem : GlobalItem
{
    public override bool InstancePerEntity => true;
    public float critMultiplier = 1f;
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        if (item.damage == 0 || tooltips.Any(t => t.Name == "CritChance"))
            return;
        int index = tooltips.FindIndex(t => t.Name == "Damage") + 1;
        if (index == 0)
            return;
        int crit = Main.LocalPlayer.GetWeaponCrit(item);
        tooltips.Insert(index, new TooltipLine(Mod, "CritChance", $"{crit}% critical chance"));
    }
}
