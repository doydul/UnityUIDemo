using UnityEngine;

namespace UIDemo {

  public class PhysicsBasedMenuPanel : MonoBehaviour {
    [SerializeField] PhysicsBasedAnimator _animator;

    public Vector3 Facing { get; private set; }

    void Start() {
      Facing = transform.forward;
    }

    //

    public void Activate() {
        _animator.Face(transform);
    }
  }
}