using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectS : MonoBehaviour
{
    public void Stage_1()
    {
        // "Stage_1"���� �� ��ȯ
        SceneManager.LoadScene("01_Stage");
    }
}
