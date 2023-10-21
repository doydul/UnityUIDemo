using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

namespace UIDemo {

  public abstract class PhysicsBasedUIElement : UIBehaviour {
    [SerializeField] protected float _springForce = 0.1f;
    [SerializeField] protected float _damping = 0.1f;

    private Dictionary<string, AnimatingAttribute> _animatingAttributes;

    void Awake() {
      _animatingAttributes = new Dictionary<string, AnimatingAttribute>();
    }

    void FixedUpdate() {
      foreach (var animAttr in _animatingAttributes.Values) {
        animAttr.Update();
      }
    }

    private class AnimatingAttribute {
      private float _springForce;
      private float _damping;
      private float _desiredValue;
      private float _velocity;
      private Func<float> _currentValueCallback;
      private Action<float> _changeValueCallback;

      public AnimatingAttribute(float desiredValue, float springForce, float damping, Func<float> currentValueCallback, Action<float> changeValueCallback) {
        _desiredValue = desiredValue;
        _springForce = springForce;
        _damping = damping;
        _currentValueCallback = currentValueCallback;
        _changeValueCallback = changeValueCallback;
      }

      public float GetVelocity() {
        return _velocity;
      }

      public void SetVelocity(float value) {
        _velocity = value;
      }

      public void Update() {
        var delta = _desiredValue - _currentValueCallback();
        _velocity += delta * _springForce;
        _velocity *= 1 - _damping;
        _changeValueCallback(_currentValueCallback() + _velocity);
      }

      public void Nudge(float impulse) {
        _velocity += impulse;
      }

      public void AdjustDesiredValue(float value) {
        _desiredValue = value;
      }
    }

    //

    protected void Animate(string attrName, float desiredValue, Func<float> currentValueCallback, Action<float> changeValueCallback, float springForce = 0, float damping = 0) {
      if (!_animatingAttributes.ContainsKey(attrName)) {
        _animatingAttributes[attrName] = new AnimatingAttribute(
          desiredValue: desiredValue,
          springForce: springForce <= 0 ? _springForce : springForce,
          damping: damping <= 0 ? _damping : damping,
          currentValueCallback: currentValueCallback,
          changeValueCallback: changeValueCallback
        );
      } else {
        _animatingAttributes[attrName].AdjustDesiredValue(desiredValue);
      }
    }

    protected void Nudge(string attrName, float nudgeStrength, Func<float> currentValueCallback, Action<float> changeValueCallback, float springForce = 0, float damping = 0) {
      if (!_animatingAttributes.ContainsKey(attrName)) {
        _animatingAttributes[attrName] = new AnimatingAttribute(
          desiredValue: currentValueCallback(),
          springForce: springForce <= 0 ? _springForce : springForce,
          damping: damping <= 0 ? _damping : damping,
          currentValueCallback: currentValueCallback,
          changeValueCallback: changeValueCallback
        );
      }
      _animatingAttributes[attrName].Nudge(nudgeStrength);
    }

    protected float GetVelocity(string attrName) {
      if (_animatingAttributes.ContainsKey(attrName)) {
        return _animatingAttributes[attrName].GetVelocity();
      }
      return 0;
    }

    protected void SetVelocity(string attrName, float value) {
      if (_animatingAttributes.ContainsKey(attrName)) {
        _animatingAttributes[attrName].SetVelocity(value);
      }
    }
  }
}