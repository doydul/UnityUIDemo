using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;
using System.Collections.Generic;

namespace UIDemo {

  public class PhysicsBasedTextInput : PhysicsBasedUIElement {
    [SerializeField] Transform _textTransform;
    [SerializeField] TMP_Text _textElement;
    [SerializeField] float _rotationStrength = 5f;
    [SerializeField] float _scaleStrength = 0.03f;

    private string _currentText;

    void Start() {
      _currentText = "";
      TMPro_EventManager.TEXT_CHANGED_EVENT.Add(OnTextChanged);
    }

    void OnDestroy() {
      TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(OnTextChanged);
    }

    private void OnTextChanged(Object element) {
      if (element == _textElement && _textElement.text != _currentText) {
        _currentText = _textElement.text;
        Nudge("scale",
          _scaleStrength,
          () => { return transform.localScale.x; },
          (value) => { transform.localScale = new Vector3(value, value, value); }
        );
        Nudge("text_scale",
          -_scaleStrength * 2,
          () => { return _textTransform.localScale.x; },
          (value) => { _textTransform.localScale = new Vector3(value, value, value); }
        );
        Nudge("rotation",
          Random.value * _rotationStrength * 2 - _rotationStrength,
          () => { return Mathf.Asin(transform.up.x) * Mathf.Rad2Deg; },
          (value) => { transform.rotation = Quaternion.Euler(0, 0, -value); }
        );
      }
    }
  }
}