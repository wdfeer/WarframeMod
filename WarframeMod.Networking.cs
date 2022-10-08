using System.IO;
using WarframeMod.Common.Players;
using WarframeMod.Content.Items.Accessories;

namespace WarframeMod;
public partial class WarframeMod : Mod
{
    public enum MessageType
    {
        SetProjectileExtraUpdates,
        AuraSync
    }
    public override void HandlePacket(BinaryReader reader, int whoAmI)
    {
        MessageType type = (MessageType)reader.ReadByte();
        switch (type)
        {
            case MessageType.SetProjectileExtraUpdates:
                short proj = reader.ReadInt16();
                short extraUpdates = reader.ReadInt16();
                switch (Main.netMode)
                {
                    case NetmodeID.Server:
                        SendProjectileExtraUpdatesPacket(proj, extraUpdates, whoAmI);
                        return;
                    case NetmodeID.MultiplayerClient:
                        Main.projectile[proj].extraUpdates = extraUpdates;
                        return;
                    default:
                        return;
                }
            case MessageType.AuraSync:
                switch (Main.netMode)
                {
                    case NetmodeID.Server:
                        AuraPlayer.SendAuraPacket(this, reader.ReadByte(), reader.ReadBytes(AuraData.byteCount));
                        return;
                    case NetmodeID.MultiplayerClient:
                        byte from = reader.ReadByte();
                        AuraData data = AuraData.fromBytes(reader.ReadBytes(AuraData.byteCount));
                        AuraPlayer.playerAuras[from] = data;
                        return;
                    default:
                        return;
                }
            default:
                throw new Exception("Invalid MessageType!");
        }
    }
    public void SetProjectileExtraUpdatesNetSafe(Projectile proj, int extraUpdates)
        => SetProjectileExtraUpdatesNetSafe(proj.whoAmI, extraUpdates);
    public void SetProjectileExtraUpdatesNetSafe(int proj, int extraUpdates)
    {
        Main.projectile[proj].extraUpdates = extraUpdates;
        if (Main.netMode == NetmodeID.SinglePlayer)
            return;
        SendProjectileExtraUpdatesPacket(proj, extraUpdates);
    }
    void SendProjectileExtraUpdatesPacket(int proj, int extraUpdates, int ignoreClient = -1)
    {
        ModPacket packet = GetPacket();
        packet.Write((byte)MessageType.SetProjectileExtraUpdates);
        packet.Write((short)proj);
        packet.Write((short)extraUpdates);
        packet.Send(ignoreClient: ignoreClient);
    }
}