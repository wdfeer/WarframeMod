using WarframeMod.Content.Buffs;
using WarframeMod.Content.Items.Consumables;

namespace WarframeMod.Common.GlobalNPCs;

public class GrimoireKillGlobalNPC : GlobalNPC
{
    public override bool InstancePerEntity => true;

    private int markedByTeam = -1;
    private bool jahu;
    private bool lohk;

    public void Mark(Player player, GrimoireUpgradeType upgradeType)
    {
        markedByTeam = player.team;
        switch (upgradeType)
        {
            case GrimoireUpgradeType.JahuCanticle:
                jahu = true;
                break;
            case GrimoireUpgradeType.LohkCanticle:
                lohk = true;
                break;
        }
    }

    public override bool PreKill(NPC self)
    {
        if (markedByTeam != -1)
        {
            if (jahu)
            {
                foreach (NPC other in Main.npc.Where(other =>
                             !other.friendly &&
                             self.Distance(other.position) < JahuCanticle.ICHOR_DISTANCE))
                {
                    other.AddBuff(BuffID.Ichor, 10 * 60);
                }
            }

            if (lohk)
            {
                foreach (var player in Main.player.Where(it => it.active && it.team == markedByTeam))
                {
                    player.AddBuff(ModContent.BuffType<LohkCanticleBuff>(), LohkCanticle.BUFF_TIME_SECONDS * 60,
                        Main.LocalPlayer == player);
                }
            }
        }

        return base.PreKill(self);
    }
}