using System;
using UniRx;
using UnityEngine;

namespace Actors.Balls
{
    public class PlayerPositionService
    {

        private BallHandler _playerBallHandler;
        
        public void TrackPlayerHandler(BallHandler ballHandler)
        {
            _playerBallHandler = ballHandler;
        }

        public Vector3? PlayerPosition => _playerBallHandler?.Position;
    }
}