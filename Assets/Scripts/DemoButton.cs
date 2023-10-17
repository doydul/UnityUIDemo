using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

namespace UIDemo {

  [RequireComponent(typeof(Image))]
  public class DemoButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler {
    [SerializeField] TMP_Text _text;
    [SerializeField] Color _defaultColor;
    [SerializeField] Color _highlightColor;
    [SerializeField] Color _selectColor;
    [SerializeField] Color _disabledColor;
    [SerializeField] bool _disabled;
    [SerializeField] UnityEvent _onClick;
    
    private Image _image;
    private bool _hovering;
    private bool _clicking;

    void Start() {
      _image = GetComponent<Image>();
      SetColor();
    }

    private void SetColor() {
      var color = _defaultColor;
      if (_disabled) {
        color = _disabledColor;
      } else {
        if (_clicking) {
          color = _selectColor;
        } else if (_hovering) {
          color = _highlightColor;
        }
      }
      _image.color = color;
      _text.color = color;
    }

    //

    public void OnPointerEnter(PointerEventData eventData) {
      _hovering = true;
      SetColor();
    }

    public void OnPointerExit(PointerEventData eventData) {
      _hovering = false;
      SetColor();
    }

    public void OnPointerDown(PointerEventData eventData) {
      _clicking = true;
      SetColor();
    }

    public void OnPointerUp(PointerEventData eventData) {
      _clicking = false;
      SetColor();
    }

    public void OnPointerClick(PointerEventData eventData) {
      if (!_disabled) _onClick.Invoke();
    }

    //

    public void Disable() {
      _disabled = true;
      SetColor();
    }

    public void Enable() {
      _disabled = false;
      SetColor();
    }
  }
}