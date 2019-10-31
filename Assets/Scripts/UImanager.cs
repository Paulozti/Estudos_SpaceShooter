using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;

    private void Start()
    {
        _scoreText.text = "Score: 0";
    }
    public void UpdateScore(int points)
    {
        _scoreText.text = "Score: " + points;
    }
}
