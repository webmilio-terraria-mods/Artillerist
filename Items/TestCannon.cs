using System;
using Artillerist.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WebmilioCommons;
using WebmilioCommons.Extensions;
using WebmilioCommons.Helpers;
using WebmilioCommons.Tinq;

namespace Artillerist.Items;

public class TestCannon : ModItem
{
    private Vector2 _peak;

    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 22;

        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.useTime = Item.useAnimation = 20;

        Item.autoReuse = true;
    }

    public override bool? UseItem(Player player)
    {
        var position = player.Center;
        var target = Main.MouseWorld;

        var v = target - position;
        var angle = v.ToNormalRotation();

        var magnitude = PhysicsHelpers.PointABVelocity(position.Y, target.Y, Math.Abs(target.X - position.X), angle, Constants.GravityInMeters * 1.05f / Constants.TicksPerSecond);

        System.Diagnostics.Debug.WriteLine($"{angle} || {magnitude}");
        var vector = VectorHelpers.FromDirection(angle, magnitude);

        Shoot(player, position, vector);

        return true;
    }

    private static void Shoot(Player player, Vector2 position, Vector2 velocity)
    {
        Projectile.NewProjectile(player.AsItemProjectileSource(), position, velocity,
            ModContent.ProjectileType<ArtilleryCannonBall>(), 50, 4.5f, player.whoAmI);
    }

    private static Entity FindNearestAlive(Vector2 position)
    {
        return Main.npc.WhereActive(n => !n.friendly && n.damage > 0).Nearest(position);
    }
}