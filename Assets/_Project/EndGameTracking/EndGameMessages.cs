using UnityEngine;

namespace EndGameTracking
{
    public interface IEndGameMessages
    {
        string Victory { get; }
        string Defeat { get; }
    }

    [CreateAssetMenu]
    public class EndGameMessages : ScriptableObject, IEndGameMessages
    {
        [SerializeField] private string _victory;
        [SerializeField] private string _defeat;

        public string Victory => _victory;

        public string Defeat => _defeat;
    }
}