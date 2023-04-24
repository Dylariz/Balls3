using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    public void StartPressed()
    {
        SceneManager.LoadScene("Game");
    }
    
    public void ExitPressed()
    {
        Application.Quit();
    }
}
