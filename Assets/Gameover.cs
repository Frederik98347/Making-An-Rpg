using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Gameover script, calls when game is lost
/// </summary>
namespace RpgTools.GameOver
{
    [RequireComponent(typeof(AudioSource))]
    public class Gameover : MonoBehaviour
    {

        public GameObject GameOverMenuUI;
        public UserCamera cam;
        public AudioClip GameOverSound;
        [SerializeField]AudioSource Audio;

        RpgTools.PlayerClass.Player player;

        // Use this for initialization
        void Start()
        {
            player = FindObjectOfType<PlayerClass.Player>();
            Audio = GetComponent<AudioSource>();

            //set Gameover menu to be inactive
            GameOverMenuUI.SetActive(false);
        }

        // Update is called once per frame
        private void Update()
        {
            // if player is dead = gameover
            // need to implement family & check if all family is dead aswell
            if (player.state == PlayerClass.Player.State.DEAD)
            {
                GameOver();
            }
        }

        void GameOver()
        {
            AudioManger.instance.GetComponent<AudioSource>().Stop();
            Audio.clip = GameOverSound;
            Audio.Play();
            GameOverMenuUI.SetActive(true);
            Time.timeScale = 0f;

            cam.allowMouseInputX = false;
            cam.allowMouseInputY = false;
        }

        public void LoadMenu()
        {
            Time.timeScale = 1f;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            SceneManager.LoadScene(0);
            Debug.Log("Loading menu");

        }

        public void QuitGame()
        {
            Debug.Log("Quitting Game");
            Application.Quit();
        }
    }
}