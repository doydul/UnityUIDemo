using UnityEngine;

namespace UIDemo {

  public class SkyboxRotator : MonoBehaviour {
    [Tooltip("The axis of rotation")]
    [SerializeField] Vector3 _rotationAxis;
  
    [Tooltip("The speed of rotation, measured in degrees per second")]
    [SerializeField] float _rotationSpeed;

    void Update() {
      transform.rotation = Quaternion.AngleAxis(_rotationSpeed * Time.deltaTime, _rotationAxis) * transform.rotation;
    }
  }
}