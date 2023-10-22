using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;
using System.Collections.Generic;

namespace UIDemo {

  public class PhysicsBasedTextInput : PhysicsBasedUIElement {
    [SerializeField] TMP_Text _textElement;

    private string _currentText;
    private List<AnimatableCharacter> _charAnims;
    private TMP_TextInfo _textInfo;
    private TMP_MeshInfo[] _cachedMeshInfo;
    private bool _hasTextChanged;

    void Start() {
      _currentText = "";
      _charAnims = new List<AnimatableCharacter>();
      TMPro_EventManager.TEXT_CHANGED_EVENT.Add(OnTextChanged);

      _textElement.ForceMeshUpdate();
      _textInfo = _textElement.textInfo;
      _cachedMeshInfo = _textInfo.CopyMeshInfoVertexData();
    }

    void OnDestroy() {
      TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(OnTextChanged);
    }

    void Update() {
      if (_hasTextChanged) {
        _cachedMeshInfo = _textInfo.CopyMeshInfoVertexData();
        for (int i = 0; i < _textInfo.characterCount; i++) {
          var charInfo = _textInfo.characterInfo[i];
          int materialIndex = charInfo.materialReferenceIndex;
          int vertexIndex = charInfo.vertexIndex;
          Debug.Log($"i: {i}");
          Debug.Log($"vertexIndex: {vertexIndex}");

          var charAnim = new AnimatableCharacter(vertexIndex);
          _charAnims.Add(charAnim);

          // Animate($"character_{i}_scale",
          //   1,
          //   charAnim.Get,
          //   charAnim.Set
          // );
        }
        _currentText = _textElement.text;
        _hasTextChanged = false;
      }
    }

    private void OnTextChanged(Object element) {
      if (element == _textElement && _textElement.text != _currentText) {
        _hasTextChanged = true;
      }
    }

    private class AnimatableCharacter {
      public int firstVertexIndex { get; private set; }
      public int currentScale { get; set; }

      public AnimatableCharacter(int firstVertexIndex) {
        firstVertexIndex = firstVertexIndex;
        currentScale = 0;
      }

      // public float Get() {
      //   return _currentScale;
      // }

      // public void Set(float value) {
      //   _currentScale = value;
      //   for (int i = 0; i < _originalVertices.Length; i++) {
      //     var distFromCentre = _originalVertices[i] - _centre;
      //     _meshVerticesRef[_firstVertexIndex + i] = _centre + distFromCentre * _currentScale;
      //   }
      // }
    }
  }
}