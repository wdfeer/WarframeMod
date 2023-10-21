using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories.Auras;
public class PowerDonation : ModItem
{
    public const float DAMAGE = 0.3f;
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 32;
        Item.height = 32;
        Item.rare = 4;
        Item.value = Item.sellPrice(gold: 2);
    }
    public override void AddRecipes()
        => CreateRecipe().AddIngredient(ItemID.SoulofLight, 15)
                         .AddIngredient(ItemID.SoulofNight, 15)
                         .AddTile(TileID.Anvils)
                         .Register();
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AuraPlayer>().myAuras.powerDonation = true;
        player.GetDamage(DamageClass.Generic) -= DAMAGE;
    }
}
class PowerDonationPlayer : ModPlayer
{
    public int TeamCount => Player.GetModPlayer<AuraPlayer>().CountAurasInMyTeamExceptMe(x => x.powerDonation);
    public override void PostUpdateEquips()
    {
        Player.GetDamage(DamageClass.Generic) += TeamCount * PowerDonation.DAMAGE;
    }
}