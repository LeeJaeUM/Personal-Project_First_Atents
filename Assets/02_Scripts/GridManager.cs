using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Material lineMaterial; // 라인의 머티리얼
    public Color lineColor = Color.white; // 라인의 색상
    public int gridSize = 3; // 격자의 크기
    public float cellSize = 1.0f; // 셀의 크기

    LineRenderer lineRenderer;

    void Start()
    {
        // 라인 렌더러 초기화
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material = lineMaterial;
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
        lineRenderer.startWidth = 0.05f; // 라인의 시작 너비
        lineRenderer.endWidth = 0.05f; // 라인의 끝 너비

        // 격자 무늬 그리기
        DrawGridLines(lineRenderer);
    }

    void DrawGridLines(LineRenderer lineRenderer)
    {
        int lineCount = (gridSize + 1) * 2 * 2; // 격자 라인과 세로 라인, 가로 라인, 총 4배

        // 라인의 점 개수 설정
        lineRenderer.positionCount = lineCount;

        float halfGridSize = gridSize * cellSize / 2;

        // 가로 라인 그리기
        for (int i = 0; i <= gridSize; i++)
        {
            float yPos = i * cellSize - halfGridSize;

            lineRenderer.SetPosition(i * 2, new Vector3(-halfGridSize, yPos, 0));
            lineRenderer.SetPosition(i * 2 + 1, new Vector3(halfGridSize, yPos, 0));
        }

        // 세로 라인 그리기
        for (int i = 0; i <= gridSize; i++)
        {
            float xPos = i * cellSize - halfGridSize;

            lineRenderer.SetPosition((gridSize + 1) * 2 + i * 2, new Vector3(xPos, -halfGridSize, 0));
            lineRenderer.SetPosition((gridSize + 1) * 2 + i * 2 + 1, new Vector3(xPos, halfGridSize, 0));
        }

        // 꼭짓점을 중복으로 그리지 않도록 수정
        lineRenderer.loop = false;
    }
}
