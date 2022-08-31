using UnityEngine;

namespace Stats
{
    public class Traits : MonoBehaviour
    {
        [SerializeField] private StatsHandler _statsHandler;
        [SerializeField] private ResourcesHandler _resourcesHandler;

        public StatInstance GetStat(Stat stat)
        {
            return _statsHandler.GetStat(stat);
        }

        public ResourceInstance GetResource(Resource resource)
        {
            return _resourcesHandler.GetResource(resource);
        }

        public ResourceInstance Get(Resource resource)
        {
            return _resourcesHandler.GetResource(resource);
        }

        public StatInstance Get(Stat stat)
        {
            return _statsHandler.GetStat(stat);
        }
        
        public ResourceInstance this[Resource resource] => Get(resource);
        public StatInstance this[Stat stat] => Get(stat);

        public void AddEffect(TraitEffect effect)
        {
            _statsHandler.AddEffect(effect);
            _resourcesHandler.AddEffect(effect);
        }

        public void RemoveEffect(TraitEffect effect)
        {
            _statsHandler.RemoveEffect(effect);
            _resourcesHandler.RemoveEffect(effect);
        }

        public bool HasEffect(TraitEffect effect)
        {
            if (effect.EffectsLines.Count == 0)
            {
                return false;
            }

            return effect.EffectsLines[0]._traitsType == TraitsTypes.Stat
                    ? _statsHandler.HasEffect(effect) 
                    : _resourcesHandler.HasEffect(effect);
        }
    }
}