using UnityEngine;

namespace Actors.Balls
{
    public interface IBallSettings
    {
        float Speed { get; }
        float JumpForce { get; }
    }

    [CreateAssetMenu]
    public class BallSettings : ScriptableObject, IBallSettings
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpForce;

        public float Speed => _speed;

        public float JumpForce => _jumpForce;
    }
}