using System;
using System.Collections.Generic;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public class StatsHandler
    {
        [SerializeField] private StatInstance[] _stats;

        public void AddEffect(TraitEffect effect)
        {
            foreach (var line in effect.EffectsLines)
            {
                if (line._traitsType != TraitsTypes.Stat)
                {
                    continue;
                }
                
                GetStat(line._stat).AddEffect(new TraitModification(effect, line._modificationValue, line._modificationType));
            }
        }

        public void RemoveEffect(TraitEffect effect)
        {
            foreach (var line in effect.EffectsLines)
            {
                if (line._traitsType != TraitsTypes.Stat)
                {
                    continue;
                }
                
                GetStat(line._stat).RemoveEffect(effect);
            }
        }

        public bool HasEffect(TraitEffect effect)
        {
            if (effect.EffectsLines.Count == 0)
            {
                return false;
            }

            return GetStat(effect.EffectsLines[0]._stat).HasEffect(effect);
        }

        public StatInstance this[Stat stat] => GetStat(stat);

        public StatInstance GetStat(Stat stat)
        {
            return _stats[(int) stat];
        }
    }
}