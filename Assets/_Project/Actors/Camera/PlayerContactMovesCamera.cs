using System;
using Actors.Balls;
using AlkarInjector;
using AlkarInjector.Attributes;
using UnityEngine;
using Utilities;

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
            var isBellow = transform.position.y < other.transform.position.y;
            if (other.IsPlayer() && isBellow && _currentCameraHolder != this)
            {
                _currentCameraHolder = this;
                _cameraService.MoveToPosition(transform.position);
            }
        }
    }
}