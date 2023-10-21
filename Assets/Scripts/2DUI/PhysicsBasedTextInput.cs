using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace UIDemo {

  [RequireComponent(typeof(Image))]
  public class PhysicsBasedTextInput : PhysicsBasedUIElement {
    [SerializeField] TMP_Text text;
  }
}