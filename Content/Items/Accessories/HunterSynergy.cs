using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;

public class HunterSynergy : HunterAccessory
{
    public const int CRIT_LEECH_PERCENT = 33;
    public override string DefaultTooltip => $"Increase summon critical chance by {CRIT_LEECH_PERCENT}% of the highest base crit chance of all weapons in inventory\nCurrent bonus: 0%";
    int critBonus = 0;
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var t1 = tooltips.Find(t => t.Name == "Tooltip1");
        if (t1 != null)
            t1.Text = $"Current bonus: {critBonus}%";
    }
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 44;
        Item.height = 64;
        Item.rare = 2;
        Item.value = Item.sellPrice(silver: 50);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        base.UpdateAccessory(player, hideVisual);
        Item maxCritWeapon = player.inventory.MaxBy(i => (i != null && i.stack > 0) ? i.crit : 0);
        if (maxCritWeapon != null)
        {
            critBonus = (int)(CRIT_LEECH_PERCENT * (maxCritWeapon.crit + 4) / 100f);
            player.GetModPlayer<CritPlayer>().summonCritChance += critBonus;
        }
    }
}
