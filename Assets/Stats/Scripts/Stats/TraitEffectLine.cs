using System;

namespace Stats
{
    [Serializable]
    public struct TraitEffectLine
    {
        public TraitsTypes _traitsType;
        
        public Stat _stat;
        public Resource _resource;
        
        public float _modificationValue;
        public TraitModificationType _modificationType;
    }
}