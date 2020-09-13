using Actors.Balls;
using EndGameTracking;
using UnityEngine;
using UnityEngine.SceneManagement;
using Services = AlkarInjector.Alkar.Services;

namespace Core
{
    public class CompositionRoot : MonoBehaviour
    {
        [SerializeField] private BallSettings _ballSettings;
        [SerializeField] private PlayerInputBehaviour _playerInput;
        [SerializeField] private CameraService _cameraService;
        [SerializeField] private EndGameMessages _endGameMessages;
        [SerializeField] private EndGameCanvas _endGameCanvas;

        private void Awake()
        {
            var playerPositionService = new PlayerPositionService();
            var inputFactory = new BallInputFactory(_playerInput, playerPositionService, _ballSettings);
            var endGameService = new EndGameService(_endGameMessages);
            
            _playerInput.Initialize(playerPositionService, endGameService);
            _endGameCanvas.Initialize(endGameService);

            Services.Register<ICameraService>(_cameraService);
            Services.Register(playerPositionService);
            Services.Register(inputFactory);
            Services.Register<IBallSettings>(_ballSettings);
            Services.Register<IEndGameService>(endGameService);

            SceneManager
                .LoadSceneAsync(Scenes.Play, LoadSceneMode.Additive)
                .completed += _ => { SceneManager.SetActiveScene(SceneManager.GetSceneByName(Scenes.Play)); };
        }
    }
}