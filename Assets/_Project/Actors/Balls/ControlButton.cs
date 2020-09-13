using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Actors.Balls
{
    public class ControlButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private UnityEvent<bool> _onPointer;

        public void OnPointerDown(PointerEventData eventData)
        {
            _onPointer?.Invoke(true);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _onPointer?.Invoke(false);
        }
    }
}
