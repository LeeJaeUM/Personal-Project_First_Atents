using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerWin : MonoBehaviour
{
    public GameObject testObj; 
    private void Start()
    {
        EnemyManager.OnEnemyAllDie += PlayerWin_UI;
        DisableUI();
    }
    private void OnDestroy()
    {
        EnemyManager.OnEnemyAllDie -= PlayerWin_UI;
    }
    public void PlayerWin_UI()
    {
        testObj.SetActive(true);
    }

    public void SelectScene()
    {
        SceneManager.LoadScene("StageSelect");
    }

    public void DisableUI()
    {
        testObj.SetActive(false);
    }
}
