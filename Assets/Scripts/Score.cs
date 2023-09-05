using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TMP_Text scoreText;
    public int scoreValue;

    public void SetScore(int addedScore)
    {
        scoreValue = int.Parse(scoreText.text);

        scoreValue += addedScore;

        scoreText.text = scoreValue.ToString();
    }
}
