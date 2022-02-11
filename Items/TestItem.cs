using Artillerist.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WebmilioCommons.Extensions;

namespace Artillerist.Items;

public class TestItem : ModItem
{
    public override void SetDefaults()
    {
        Item.width = Item.height = 40;

        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.useTime = Item.useAnimation = 10;

        Item.damage = 100;
        Item.shootSpeed = 10;
        Item.knockBack = 4.5f;

        Item.noMelee = true;
        Item.autoReuse = true;
    }

    public override bool? UseItem(Player player)
    {
        if (!player.IsLocalPlayer())
            return null;

        ArtilleryHelper.SpawnArtilleryProjectilesLocal(ModContent.ProjectileType<ArtilleryCannonBall>(), 500, 3, 3, 15,
            100, -100);

        return true;
    }
}