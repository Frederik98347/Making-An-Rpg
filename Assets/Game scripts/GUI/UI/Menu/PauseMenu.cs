using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;
    
    public GameObject PauseMenuUI;
    [SerializeField] UserCamera cam;
    RpgTools.PlayerClass.Player player;

    public string StartMenu = "StartMenu";

    private void Start()
    {
        player = FindObjectOfType<RpgTools.PlayerClass.Player>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && player.autoAttacking == false)
        {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        cam.allowMouseInputX = true;
        cam.allowMouseInputY = true;
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        cam.allowMouseInputX = false;
        cam.allowMouseInputY = false;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        SceneManager.LoadScene(StartMenu);
        Debug.Log("Loading menu");

    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}