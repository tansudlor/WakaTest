using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerClickInputHandle : MonoBehaviour
{
    public Camera mainCamera;  // กล้องที่ใช้ในฉาก
    public NavMeshAgent playerAgent;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // ตรวจจับการคลิกซ้ายของเมาส์
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);  // ยิง Ray จากจุดที่คลิกบนหน้าจอ
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))  // ตรวจสอบว่ามีการชนวัตถุใด ๆ
            {
                playerAgent.SetDestination(hit.point);  // สั่งให้ตัวละครเดินไปยังจุดที่คลิก
            }
        }
    }
}

