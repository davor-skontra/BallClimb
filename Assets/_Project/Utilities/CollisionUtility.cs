using Actors.Balls;
using UnityEngine;

namespace Utilities
{
    public static class CollisionUtility
    {
        public static bool IsPlayer(this Collision self)
        {
            var ball = self.gameObject.GetComponent<BallBehaviour>();
            var isPlayer = ball != null && ball.BallKind == BallKind.Player;

            return isPlayer;
        }
    }
}