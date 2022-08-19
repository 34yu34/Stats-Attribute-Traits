using UnityEngine;

namespace Stats
{
    public class Traits : MonoBehaviour
    {
        [SerializeField] private StatsHandler _stats;
        [SerializeField] private ResourcesHandler _resources;

        public StatInstance GetStat(Stat stat)
        {
            return _stats.GetStat(stat);
        }

        public ResourceInstance GetResource(Resource resource)
        {
            return _resources.GetResource(resource);
        }

        public void AddEffect(TraitEffect effect)
        {
            _stats.AddEffect(effect);
            _resources.AddEffect(effect);
        }

        public void RemoveEffect(TraitEffect effect)
        {
            _stats.RemoveEffect(effect);
            _resources.RemoveEffect(effect);
        }
    }
}