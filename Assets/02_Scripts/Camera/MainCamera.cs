using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform target; // �÷��̾��� Transform�� ������ ����
    public float smoothSpeed = 0.125f; // ī�޶� �̵��� ����� �ε巯�� �ӵ�

    private void FixedUpdate()
    {
        if (target != null)
        {
            // �÷��̾��� ���� ��ġ�� �����ͼ� ī�޶� �ش� ��ġ�� �̵�
            Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
