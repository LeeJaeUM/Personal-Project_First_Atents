using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // ���� ������ ����Ͽ� �ν��Ͻ��� �����մϴ�.
    //private static PauseMenu instance;

    private void Awake()
    {
        var obj = FindObjectsOfType<PauseMenu>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private bool isPaused = false;
    private GameObject pauseMenuObj;
    
    // PauseMenu ĵ������ 'DontDestroyOnLoad'�� ����
    private void Start()
    {
        pauseMenuObj = transform.GetChild(0).gameObject;
    }


    // �� �Լ��� �� �����Ӹ��� ȣ��˴ϴ�.
    void Update()
    {
        // 'esc' Ű�� ������ �Ͻ� ���� ���¸� �����մϴ�.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    // �Ͻ� ���� ���¸� ��ȯ�ϴ� �Լ�
    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            // �Ͻ� ���� ������ ��
            ShowPauseMenu();
            Time.timeScale = 0f; // �ð� �帧�� ����ϴ�.
        }
        else
        {
            // �Ͻ� ���� ���°� �ƴ� ��
            HidePauseMenu();
            Time.timeScale = 1f; // �ð� �帧�� �����մϴ�.
        }
    }

    // PauseMenu�� Ȱ��ȭ�ϴ� �Լ�
    void ShowPauseMenu()
    {
        // ĵ������ ���̰� ����ϴ�.
        pauseMenuObj.SetActive(true);
    }

    // PauseMenu�� ��Ȱ��ȭ�ϴ� �Լ�
    void HidePauseMenu()
    {
        // ĵ������ ����ϴ�.
        pauseMenuObj.SetActive(false);
    }

    public void ContinueGame()
    {
        TogglePause();
    }

    public void SelectScene()
    {
        SceneManager.LoadScene("StageSelect");
        TogglePause();
    }

    // 'Exit' ��ư�� Ŭ���� �� ȣ��Ǵ� �Լ�
    public void ExitGame()
    {
        // ���� ����
        Application.Quit();
    }
}
