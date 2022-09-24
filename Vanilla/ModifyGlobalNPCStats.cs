using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarframeMod.Vanilla;
internal class VanillaNPCStats : GlobalNPC
{
    public const float MAX_LIFE_MULT = 1.08f;
    public const float DEFENSE_MULT = 1.08f;
    public const float DAMAGE_MULT = 1.11f;
    public override void SetDefaults(NPC npc)
    {
        base.SetDefaults(npc);
        npc.lifeMax = (int)(npc.lifeMax * MAX_LIFE_MULT);
        npc.life = (int)(npc.life * MAX_LIFE_MULT);
        npc.defense = (int)(npc.defense * DEFENSE_MULT);
        npc.damage = (int)(npc.damage * DAMAGE_MULT);
    }
}
