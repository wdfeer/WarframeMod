namespace WarframeMod.Common.GlobalNPCs;
public class StackableDebuffNPC : GlobalNPC
{
    public override bool InstancePerEntity => true;

    public List<BleedingBuff> bleeds = [];
    public List<ElectricityBuff> electricity = [];
    const int tickTime = 60;
    int bleedDPS = 0;
    int BleedDamagePerTick => bleedDPS * tickTime / 60;
    int bleedTimer = 0;
    int electroDPS = 0;
    int ElectroDamagePerTick => electroDPS * tickTime / 60;
    int electroTimer = 0;
    void ResetBleeds()
    {
        bleeds = [];
        bleedDPS = 0;
        bleedTimer = 0;
    }
    void ResetElectro()
    {
        electricity = [];
        electroDPS = 0;
        electroTimer = 0;
    }
    public override void UpdateLifeRegen(NPC npc, ref int damage)
    {
        UpdateBleeds(npc);
        UpdateElectro(npc);
    }
    void UpdateBleeds(NPC npc)
    {
        if (bleeds.Count == 0)
        {
            ResetBleeds();
            return;
        }
        if (npc.lifeRegen > 0)
            npc.lifeRegen = 0;
        bleeds = BleedingBuff.UpdateAll(bleeds, out bleedDPS);
        if (bleedTimer % tickTime == 0)
        {
            BleedingBuff.Damage(npc, BleedDamagePerTick);
            bleedTimer = 0;
        }
        bleedTimer++;
    }
    void UpdateElectro(NPC npc)
    {
        if (electricity.Count == 0)
        {
            ResetElectro();
            return;
        }
        if (npc.lifeRegen > 0)
            npc.lifeRegen = 0;
        electricity = ElectricityBuff.UpdateAll(electricity, out electroDPS);
        if (electroTimer % tickTime == 0)
        {
            ElectricityBuff.Damage(npc, ElectroDamagePerTick);
            electroTimer = 0;
        }
        electroTimer++;
        if (Main.rand.NextBool(3))
            Dust.NewDust(npc.position, npc.width, npc.height, DustID.Electric);
    }
}
