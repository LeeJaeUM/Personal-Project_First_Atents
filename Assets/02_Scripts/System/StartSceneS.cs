using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneS : MonoBehaviour
{
    public void StartGame()
    {
        // "GameScene"으로 씬 전환
        SceneManager.LoadScene("StageSelect");
    }

    public void ExitGame()
    {
        // 게임 종료
        Application.Quit();
        AudioManager.instance.PlayBgm(false);
    }

    private void Start()
    {
        AudioManager.instance.PlayBgm(true);
    }
}
