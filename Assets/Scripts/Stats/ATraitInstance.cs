using System;

namespace Stats
{
    public interface ITraitInstance
    {
        public abstract void AddEffect(TraitEffect effect);
        public abstract void RemoveEffect(TraitEffect effect);
    }
}