namespace WarframeMod.Global;
internal class BleedingGlobalNPC : GlobalNPC
{
    public override bool InstancePerEntity => true;
    public List<BleedingBuff> bleeds = new();
    const int bleedTickTime = 60;
    int bleedDPS = 0;
    int BleedDamagePerTick => bleedDPS * bleedTickTime / 60;
    int bleedTimer = 0;
    void ResetBleeds()
    {
        bleeds = new List<BleedingBuff>();
        bleedDPS = 0;
        bleedTimer = 0;
    }
    public override void UpdateLifeRegen(NPC npc, ref int damage)
    {
        if (bleeds.Count == 0)
        {
            ResetBleeds();
            return;
        }
        if (npc.lifeRegen > 0)
            npc.lifeRegen = 0;
        bleeds = BleedingBuff.UpdateBleeds(bleeds, out bleedDPS);
        if (bleedTimer % bleedTickTime == 0)
        {
            int oldDefense = npc.defense;
            npc.defense = 0;
            npc.StrikeNPC(BleedDamagePerTick, 0, -2);
            npc.defense = oldDefense;
            bleedTimer = 0;
        }
        bleedTimer++;
    }
}
