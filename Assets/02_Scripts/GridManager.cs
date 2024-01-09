using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Material lineMaterial; // ������ ��Ƽ����
    public Color lineColor = Color.white; // ������ ����
    public int gridSize = 3; // ������ ũ��
    public float cellSize = 1.0f; // ���� ũ��

    LineRenderer lineRenderer;

    void Start()
    {
        // ���� ������ �ʱ�ȭ
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material = lineMaterial;
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
        lineRenderer.startWidth = 0.05f; // ������ ���� �ʺ�
        lineRenderer.endWidth = 0.05f; // ������ �� �ʺ�

        // ���� ���� �׸���
        DrawGridLines(lineRenderer);
    }

    void DrawGridLines(LineRenderer lineRenderer)
    {
        int lineCount = (gridSize + 1) * 2 * 2; // ���� ���ΰ� ���� ����, ���� ����, �� 4��

        // ������ �� ���� ����
        lineRenderer.positionCount = lineCount;

        float halfGridSize = gridSize * cellSize / 2;

        // ���� ���� �׸���
        for (int i = 0; i <= gridSize; i++)
        {
            float yPos = i * cellSize - halfGridSize;

            lineRenderer.SetPosition(i * 2, new Vector3(-halfGridSize, yPos, 0));
            lineRenderer.SetPosition(i * 2 + 1, new Vector3(halfGridSize, yPos, 0));
        }

        // ���� ���� �׸���
        for (int i = 0; i <= gridSize; i++)
        {
            float xPos = i * cellSize - halfGridSize;

            lineRenderer.SetPosition((gridSize + 1) * 2 + i * 2, new Vector3(xPos, -halfGridSize, 0));
            lineRenderer.SetPosition((gridSize + 1) * 2 + i * 2 + 1, new Vector3(xPos, halfGridSize, 0));
        }

        // �������� �ߺ����� �׸��� �ʵ��� ����
        lineRenderer.loop = false;
    }
}
