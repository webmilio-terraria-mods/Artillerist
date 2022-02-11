using Terraria.ModLoader;
using WebmilioCommons;

namespace Artillerist.Projectiles;

public class ArtilleryCannonBall : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = Projectile.height = 18;
        Projectile.friendly = true;
        Projectile.penetrate = 4;
    }

    public override void AI()
    {
        Projectile.velocity.Y += Constants.GravityInMeters / Constants.TicksPerSecond;
    }
}