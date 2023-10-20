using UnityEngine;

namespace UIDemo {

  [RequireComponent(typeof(Rigidbody))]
  public class PhysicsBasedAnimator : MonoBehaviour {
    [Tooltip("The strength of the force that resists rotation")]
    [SerializeField] float _rotationalDamping = 0.5f;

    [Tooltip("The strength of the torque that pushes the object torwards it's desired rotation")]
    [SerializeField] float _rotationalSpringForce = 0.5f;

    [Tooltip("The strength of the force that resists translation")]
    [SerializeField] float _translationalDamping = 0.5f;

    [Tooltip("The strength of the force that pushes the object torwards it's desired position")]
    [SerializeField] float _translationalSpringForce = 0.5f;

    [Tooltip("The amount of resistance applied to scale changes")]
    [SerializeField] float _scaleDamping = 0.05f;

    [Tooltip("The speed at which the scale will change to match the desired scale")]
    [SerializeField] float _scaleSpringForce = 0.02f;

    [Tooltip("The relative magnitude of the impulse applied when the object is 'nudged'")]
    [SerializeField] float _nudgeStrength = 1f;

    private Rigidbody _rigidbody;
    private Vector3 _desiredPosition;
    private Vector3 _desiredFacing;
    private Vector3 _desiredScale;
    private Vector3 _scaleVelocity;
    private Transform _facingTransform;
    private Vector3 _originalUp;
    private Vector3 _originalForward;

    private Quaternion _arcFromCurrentFacingToDesiredFacing => Quaternion.FromToRotation(transform.forward, _desiredFacing);
    private Vector3 _vectorFromCurrentPositionToDesiredPosition => _desiredPosition - transform.position;

    void Awake() {
      _desiredPosition = transform.position;
      _desiredFacing = transform.forward;
      _desiredScale = transform.localScale;
      _originalUp = transform.up;
      _originalForward = transform.forward;
      _rigidbody = GetComponent<Rigidbody>();
      _rigidbody.drag = _translationalDamping;
      _rigidbody.angularDrag = _rotationalDamping;
      _rigidbody.useGravity = false;

      transform.rotation = Random.rotation;
      transform.localScale = new Vector3(0, 0, 0);
    }

    void FixedUpdate() {
      if (_facingTransform != null) {
        ApplyTorque(_facingTransform.forward, _originalForward);
        ApplyTorque(_facingTransform.up, _originalUp);
      } else {
        ApplyTorque(transform.forward, _desiredFacing);
      }
      ApplyForce();
      AdjustScale();
    }

    private void ApplyTorque(Vector3 from, Vector3 to) {
      var cross = Vector3.Cross(from, to);
      var theta = Mathf.Asin(cross.magnitude * 0.99f);
      var angvel = cross.normalized * theta * _rotationalSpringForce;
      _rigidbody.AddTorque(angvel);
    }

    private void ApplyForce() {
      var delta = _desiredPosition - transform.position;
      _rigidbody.AddForce(delta * _translationalSpringForce);
    }

    private void AdjustScale() {
      var delta = _desiredScale - transform.localScale;
      _scaleVelocity += delta * _scaleSpringForce;
      _scaleVelocity *= 1 - _scaleDamping;
      transform.localScale += _scaleVelocity;
    }

    //

    public void Face(Vector3 newFacing) {
      _desiredFacing = newFacing.normalized;
      _facingTransform = null;
    }

    public void Face(Transform facingTransform) {
      _facingTransform = facingTransform;
    }

    public void Move(Vector3 newPosition) {
      _desiredPosition = newPosition;
    }

    public void Nudge(Vector3 nudgePosition, Vector3 nudgeDirection) {
      _rigidbody.AddForceAtPosition(nudgeDirection.normalized * _nudgeStrength, nudgePosition, ForceMode.Impulse);
    }
  }
}