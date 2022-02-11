using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using WebmilioCommons;

namespace Artillerist.Players;

public class ArtilleristPlayer : ModPlayer
{
    private int _timer;
    private Vector2 _lastPosition;

    public override void PostUpdate()
    {
        if (_timer == 0)
        {
            if (_lastPosition != Vector2.Zero)
                System.Diagnostics.Debug.WriteLine(_lastPosition - Player.position);

            _lastPosition = Player.position;
        }

        _timer = ++_timer % Constants.TicksPerSecond;
    }
}