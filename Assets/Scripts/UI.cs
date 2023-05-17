using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    public static bool gameOver = false;
    public static int score;
    private int topScore;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI topScoreText;
    public AudioSource cameraAudio;
    public AudioClip miPodveliRodinu;

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        scoreText.SetText("Score: " + score);
        if (score > topScore)
        {
            topScore = score;
            topScoreText.SetText("Top Score: " + topScore);
        }

        if (gameOver)
        {
            score = 0;
            cameraAudio.PlayOneShot(miPodveliRodinu);
        }
    }
}
