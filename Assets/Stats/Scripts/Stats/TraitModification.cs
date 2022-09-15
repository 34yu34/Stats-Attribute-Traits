using System;

namespace Stats
{
    [Serializable]
    public struct TraitModification
    {
        public TraitEffect _parentEffect;
        public float _value;
        public TraitModificationType _modification_type;

        public TraitModification(TraitEffect parentEffect, float value, TraitModificationType modificationType)
        {
            _parentEffect = parentEffect;
            _value = value;
            _modification_type = modificationType;
        }
    }
}