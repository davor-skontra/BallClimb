using System;
using Actors.Balls;
using AlkarInjector;
using AlkarInjector.Attributes;
using UnityEngine;

namespace Actors.Camera
{
    public class PlayerContactMovesCamera: MonoBehaviour
    {
        [Inject] private ICameraService _cameraService;
        
        private static PlayerContactMovesCamera _currentCameraHolder;

        private void Awake()
        {
            Alkar.InjectMonoBehaviour(this);
        }

        private void OnCollisionEnter(Collision other)
        {
            var ball = other.gameObject.GetComponent<BallBehaviour>();
            var isPlayer = ball != null && ball.BallKind == BallKind.Player;
            var isBellow = transform.position.y < other.transform.position.y;
            if (isPlayer && isBellow && _currentCameraHolder != this)
            {
                _currentCameraHolder = this;
                _cameraService.MoveToPosition(transform.position);
            }
        }
    }
}