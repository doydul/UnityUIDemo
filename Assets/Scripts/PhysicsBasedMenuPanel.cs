using UnityEngine;

namespace UIDemo {

  public class PhysicsBasedMenuPanel : MonoBehaviour {
    [SerializeField] PhysicsBasedAnimator _animator;

    public Vector3 Facing { get; private set; }

    private DemoButton[] _buttons;

    void Awake() {
      Facing = transform.forward;
      _buttons = GetComponentsInChildren<DemoButton>();
    }

    //

    public void Activate() {
      _animator.Face(transform);
      foreach (var button in _buttons) {
        button.Enable();
      }
    }

    public void Deactivate() {
      foreach (var button in _buttons) {
        button.Disable();
      }
    }
  }
}