using System;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    public static event Action GameOver;
    public static int score;
    private int topScore;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI topScoreText;
    public AudioSource cameraAudio;
    public AudioClip miPodveliRodinu;

    private void Start()
    {
        Cursor.visible = false;
        GameOver += PlayDefeatMusic;
    }

    private void Update()
    {
        scoreText.SetText("Score: " + score);
        if (score > topScore)
        {
            topScore = score;
            topScoreText.SetText("Top Score: " + topScore);
        }
    }

    private void PlayDefeatMusic()
    {
        cameraAudio.PlayOneShot(miPodveliRodinu);
    }
    
    public static void ResetGame()
    {
        score = 0;
        GameOver?.Invoke();
    }
}
