namespace WarframeMod.Common;

public class Calamity : ModSystem
{
    public override void PostAddRecipes()
    {
        loaded = ModLoader.TryGetMod("CalamityMod", out calamityMod);
        if (loaded)
        {
            string rogueName = "CalamityMod/RogueDamageClas";
            if (DamageClass.Search.ContainsName(rogueName))
            {
                int id = DamageClass.Search.GetId(rogueName);
                rogue = DamageClassLoader.GetDamageClass(id);
            }
        }
    }

    public static bool loaded;
    public static Mod calamityMod;
    public static DamageClass rogue;
}