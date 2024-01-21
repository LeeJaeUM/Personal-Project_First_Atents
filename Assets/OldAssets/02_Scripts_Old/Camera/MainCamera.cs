using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform target; // 플레이어의 Transform을 저장할 변수
    public float smoothSpeed = 0.125f; // 카메라 이동에 사용할 부드러운 속도

    private void FixedUpdate()
    {
        if (target != null)
        {
            // 플레이어의 현재 위치를 가져와서 카메라를 해당 위치로 이동
            Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
