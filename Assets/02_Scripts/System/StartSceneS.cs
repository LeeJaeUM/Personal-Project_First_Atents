using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneS : MonoBehaviour
{
    public void StartGame()
    {
        // "GameScene"���� �� ��ȯ
        SceneManager.LoadScene("StageSelect");
    }

    public void ExitGame()
    {
        // ���� ����
        Application.Quit();
        AudioManager.instance.PlayBgm(false);
    }

    private void Start()
    {
        AudioManager.instance.PlayBgm(true);
    }
}
