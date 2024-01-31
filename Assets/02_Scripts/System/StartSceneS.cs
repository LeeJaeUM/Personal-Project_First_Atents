using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneS : MonoBehaviour
{
    public void StartGame()
    {
        // "GameScene"으로 씬 전환
        SceneManager.LoadScene("02_StageSelect");
    }

    public void ExitGame()
    {
        // 게임 종료
        Application.Quit();
    }
}
