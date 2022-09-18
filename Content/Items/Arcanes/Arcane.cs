using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Arcanes;
public abstract class Arcane : ModItem
{
    public override void SetDefaults()
    {
        Item.rare = -12;
        Item.expert = true;
        Item.value = Item.sellPrice(gold: 4);
        Item.useAnimation = 20;
        Item.useTime = 20;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.noUseGraphic = true;
        Item.UseSound = SoundID.Unlock;
    }
    public override bool? UseItem(Player player)
    {
        player.GetModPlayer<ArcanePlayer>().EquipArcane(this);
        return true;
    }
    public abstract void UpdateArcane(Player player);
}
