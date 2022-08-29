using System.IO;
using WarframeMod.Common.Players;

namespace WarframeMod;
public partial class WarframeMod : Mod
{
	public enum MessageType
	{
		CombatTextCritLevel
	}
	public override void HandlePacket(BinaryReader reader, int whoAmI)
	{
		MessageType type = (MessageType)reader.ReadByte();
		switch (type)
		{
			case MessageType.CombatTextCritLevel:
				byte combatText = reader.ReadByte();
				byte critLevel = reader.ReadByte();
				if (Main.netMode == NetmodeID.Server)
				{
					ModPacket packet = GetPacket();
					packet.Write((byte)MessageType.CombatTextCritLevel);
                    packet.Write((byte)combatText);
                    packet.Write((byte)critLevel);
					packet.Send(ignoreClient: whoAmI);
				}
				else
				{
                    Main.combatText[combatText].color = CritPlayer.GetCritColor(critLevel);
                }
				break;
			default:
				throw new Exception("Invalid MessageType!");
		}
	}
}