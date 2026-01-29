using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    int score;
    Text text;
    public GameManager gameManager;

    public int Score => score;

    void Start() {
        score = 0;
        text = GetComponent<Text>();
        text.text = $"Score: {score}";
    }

    public void OnCollideBlock() {
        score++;
        text.text = $"Score: {score}";
    }

    public void ResetScore()
    {
        score = 0;
        if (text == null) text = GetComponent<Text>();
        if (text != null) text.text = $"Score: {score}";
    }
}
