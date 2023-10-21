using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UIDemo {

  [RequireComponent(typeof(Image))]
  public class PhysicsBasedButton : PhysicsBasedUIElement, IPointerDownHandler, IPointerUpHandler {
    [SerializeField] float _scaleStrength = -0.03f;
    [SerializeField] float _rotationStrength = 0.03f;
      
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
    }
  }
}