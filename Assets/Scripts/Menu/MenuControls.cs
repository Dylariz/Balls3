using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    public float buttonsPressedAnimationDelay;
    public float buttonsNormalAnimationDelay;

    private void Start()
    {
        Cursor.visible = true;
    }

    public void StartPressed()
    {
        StartCoroutine(WaitAnimationForStartRoutine());
    }
    
    public void ExitPressed()
    {
        StartCoroutine(WaitAnimationForExitRoutine());
    }
    
    private IEnumerator WaitAnimationForStartRoutine()
    {
        yield return new WaitForSecondsRealtime(buttonsPressedAnimationDelay + buttonsNormalAnimationDelay);
        SceneManager.LoadScene("Game");
    }
    
    private IEnumerator WaitAnimationForExitRoutine()
    {
        yield return new WaitForSecondsRealtime(buttonsPressedAnimationDelay + buttonsNormalAnimationDelay);
        Application.Quit();
    }
}
