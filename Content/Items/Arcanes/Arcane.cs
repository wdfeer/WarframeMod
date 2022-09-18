using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarframeMod.Content.Items.Arcanes;
public abstract class Arcane : ModItem
{
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.rare = -12;
        Item.expert = true;
        Item.value = Item.sellPrice(gold: 4);
    }
}
