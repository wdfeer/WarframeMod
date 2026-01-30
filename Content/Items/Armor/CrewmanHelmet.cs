using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Localization;

namespace WarframeMod.Content.Items.Armor;

[AutoloadEquip(EquipType.Head)]
public class CrewmanHelmet : ModItem
{
    public const int CRIT_CHANCE = 3;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(CRIT_CHANCE);

    private static Asset<Texture2D> glowTexture;

    public override void Load()
    {
        glowTexture = ModContent.Request<Texture2D>(Texture + "_Glow");
    }

    public override void SetStaticDefaults()
    {
        ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false; // Don't draw the head at all. Used by Space Creature Mask
    }

    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = Item.sellPrice(silver: 50);
        Item.rare = ItemRarityID.Blue;
        Item.defense = 2;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddRecipeGroup(RecipeGroupID.IronBar, 4)
            .AddIngredient(ItemID.SilverCoin, 50)
            .AddTile(TileID.Anvils)
            .Register();
    }

    public override void UpdateEquip(Player player)
    {
        player.GetCritChance<GenericDamageClass>() += 3;
    }

    // TODO: add glowmask for when actually wearing the helmet
    public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation,
        float scale,
        int whoAmI)
    {
        Texture2D texture = glowTexture.Value;
        spriteBatch.Draw
        (
            texture,
            new Vector2
            (
                Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f
            ),
            new Rectangle(0, 0, texture.Width, texture.Height),
            Color.White,
            rotation,
            texture.Size() * 0.5f,
            scale,
            SpriteEffects.None,
            0f
        );
    }
}