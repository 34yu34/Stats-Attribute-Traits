using System;
using Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Test
{
    public class StatUI : MonoBehaviour
    {
        [SerializeField] private Stat _stat;
        [SerializeField] private Resource _resource;

        [SerializeField] private Traits _observed;
        [SerializeField] private TraitEffect _effectToAdd;

        [SerializeField] private TextMeshProUGUI _statText;
        [SerializeField] private TextMeshProUGUI _resourceText;
        [SerializeField] private TextMeshProUGUI _hasEffectText;

        public void Update()
        {
            var stat = _observed.GetStat(_stat);
            var resource = _observed.GetResource(_resource);
            _statText.text = $"Stat - {stat.BaseValue}/{stat.ModifiedValue}";
            _resourceText.text = $"Resource - {resource.CurrentValue}/{resource.ModifiedMaxValue} - ({resource.BaseMaxValue})";
            _hasEffectText.text = $"Has Effect : {_observed.HasEffect(_effectToAdd)}";
        }
        
        public void AddEffect()
        {
            _observed.AddEffect(_effectToAdd);
        }

        public void RemoveEffect()
        {
            _observed.RemoveEffect(_effectToAdd);
        }
    }
}