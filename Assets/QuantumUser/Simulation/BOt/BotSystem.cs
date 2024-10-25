namespace Quantum
{
    using Photon.Deterministic;
    using UnityEngine;
    using UnityEngine.Scripting;
    using static UnityEngine.EventSystems.EventTrigger;

    [Preserve]
    public unsafe class BotSystem : SystemMainThreadFilter<BotSystem.Filter>
    {
        

        public override void Update(Frame f, ref Filter filter)
        {
            Debug.Log("Haaaa");

            var bot = f.Unsafe.GetPointer<Bot>(filter.Entity);
            if(bot == null)
            {
                Debug.Log("botNull");
                return;
            }

            /*if (f.Number%500 == 0)
            {
                Debug.Log("Haaaa1");
                FPVector3 fpVector3 = new FPVector3(f.RNG->Next(-8,8),FP._0_33 - FP._0_04 , f.RNG->Next(-9, 9));
                Debug.Log("fpVector3 " + fpVector3);
                var navMeshPathfinder = f.Unsafe.GetPointer<NavMeshPathfinder>(filter.Entity);
                var navMeshAsset = f.FindAsset<NavMesh>(navMeshPathfinder->NavMeshGuid);
                Debug.Log("navMeshAsset " + navMeshAsset);
                navMeshPathfinder->SetTarget(f, fpVector3, navMeshAsset);
                navMeshPathfinder->ForceRepath(f);
            }*/
           
            //f.Unsafe.GetPointer<>
            //f.Unsafe.GetPointer<Bot>(bot);

        }

        public struct Filter
        {
            public EntityRef Entity;
            public Transform2D* Transform;
            public Bot* Bot;


        }
    }
}
