using Microsoft.Xna.Framework.Graphics;

namespace WarframeMod.Content.Items.Arcanes;

public abstract class Arcane : ModItem
{
    public static readonly Func<int>[] bossArcaneTypeGetters =
    [
        ModContent.ItemType<ArcaneAvenger>,
        ModContent.ItemType<ArcaneGuardian>,
        ModContent.ItemType<VirtuosStrike>,
        ModContent.ItemType<ArcaneStrike>,
        ModContent.ItemType<ArcanePrecision>,
        ModContent.ItemType<ArcaneFury>,
        ModContent.ItemType<ArcaneBodyguard>,
        ModContent.ItemType<ArcaneArachne>,
        ModContent.ItemType<ArcaneVictory>,
        ModContent.ItemType<MoltAugmented>,
        ModContent.ItemType<EternalOnslaught>,
        ModContent.ItemType<ArcaneConsequence>,
        ModContent.ItemType<ArcaneGrace>,
        ModContent.ItemType<EmergenceSavior>,
        ModContent.ItemType<ArcanePistoleer>,
        ModContent.ItemType<ArcaneBlessing>,
        ModContent.ItemType<ArcaneAcceleration>,
        ModContent.ItemType<ArcaneEruption>,
        ModContent.ItemType<ArcaneBattery>,
        ModContent.ItemType<MoltVigor>,
        ModContent.ItemType<ArcaneCircumvent>,
        ModContent.ItemType<ArcaneHealing>,
        ModContent.ItemType<ArcaneIce>,
        ModContent.ItemType<EternalLogistics>,
        ModContent.ItemType<CascadiaOvercharge>,
    ];

    /// <returns>List of types of arcanes that should drop from all bosses</returns>
    public static int[] GetArcaneTypesFromBosses()
        => bossArcaneTypeGetters.Select(x => x()).ToArray();

    public static int GetArcaneIndex(int type)
        => Array.IndexOf(GetArcaneTypesFromBosses(), type);

    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.rare = ItemRarityID.Expert;
        Item.expert = true;
        Item.value = Item.sellPrice(gold: 4);
    }

    public abstract void UpdateArcane(Player player);

    public override void UpdateAccessory(Player player, bool hideVisual)
        => UpdateArcane(player);

    public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
    {
        return equippedItem.ModItem is not Arcane || incomingItem.ModItem is not Arcane;
    }

    public override bool PreDrawInWorld(SpriteBatch spriteBatch,
        Color lightColor,
        Color alphaColor,
        ref float rotation,
        ref float scale,
        int whoAmI)
    {
        Texture2D texture = ModContent.Request<Texture2D>("WarframeMod/Assets/DroppedArcane").Value;
        Vector2 pos = Item.Center - new Vector2(texture.Width / 2f, texture.Height / 2f) - Main.screenPosition;
        Rectangle drawRect = new((int)pos.X, (int)pos.Y, texture.Width, texture.Height);
        Vector2 origin = new Vector2(texture.Width / 2f, texture.Height / 2f);
        spriteBatch.Draw(texture, drawRect, null, lightColor, rotation, origin, SpriteEffects.None, 0f);

        return false;
    }
}