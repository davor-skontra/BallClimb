using System;
using AlkarInjector;
using AlkarInjector.Attributes;
using Core.Disposal;
using UnityEngine.UI;
using UniRx;
using UnityEngine;

namespace EndGameTracking
{
    public class EndGameCanvas : MonoBehaviour
    {
        [InjectChildComponent] private Text _text;
        
        private CompositeDisposable _disposable = new CompositeDisposable();

        public void Initialize(IEndGameService endGameService)
        {
            Alkar.InjectMonoBehaviour(this);

            _disposable.Add(
                endGameService
                    .EndGameWithMessage
                    .Subscribe(x => _text.text = x)
            );
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}