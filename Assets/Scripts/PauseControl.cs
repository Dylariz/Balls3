using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseControl : MonoBehaviour
{
    public static bool gameIsPaused;
    public TextMeshProUGUI pauseText;
    public AudioSource cameraAudio;

    private void Start()
    {
        pauseText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.P))
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
            gameIsPaused = false;
            PauseGame();
        }
    }
    void PauseGame()
    {
        if(gameIsPaused)
        {
            Time.timeScale = 0f;
            pauseText.gameObject.SetActive(true);
            cameraAudio.Pause();
        }
        else 
        {
            Time.timeScale = 1;
            pauseText.gameObject.SetActive(false);
            cameraAudio.UnPause();
        }
    }
}
