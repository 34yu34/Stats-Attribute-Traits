using System;
using System.Collections.Generic;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public class ResourcesHandler
    {
        [SerializeField] private ResourceInstance[] _resources;

        public void AddEffect(TraitEffect effect)
        {
            foreach (var line in effect.EffectsLines)
            {
                if (line._traitsType != TraitsTypes.Resource) continue;
                
                GetResource(line._resource).AddEffect(new TraitModification(effect, line._modificationValue, line._modificationType));
            }
        }

        public void RemoveEffect(TraitEffect effect)
        {
            foreach (var line in effect.EffectsLines)
            {
                if (line._traitsType != TraitsTypes.Resource) continue;
                
                GetResource(line._resource).RemoveEffect(effect);
            }
        }

        public bool HasEffect(TraitEffect effect)
        {
            if (effect.EffectsLines.Count == 0)
            {
                return false;
            }

            return GetResource(effect.EffectsLines[0]._resource).HasEffect(effect);
        }

        public ResourceInstance GetResource(Resource resource)
        {
            return _resources[(int) resource];
        }
    }
}