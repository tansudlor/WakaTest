
using Photon.Deterministic;
using Quantum;
using UnityEngine;


public unsafe class DebugPath : MonoBehaviour
{
    public QuantumRunner runner;



    void OnDrawGizmos()
    {
        runner = QuantumRunner.Default;
        if (runner == null || !runner.IsRunning)
            return;

        var f = runner.Game.Frames.Predicted;

        foreach (var pair in f.GetComponentIterator<NavMeshPathfinder>())
        {

            var navMeshPathfinder = f.Unsafe.GetPointer<NavMeshPathfinder>(pair.Entity);

            if (navMeshPathfinder->WaypointCount > 1) // ตรวจสอบว่ามีเส้นทางหลายจุดหรือไม่
            {
                for (int i = 0; i < navMeshPathfinder->WaypointCount - 1; i++)
                {
                    FPVector3 currentWaypoint = navMeshPathfinder->GetWaypoint(f, i);
                    FPVector3 nextWaypoint = navMeshPathfinder->GetWaypoint(f, i + 1);

                    // แปลงจาก FPVector3 เป็น Vector3
                    Vector3 start = currentWaypoint.ToUnityVector3();
                    Vector3 end = nextWaypoint.ToUnityVector3();

                    // วาดเส้นระหว่างทางแต่ละจุด
                    Debug.DrawLine(start, end, Color.green);
                }
            }
        }
    }
}


