using System;
using System.Collections.Generic;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public class StatInstance
    {
        private bool _isDirty;
        
        [SerializeField] private float _baseValue;
        [SerializeField] private float _calculatedValue;

        private Dictionary<TraitEffect, TraitModification> _modifications = new();

        public float BaseValue => _baseValue;

        public float ModifiedValue
        {
            get
            {
                if (_isDirty)
                {
                    RecalculateValue();
                }

                return _calculatedValue;
            }
        }

        public void AddEffect(TraitModification modif)
        {
            if (_modifications.ContainsKey(modif._parentEffect))
            {
                RemoveEffect(modif._parentEffect);
            }

            _isDirty = true;
            _modifications[modif._parentEffect] = modif;
        }

        public void RemoveEffect(TraitEffect effect)
        {
            if (_modifications.Remove(effect))
            {
                _isDirty = true;
            }
        }

        public bool HasEffect(TraitEffect effect)
        {
            return _modifications.ContainsKey(effect);
        }

        private void RecalculateValue()
        {
            _calculatedValue = _baseValue;

            foreach (var modification in _modifications)
            {
                _calculatedValue += CalculateEffect(modification.Value);
            }

            _isDirty = false;
        }

        private float CalculateEffect(TraitModification modif)
        {
            return modif._modification_type switch
            {
                TraitModificationType.Flat => modif._value,
                TraitModificationType.Relative => modif._value * _baseValue * 0.01f,
                _ => 0
            };
        }
    }
}
