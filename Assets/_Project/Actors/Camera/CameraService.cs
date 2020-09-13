using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public interface ICameraService
{
    void MoveToPosition(Vector3 newPosition);
}

[RequireComponent(typeof(Camera))]
public class CameraService : MonoBehaviour, ICameraService
{
    [SerializeField] private Vector3 _positionAdjust;
    [SerializeField] private float _moveDuration;
    [SerializeField] private Ease _moveEase;

    private Tween _cameraMotion;

    public void MoveToPosition(Vector3 newPosition)
    {
        _cameraMotion?.Kill();
        _cameraMotion = transform
            .DOMove(newPosition + _positionAdjust, _moveDuration)
            .SetEase(_moveEase)
            .Play();
    }
}
