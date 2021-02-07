using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnifeConditionText : MonoBehaviour
{
    Text scoreText;
    float score;

    void Start()
    {
        scoreText = GetComponent<Text>();
    }

    void Update()
    {
        if(GameManager.actualChisel == "Square")
        {
            score = (GameManager.counterSquare) / GameManager.chiselStrenght;
            if(score <= 100)
            {
                scoreText.text = score.ToString("0" + "%");
            }
        }

        if (GameManager.actualChisel == "Round")
        {
            score = (GameManager.counterRound) / GameManager.chiselStrenght;
            if (score <= 100)
            {
                scoreText.text = score.ToString("0" + "%");
            }
        }

        if (GameManager.actualChisel == "Triangle")
        {
            score = (GameManager.counterTriangle) / GameManager.chiselStrenght;
            if (score <= 100)
            {
                scoreText.text = score.ToString("0" + "%");
            }
        }

        if (GameManager.actualChisel == "TrapezRight")
        {
            score = (GameManager.counterTrapezRight) / GameManager.chiselStrenght;
            if (score <= 100)
            {
                scoreText.text = score.ToString("0" + "%");
            }
        }

        if (GameManager.actualChisel == "TrapezLeft")
        {
            score = (GameManager.counterTrapezLeft) / GameManager.chiselStrenght;
            if (score <= 100)
            {
                scoreText.text = score.ToString("0" + "%");
            }
        }
    }
}
