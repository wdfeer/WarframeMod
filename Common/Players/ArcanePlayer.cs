using Terraria.ModLoader.IO;
using WarframeMod.Content.Items.Arcanes;

namespace WarframeMod.Common.Players;
public class ArcanePlayer : ModPlayer
{
    public Arcane equippedArcane = null;
    public int equippedArcaneType = -1;
    public void EquipArcane(Arcane arcane)
    {
        Main.NewText($"Equipped {arcane.Item.Name}", Color.Yellow);
        if (equippedArcaneType != -1)
            Player.QuickSpawnItem(arcane.Item.GetSource_FromThis(), equippedArcaneType);
        equippedArcane = arcane;
        equippedArcaneType = arcane.Type;
        arcane.Item.stack = 0;
    }
    public override void PostUpdateBuffs()
    {
        if (equippedArcane != null)
            equippedArcane.UpdateArcane(Player);
    }
    public override void LoadData(TagCompound tag)
    {
        int? type = tag["equippedArcaneType"] as int?;
        equippedArcaneType = type == null ? -1 : (int)type;
        if (equippedArcaneType != -1)
        {
            Item arcane = new Item(0);
            arcane.type = equippedArcaneType;
            arcane.SetDefaults(equippedArcaneType);
            equippedArcane = arcane.ModItem as Arcane;
            Main.NewText($"{arcane.Name} is currently equipped", Color.Yellow);
        }
    }
    public override void SaveData(TagCompound tag)
    {
        if (equippedArcaneType != -1)
        {
            tag.Add("equippedArcaneType", equippedArcaneType);
        }
    }
}
