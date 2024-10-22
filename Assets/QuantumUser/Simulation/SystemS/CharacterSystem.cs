namespace Quantum
{
    using Photon.Deterministic;
    using UnityEngine.Scripting;

    [Preserve]
    public unsafe class CharacterSystem : SystemMainThreadFilter<CharacterSystem.Filter>, ISignalOnComponentAdded<CharacterStats>
    {

        public override void Update(Frame f, ref Filter filter)
        {
           
            

        }

        public void OnAdded(Frame f, EntityRef entity, CharacterStats* component)
        {
            var spec = f.FindAsset(component->Spec);
            component->Power = spec.BasePower;
        }

        public struct Filter
        {
            public EntityRef Entity;
            public  CharacterStats* Stats;
        }
    }
}
