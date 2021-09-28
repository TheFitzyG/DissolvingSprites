using UnityEngine;
using UnityEngine.Events;

namespace TFG_SP
{
    [RequireComponent(typeof(Collider2D))]
    public class EventOnTrigger2D : MonoBehaviour
    { 
        [Tooltip("Trigger enter only once")]
        [SerializeField] private bool _oneShot;
        private bool _triggered;

        [SerializeField] private LayerMask _layerMask;

        public UnityEvent _onTriggerEnterEvent;
        public UnityEvent _onTriggerExitEvent;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_oneShot || !_triggered)
            {
                if (TfgChecks.CompareLayers(_layerMask, other.gameObject))
                {
                    _onTriggerEnterEvent?.Invoke();

                    if (_oneShot)
                    {
                        _triggered = true;
                    }
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!_oneShot || !_triggered)
            {
                if (TfgChecks.CompareLayers(_layerMask, other.gameObject))
                {
                    _onTriggerExitEvent?.Invoke();
                }
            }
        }
    }
}
