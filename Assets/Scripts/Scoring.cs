using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI seekerScoreText;
    public TextMeshProUGUI titleUI;
    public int tokenTotal = 0;

    public void UpdateScore(string colEntTag, int score)
    {
        if (colEntTag == "Player")
        {
            playerScoreText.text = "Player Tokens: " + score.ToString() + "/13";
        }
        else if (colEntTag == "Seeker")
        {
            seekerScoreText.text = "Seeker Tokens: " + score.ToString() + "/13";
        }
    }
}