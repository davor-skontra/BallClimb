using System;
using UniRx;
using UnityEngine;

namespace Actors.Balls
{
    public class PlayerPositionService
    {
        
        public void TrackPlayerHandler(BallHandler ballHandler)
        {
            PlayerPosition = ballHandler.Position;
        }
        
        public IObservable<Vector3> PlayerPosition { get; private set; } = new Subject<Vector3>();

    }
}