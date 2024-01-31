using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectS : MonoBehaviour
{
    public void Stage_1()
    {
        // "Stage_1"으로 씬 전환
        SceneManager.LoadScene("01_Stage");
    }
}
