using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UIDemo {

  [RequireComponent(typeof(Image))]
  public class PhysicsBasedCheckBox : PhysicsBasedUIElement, IPointerDownHandler, IPointerUpHandler {
    [SerializeField] GameObject _tickIcon;
    [SerializeField] float _scaleStrength = -0.03f;
    [SerializeField] float _rotationStrength = 0.03f;

    private bool _ticked;

    void Start() {
      _tickIcon.transform.localScale = Vector3.zero;
      _ticked = false;
    }
      
    public void OnPointerDown(PointerEventData eventData) {
      Animate("scale",
        0.75f,
        () => { return transform.localScale.x; },
        (value) => { transform.localScale = new Vector3(value, value, value); }
      );
      Nudge("rotation",
        Random.value * _rotationStrength * 2 - _rotationStrength,
        () => { return Mathf.Asin(transform.up.x) * Mathf.Rad2Deg; },
        (value) => { transform.rotation = Quaternion.Euler(0, 0, -value); }
      );
    }

    public void OnPointerUp(PointerEventData eventData) {
      _ticked = !_ticked;
      Animate("scale",
        1,
        () => { return transform.localScale.x; },
        (value) => { transform.localScale = new Vector3(value, value, value); }
      );
      Nudge("rotation",
        Random.value * _rotationStrength * 2 - _rotationStrength,
        () => { return Mathf.Asin(transform.up.x) * Mathf.Rad2Deg; },
        (value) => { transform.rotation = Quaternion.Euler(0, 0, -value); }
      );
      Animate("icon_scale",
        _ticked ? 1 : 0,
        () => { return _tickIcon.transform.localScale.x; },
        (value) => { _tickIcon.transform.localScale = value < 0 ? Vector3.zero : new Vector3(value, value, value); }
      );
    }
  }
}