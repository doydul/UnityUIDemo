using UnityEngine;

namespace UIDemo {

  public class PhysicsBasedMenu : MonoBehaviour {
    private PhysicsBasedMenuPanel[] _panels;

    void Awake() {
      _panels = GetComponentsInChildren<PhysicsBasedMenuPanel>();
    }

    void Start() {
      ActivatePanel(3);
    }

    void Update() {
      if (Input.GetKeyDown("1")) {
        ActivatePanel(0);
      }
      if (Input.GetKeyDown("2")) {
        ActivatePanel(1);
      }
      if (Input.GetKeyDown("3")) {
        ActivatePanel(2);
      }
      if (Input.GetKeyDown("4")) {
        ActivatePanel(3);
      }
      if (Input.GetKeyDown("5")) {
        ActivatePanel(4);
      }
      if (Input.GetKeyDown("6")) {
        ActivatePanel(5);
      }
    }

    //

    public void DeactivateAll() {
      foreach (var panel in _panels) {
        panel.Deactivate();
      }
    }

    public void ActivatePanel(int panelIndex) {
      DeactivateAll();
      _panels[panelIndex].Activate();
    }
  }
}