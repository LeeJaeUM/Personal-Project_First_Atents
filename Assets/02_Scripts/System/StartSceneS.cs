using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneS : MonoBehaviour
{
    public void StartGame()
    {
        // "GameScene"���� �� ��ȯ
        SceneManager.LoadScene("02_StageSelect");
    }

    public void ExitGame()
    {
        // ���� ����
        Application.Quit();
    }
}
