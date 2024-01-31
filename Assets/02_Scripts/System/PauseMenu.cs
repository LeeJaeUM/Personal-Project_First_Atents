using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // 정적 변수를 사용하여 인스턴스를 저장합니다.
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
    
    // PauseMenu 캔버스를 'DontDestroyOnLoad'로 지정
    private void Start()
    {
        pauseMenuObj = transform.GetChild(0).gameObject;
    }


    // 이 함수는 매 프레임마다 호출됩니다.
    void Update()
    {
        // 'esc' 키를 누르면 일시 정지 상태를 변경합니다.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    // 일시 정지 상태를 전환하는 함수
    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            // 일시 정지 상태일 때
            ShowPauseMenu();
            Time.timeScale = 0f; // 시간 흐름을 멈춥니다.
        }
        else
        {
            // 일시 정지 상태가 아닐 때
            HidePauseMenu();
            Time.timeScale = 1f; // 시간 흐름을 복원합니다.
        }
    }

    // PauseMenu를 활성화하는 함수
    void ShowPauseMenu()
    {
        // 캔버스를 보이게 만듭니다.
        pauseMenuObj.SetActive(true);
    }

    // PauseMenu를 비활성화하는 함수
    void HidePauseMenu()
    {
        // 캔버스를 숨깁니다.
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

    // 'Exit' 버튼이 클릭될 때 호출되는 함수
    public void ExitGame()
    {
        // 게임 종료
        Application.Quit();
    }
}
