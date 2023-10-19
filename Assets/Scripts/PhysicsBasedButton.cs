using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UIDemo {

  [RequireComponent(typeof(Image))]
  public class PhysicsBasedButton : PhysicsBasedUIElement, IPointerClickHandler {
      
    public void OnPointerClick(PointerEventData eventData) {
      Animate("scale",
        2,
        () => { return transform.localScale.x; },
        (value) => { transform.localScale = new Vector3(value, value, value); }
      );
    }
  }
}