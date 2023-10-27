using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Personalities;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;
using WarframeMod.Content.EmoteBubbles;

namespace WarframeMod.Content.NPCs;
// [AutoloadHead] and NPC.townNPC are extremely important and absolutely both necessary for any Town NPC to work at all.
[AutoloadHead]
public class Ordis : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[Type] = 1; // The total amount of frames the NPC has

		NPCID.Sets.HatOffsetY[Type] = 4; // For when a party is active, the party hat spawns at a Y offset.


		// Connects this NPC with a custom emote.
		// This makes it when the NPC is in the world, other NPCs will "talk about him".
		// By setting this you don't have to override the PickEmote method for the emote to appear.
		NPCID.Sets.FaceEmote[Type] = ModContent.EmoteBubbleType<OrdisEmote>();


		// Set Example Person's biome and neighbor preferences with the NPCHappiness hook. You can add happiness text and remarks with localization (See an example in WarframeMod/Localization/en-US.lang).
		NPC.Happiness
			.SetBiomeAffection<ForestBiome>(AffectionLevel.Like) // Example Person prefers the forest.
			.SetBiomeAffection<SnowBiome>(AffectionLevel.Dislike) // Example Person dislikes the snow.
			.SetNPCAffection(NPCID.Dryad, AffectionLevel.Love) // Loves living near the dryad.
			.SetNPCAffection(NPCID.Guide, AffectionLevel.Like) // Likes living near the guide.
			.SetNPCAffection(NPCID.Merchant, AffectionLevel.Dislike) // Dislikes living near the merchant.
			.SetNPCAffection(NPCID.Demolitionist, AffectionLevel.Hate) // Hates living near the demolitionist.
		; // < Mind the semicolon!
	}

	public override void SetDefaults()
	{
		NPC.townNPC = true; // Sets NPC to be a Town NPC
		NPC.friendly = true; // NPC Will not attack player
		NPC.width = 100;
		NPC.height = 100;
		NPC.aiStyle = 7;
		NPC.damage = 0;
		NPC.lifeMax = 1;
	}
	public override bool CanBeHitByNPC(NPC attacker)
	{
		return false;
	}
	public override bool? CanBeHitByProjectile(Projectile projectile)
	{
		return false;
	}
	public override bool? CanBeHitByItem(Player player, Item item)
	{
		return false;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		// We can use AddRange instead of calling Add multiple times in order to add multiple items at once
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the preferred biomes of this town NPC listed in the bestiary.
				// With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

				new FlavorTextBestiaryInfoElement("Mods.WarframeMod.Bestiary.Ordis")
			});
	}
	public override void DrawEffects(ref Color drawColor)
	{
		for (int i = 0; i < 3; i++)
		{
			Vector2 pos = NPC.position + NPC.Size / 4;
			Vector2 area = NPC.Size / 2;

			Dust d = Dust.NewDustDirect(pos, (int)area.X, (int)area.Y, DustID.WhiteTorch);
			d.noGravity = true;
		}
	}
	public override bool CanTownNPCSpawn(int numTownNPCs)
	{ // Requirements for the town NPC to spawn.
		for (int k = 0; k < Main.maxPlayers; k++)
		{
			Player player = Main.player[k];
			if (!player.active)
			{
				continue;
			}
		}

		return false;
	}

	public override List<string> SetNPCNameList()
	{
		return new List<string>() {
				"Ordis",
			};
	}

	public override string GetChat()
	{
		WeightedRandom<string> chat = new WeightedRandom<string>();

		int partyGirl = NPC.FindFirstNPC(NPCID.PartyGirl);
		if (partyGirl >= 0 && Main.rand.NextBool(4))
		{
			chat.Add(Language.GetTextValue("Mods.WarframeMod.Dialogue.Ordis.PartyGirlDialogue", Main.npc[partyGirl].GivenName));
		}
		// These are things that the NPC has a chance of telling you when you talk to it.
		chat.Add(Language.GetTextValue("Mods.WarframeMod.Dialogue.Ordis.StandardDialogue1"));
		chat.Add(Language.GetTextValue("Mods.WarframeMod.Dialogue.Ordis.StandardDialogue2"));
		chat.Add(Language.GetTextValue("Mods.WarframeMod.Dialogue.Ordis.StandardDialogue3"));
		chat.Add(Language.GetTextValue("Mods.WarframeMod.Dialogue.Ordis.StandardDialogue4"));
		chat.Add(Language.GetTextValue("Mods.WarframeMod.Dialogue.Ordis.CommonDialogue"), 5.0);
		chat.Add(Language.GetTextValue("Mods.WarframeMod.Dialogue.Ordis.RareDialogue"), 0.1);

		string chosenChat = chat; // chat is implicitly cast to a string. This is where the random choice is made.

		// Here is some additional logic based on the chosen chat line. In this case, we want to display an item in the corner for StandardDialogue4.
		if (chosenChat == Language.GetTextValue("Mods.WarframeMod.Dialogue.Ordis.StandardDialogue4"))
		{
			// Main.npcChatCornerItem shows a single item in the corner, like the Angler Quest chat.
			Main.npcChatCornerItem = ItemID.HiveBackpack;
		}

		return chosenChat;
	}

	public override void SetChatButtons(ref string button, ref string button2)
	{ // What the chat buttons are when you open up the chat UI
		button = Language.GetTextValue("LegacyInterface.28");
		button2 = "Awesomeify";
		if (Main.LocalPlayer.HasItem(ItemID.HiveBackpack))
		{
			button = "Upgrade " + Lang.GetItemNameValue(ItemID.HiveBackpack);
		}
	}

	public override void OnChatButtonClicked(bool firstButton, ref string shop)
	{
		if (firstButton)
		{

			if (Main.LocalPlayer.HasItem(ItemID.HiveBackpack))
			{
				Main.npcChatText = $"OnChatButtonClicked({firstButton}, {shop})";
				return;
			}
		}
	}
}