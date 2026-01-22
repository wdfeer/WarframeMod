namespace WarframeMod.Content.Items.Accessories;

public class EnergyGenerator : ModItem
{
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 44;
        Item.height = 64;
        Item.rare = ItemRarityID.Pink;
        Item.value = Item.sellPrice(gold: 8);
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        EnergyGeneratorPlayer modPlayer = player.GetModPlayer<EnergyGeneratorPlayer>();
        modPlayer.active = true;
    }
}

class EnergyGeneratorPlayer : ModPlayer
{
    public bool active;
    public int counter;

    public override void ResetEffects()
    {
        if (!active)
        {
            counter = 0;
        }

        active = false;
    }

    public override void UpdateEquips()
    {
        if (counter >= 10 && Player.numMinions > 0)
        {
            var minions =
                Main.projectile.Where(it => it.active && it.minion && it.owner == Player.whoAmI).ToArray();
            if (minions.Any())
            {
                var minion = minions[Random.Shared.Next() % minions.Length];
                Item.NewItem(minion.GetSource_FromThis(), minion.getRect(), new Item(ItemID.Star));

                counter -= 10;
            }
        }
    }

    public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (hit.DamageType == DamageClass.Summon)
        {
            target.GetGlobalNPC<EnergyGeneratorGlobalNPC>().marked = true;
        }

        base.OnHitNPCWithProj(proj, target, hit, damageDone);
    }
}

class EnergyGeneratorGlobalNPC : GlobalNPC
{
    public override bool InstancePerEntity => true;
    public bool marked;

    public override void OnKill(NPC npc)
    {
        if (!marked) return;

        var validPlayers = Main.player.Where(pl =>
            pl.active && pl.GetModPlayer<EnergyGeneratorPlayer>().active && pl.Distance(npc.position) < 50 * 16);
        foreach (var player in validPlayers)
        {
            player.GetModPlayer<EnergyGeneratorPlayer>().counter++;
        }
    }
}