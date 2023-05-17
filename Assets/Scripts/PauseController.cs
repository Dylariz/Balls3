using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    private static bool GameIsPaused { get; set; }
    public AudioSource cameraAudio;

    private List<GameObject> pauseMenuObjects;

    private void Start()
    {
        pauseMenuObjects = GetComponentsInChildren<Transform>().Select(x => x.gameObject).ToList();
        pauseMenuObjects.Remove(gameObject);
        GameIsPaused = false;
        
        ToggleMenuVisibility(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame(!GameIsPaused);
        }
    }
    public void PauseGame(bool isPause)
    {
        if(isPause)
        {
            Time.timeScale = 0f;
            ToggleMenuVisibility(true);
            Cursor.visible = true;
            cameraAudio.Pause();
            GameIsPaused = true;
        }
        else 
        {
            Time.timeScale = 1;
            ToggleMenuVisibility(false);
            Cursor.visible = false;
            cameraAudio.UnPause();
            GameIsPaused = false;
        }
    }

    private void ToggleMenuVisibility(bool isVisible)
    {
        foreach (var g in pauseMenuObjects)
        {
            g.SetActive(isVisible);
        }
    }
}
