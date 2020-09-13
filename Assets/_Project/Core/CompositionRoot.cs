using Actors.Balls;
using UnityEngine;
using UnityEngine.SceneManagement;
using Services = AlkarInjector.Alkar.Services;

namespace Core
{
    public class CompositionRoot : MonoBehaviour
    {
        [SerializeField] private BallSettings _ballSettings;
        [SerializeField] private PlayerInputBehaviour _playerInput;

        private void Awake()
        {
            var playerPositionService = new PlayerPositionService();
            var inputFactory = new BallInputFactory(_playerInput, playerPositionService);

            Services.Register(playerPositionService);
            Services.Register(inputFactory);
            Services.Register<IBallSettings>(_ballSettings);

            SceneManager
                .LoadSceneAsync(Scenes.Play, LoadSceneMode.Additive)
                .completed += _ => { SceneManager.SetActiveScene(SceneManager.GetSceneByName(Scenes.Play)); };
        }
    }
}