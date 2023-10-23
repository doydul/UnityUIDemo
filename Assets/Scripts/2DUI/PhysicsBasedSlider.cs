using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UIDemo {

  [RequireComponent(typeof(Image))]
  public class PhysicsBasedSlider : PhysicsBasedUIElement, IBeginDragHandler, IDragHandler, IEndDragHandler {
    [SerializeField] Transform _leftBound;
    [SerializeField] Transform _rightBound;
    [SerializeField] float _scaleStrength = -0.03f;
    [SerializeField] float _rotationStrength = 0.1f;

    private Vector2 _mouseDragStartPos;
    private float _sliderStartPos;
    private float _sliderPosLastFrame;

    void Start() {
      _sliderStartPos = transform.localPosition.x;
      _sliderPosLastFrame = transform.localPosition.x;
    }

    void Update() {
      if (transform.localPosition.x < _leftBound.localPosition.x) {
        var tmp = transform.localPosition;
        tmp.x = _leftBound.localPosition.x;
        transform.localPosition = tmp;
        if (GetVelocity("local_position_x") < 0) {
          SetVelocity("local_position_x", -GetVelocity("local_position_x"));
          Nudge("rotation",
            GetVelocity("local_position_x") * _rotationStrength * 10,
            () => { return Mathf.Asin(transform.up.x) * Mathf.Rad2Deg; },
            (value) => { transform.rotation = Quaternion.Euler(0, 0, -value); }
          );
        }
      }
      if (transform.localPosition.x > _rightBound.localPosition.x) {
        var tmp = transform.localPosition;
        tmp.x = _rightBound.localPosition.x;
        transform.localPosition = tmp;
        if (GetVelocity("local_position_x") > 0) {
          SetVelocity("local_position_x", -GetVelocity("local_position_x"));
          Nudge("rotation",
            GetVelocity("local_position_x") * _rotationStrength * 10,
            () => { return Mathf.Asin(transform.up.x) * Mathf.Rad2Deg; },
            (value) => { transform.rotation = Quaternion.Euler(0, 0, -value); }
          );
        }
      }
    }

    //

    public void OnBeginDrag(PointerEventData eventData) {
      _mouseDragStartPos = eventData.position;
      _sliderStartPos = transform.localPosition.x;
      _sliderPosLastFrame = transform.localPosition.x;
    }

    public void OnDrag(PointerEventData eventData) {
      var posX = Mathf.Clamp(_sliderStartPos + (eventData.position.x - _mouseDragStartPos.x), _leftBound.localPosition.x, _rightBound.localPosition.x);
      Animate("local_position_x",
        posX,
        () => { return transform.localPosition.x; },
        (value) => { var tmp = transform.localPosition; tmp.x = value; transform.localPosition = tmp; }
      );
      Nudge("rotation",
        (_sliderPosLastFrame - posX) * _rotationStrength,
        () => { return Mathf.Asin(transform.up.x) * Mathf.Rad2Deg; },
        (value) => { transform.rotation = Quaternion.Euler(0, 0, -value); }
      );
      _sliderPosLastFrame = posX;
    }

    public void OnEndDrag(PointerEventData eventData) {
      // maybe i dont need this?
    }
  }
}