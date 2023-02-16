using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorial;
    public static int Level { get; private set; }
    public static bool IsCutScene { get; private set; }

    private bool isTutorial;
    private bool isPause;

    private void Awake()
    {
        EventController.OnTimeIsOverEvent += PausePlay;
        EventController.OnTutorialEvent += PausePlay;
    }

    private void Start()
    {
        isPause = false;
        IsCutScene = true;
        //PlayerPrefs.DeleteAll();
        isTutorial = PlayerPrefs.GetInt("Tutorial", 1) == 1;
        if (SceneManager.GetActiveScene().buildIndex == 1 && isTutorial)
        {
            Instantiate(tutorial);
            PlayerPrefs.SetInt("Tutorial", 0);
        }
    }

    public void PausePlay()
    {
        isPause = !isPause;
        if (isPause) 
        {
            EventController.OnPause();
            Time.timeScale = 0; 
        }
        else 
        {
            EventController.OnPlay();
            Time.timeScale = 1; 
        }
    }

    public void NextLevel()
    {
        EventController.OnLevelEnd();
        Level++;
        EventController.ResetEvents();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartGame()
    {
        Level = 0;
        EventController.ResetEvents();
        SceneManager.LoadScene(1);
    }

    public void Restart()
    {
        Level = 0;
        EventController.ResetEvents();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToMenu()
    {
        EventController.ResetEvents();
        SceneManager.LoadScene(0);
    }

    public void CutSceneOn()
    {
        IsCutScene = true;
    }

    public void CutSceneOff()
    {
        IsCutScene = false;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
