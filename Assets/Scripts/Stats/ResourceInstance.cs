using System;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public class ResourceInstance
    {
        [SerializeField] private float _initialValue;
        [SerializeField] private float _currentValue;
        [SerializeField] private StatInstance _maxValue;

        public float CurrentValue
        {
            get => _currentValue;
            set => ClampCurrentValue(value);
        }

        
        public void AddEffect(TraitModification modif)
        {
            _maxValue.AddEffect(modif);

            ClampCurrentValue(CurrentValue);

        }

        public void RemoveEffect(TraitEffect effect)
        {
            _maxValue.RemoveEffect(effect);

            CurrentValue = _currentValue;
        }

        private void ClampCurrentValue(float newValue)
        {
            _currentValue = Mathf.Clamp(newValue, 0f, _maxValue.ModifiedValue);
        }
    }
}