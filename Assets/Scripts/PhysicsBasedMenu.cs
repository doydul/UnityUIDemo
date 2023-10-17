using UnityEngine;

namespace UIDemo {

  public class PhysicsBasedMenu : MonoBehaviour {
    private PhysicsBasedMenuPanel[] _panels;

    void Start() {
      _panels = GetComponentsInChildren<PhysicsBasedMenuPanel>();
    }

    void Update() {
      if (Input.GetKeyDown("1")) {
        _panels[0].Activate();
      }
      if (Input.GetKeyDown("2")) {
        _panels[1].Activate();
      }
      if (Input.GetKeyDown("3")) {
        _panels[2].Activate();
      }
      if (Input.GetKeyDown("4")) {
        _panels[3].Activate();
      }
      if (Input.GetKeyDown("5")) {
        _panels[4].Activate();
      }
      if (Input.GetKeyDown("6")) {
        _panels[5].Activate();
      }
    }
  }
}