using System;
using System.Numerics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Artillerist;

public static class ArtilleryHelper
{
    public static int[] SpawnArtilleryProjectilesLocal(int projectile, int damage, float knockback, int count, float projectileSpeed, 
        int randomLandX = 0, int individualYOffset = 0)
    {
        return SpawnArtilleryProjectilesLocal(Main.LocalPlayer, Main.MouseScreen + Main.screenPosition, projectile, damage, knockback,
            count, projectileSpeed, randomLandX, individualYOffset);
    }

    public static int[] SpawnArtilleryProjectilesLocal(Player player, Vector2 target, int projectile, int damage, float knockback,
        int count, float projectileSpeed,
        int randomLandX = 0, int individualYOffset = 0)
    {
        var projectiles = new int[count];
        for (int i = 0; i < count; ++i)
        {
            if (randomLandX > 0)
                target.X += Main.rand.Next(randomLandX * -1 + 1, randomLandX);

            var position = new Vector2(player.Center.X, player.MountedCenter.Y - Main.screenHeight / 2 + individualYOffset * i);
            projectiles[i] = SpawnArtilleryProjectile(position, target, projectile, damage, knockback, projectileSpeed, player.whoAmI);
        }

        return projectiles;
    }

    public static int SpawnArtilleryProjectile(Vector2 position, Vector2 target, int projectile, int damage, float knockback, float projectileSpeed, int owner)
    {
        var speed = target - position;
        float ai1 = speed.Y + position.Y;

        speed *= projectileSpeed / speed.Length();

        var id = Projectile.NewProjectile(new ProjectileSource_Item(Main.player[owner], Main.player[owner].HeldItem),
            position, speed, projectile,
            damage, knockback, owner, ai1: ai1);

        if (id == -1)
            return -1;

        return id;
    }

    public static int[] SpawnLunarFlaresLocal(int damage, float knockback, int count, int randomBounds = 201)
    {
        var owner = Main.LocalPlayer;
        int[] projectiles = new int[count];

        for (int i = 0; i < count; ++i)
        {
            var position = new Vector2(
                // X Coordinates
                (owner.Center.X +
                 (randomBounds > 0 ? Main.rand.Next(randomBounds) * -owner.direction : 0) +
                 Main.mouseX + Main.screenPosition.X + owner.Center.X - owner.position.X) / 2f +
                 (randomBounds > 0 ? Main.rand.Next(randomBounds * -1 + 1, randomBounds) : 0),

                // Y Coordinates
                owner.MountedCenter.Y - Main.screenHeight / 2 - 100 * i);

            projectiles[i] = SpawnLunarFlare(new ProjectileSource_Item(owner, owner.HeldItem), position,
                Main.MouseScreen + Main.screenPosition, damage, knockback, owner.HeldItem.shootSpeed, owner.whoAmI);
        }

        return projectiles;
    }

    public static int SpawnLunarFlare(IProjectileSource source, Vector2 position, Vector2 target, int damage,
        float knockback, float projectileSpeed, int owner)
    {
        var speed = target - position;
        float ai2 = speed.Y + position.Y;

        if (speed.Y < 20f)
            speed.Y = 20f;

        speed *= (projectileSpeed / speed.Length()) / 2;

        return SpawnLunarFlare(source, position, speed, damage, knockback, owner, ai2);
    }

    public static int SpawnLunarFlare(IProjectileSource source, Vector2 position, Vector2 speed, int damage, 
        float knockback, int owner, float ai1)
    {
        return Projectile.NewProjectile(source, position, speed, ProjectileID.LunarFlare, damage, knockback,
            owner, ai1: ai1);
    }
}