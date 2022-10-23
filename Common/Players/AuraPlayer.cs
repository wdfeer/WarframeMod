using Terraria.Enums;

namespace WarframeMod.Common.Players;
public struct AuraData
{
    public bool corrosiveProjection;
    public bool physique;
    public bool standUnited;
    public bool sprintBoost;
    public bool powerDonation;
    public const int byteCount = 5;
    public byte[] toBytes()
    {
        byte Btb(bool boolean) => boolean ? (byte)1 : (byte)0;
        return new bool[byteCount] { corrosiveProjection, physique, standUnited, sprintBoost, powerDonation }
            .Select(x => Btb(x))
            .ToArray();
    }
    public static AuraData fromBytes(byte[] buffer)
    {
        bool Btb(byte b) => b == 1;
        return new AuraData() { corrosiveProjection = Btb(buffer[0]), physique = Btb(buffer[1]), standUnited = Btb(buffer[2]), sprintBoost = Btb(buffer[3]), powerDonation = Btb(buffer[4]) };
    }
}
public class AuraPlayer : ModPlayer
{
    public AuraData myAuras = new AuraData();
    public static AuraData[] playerAuras = new AuraData[Main.maxPlayers];
    public static IEnumerable<AuraData> RealPlayerAuras => playerAuras.Select(
        (x, i) => (Main.player[i] == null || !Main.player[i].active) ? new AuraData() : x
    ).ToArray();
    public bool AnyPlayerInMyTeam(Func<AuraData, bool> predicate)
        => predicate(myAuras) || (Player.team != (int)Team.None && RealPlayerAuras.Select((d, i) => (d, i)).Any(x => predicate(x.d) && Main.player[x.i].team == Player.team));
    public int CountAurasInMyTeam(Func<AuraData, bool> predicate)
        => (predicate(myAuras) ? 1 : 0) +
           CountAurasInMyTeamExceptMe(predicate);
    public int CountAurasInMyTeamExceptMe(Func<AuraData, bool> predicate)
        => (Player.team != (int)Team.None ?
           RealPlayerAuras.Select((d, i) => (d, i)).Count(x => predicate(x.d) && Main.player[x.i].team == Player.team && x.i != Player.whoAmI)
           : 0);

    public override void ResetEffects()
    {
        myAuras = new AuraData();
    }
    public static bool CanSync() => (int)Main.time % 100 == 0;
    public override void PostUpdateEquips()
    {
        if (Main.netMode != NetmodeID.SinglePlayer && CanSync())
        {
            SendAuraPacket(Mod, Player.whoAmI, myAuras.toBytes());
        }
    }
    public static void SendAuraPacket(Mod mod, int from, byte[] auras)
    {
        ModPacket packet = mod.GetPacket();
        packet.Write((byte)WarframeMod.MessageType.AuraSync);
        packet.Write((byte)from);
        packet.Write(auras);
        packet.Send();
    }
}
