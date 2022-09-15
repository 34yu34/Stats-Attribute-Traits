using System.Collections.Generic;
using UnityEngine;

namespace Stats
{
    [CreateAssetMenu(fileName = "TraitEffect", menuName = "Stats/TraitEffect", order = 1)]
    public class TraitEffect : ScriptableObject
    {
        public List<TraitEffectLine> EffectsLines => _effectLines;
        [SerializeField] private List<TraitEffectLine> _effectLines;

    }
}