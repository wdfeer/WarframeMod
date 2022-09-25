using Terraria.ModLoader.IO;
using WarframeMod.Content.Items.Arcanes;

namespace WarframeMod.Common.Players;
public class ArcanePlayer : ModPlayer
{
    public Arcane equippedArcane = null;
    public int equippedArcaneType = -1;
    public int equippedArcaneIndex = -1;
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
        if (!tag.ContainsKey("equippedArcaneIndex"))
            return;
        int? i = tag["equippedArcaneIndex"] as int?;
        equippedArcaneIndex = i == null ? -1 : (int)i;
    }
    public override void OnEnterWorld(Player player)
    {
        if (equippedArcaneIndex != -1)
        {
            equippedArcaneType = Arcane.arcaneTypeGetters[equippedArcaneIndex]();
            equippedArcane = ModContent.GetModItem(equippedArcaneType) as Arcane;
            Main.NewText($"{equippedArcane.DisplayName.GetDefault()} is currently equipped", Color.Yellow);
        }
    }
    public override void SaveData(TagCompound tag)
    {
        if (equippedArcaneType != -1)
        {
            tag.Add("equippedArcaneIndex", Arcane.GetArcaneIndex(equippedArcaneType));
        }
    }
}
