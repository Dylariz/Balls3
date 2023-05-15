using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuControls : MonoBehaviour
{
    public float buttonsPressedAnimationDelay;
    public float buttonsNormalAnimationDelay;
    
    private PauseController pauseController;

    private void Start()
    {
        pauseController = GetComponent<PauseController>();
    }

    public void ContinuePressed()
    {
        StartCoroutine(WaitAnimationForContinueRoutine());
    }
    
    public void ExitPressed()
    {
        StartCoroutine(WaitAnimationForExitRoutine());
    }

    private IEnumerator WaitAnimationForContinueRoutine()
    {
        yield return new WaitForSecondsRealtime(buttonsPressedAnimationDelay + buttonsNormalAnimationDelay);
        pauseController.PauseGame(false);
    }
    
    private IEnumerator WaitAnimationForExitRoutine()
    {
        yield return new WaitForSecondsRealtime(buttonsPressedAnimationDelay + buttonsNormalAnimationDelay);
        pauseController.PauseGame(false);
        SceneManager.LoadScene("Menu");
    }
}