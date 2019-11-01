using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private Image _LivesImg;
    [SerializeField]
    private Sprite[] _LivesSprites;
    private bool _GameOverFlicker = false;
    private Color _GameOverColor;
    private GameManager _gameManager;
    private void Start()
    {
        _scoreText.text = "Score: 0";
        _GameOverColor = _gameOverText.color;
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _GameOverFlicker = false;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (_GameOverFlicker)
        {
            _gameManager.GameOver();
        }
    }
    public void UpdateScore(int points)
    {
        _gameOverText.gameObject.SetActive(false);
        _scoreText.text = "Score: " + points;
    }

    public void UpdateLives(int currentLives)
    {
        _LivesImg.sprite =_LivesSprites[currentLives];

        if(currentLives == 0)
        {
            _gameOverText.gameObject.SetActive(true);
            _GameOverFlicker = true;
            _restartText.gameObject.SetActive(true);
            StartCoroutine(GameOverFlicker());
        }
    }
    IEnumerator GameOverFlicker()
    {
        while (_GameOverFlicker)
        {
            _gameOverText.CrossFadeColor(Color.black, 1.0f, true, false);
            yield return new WaitForSeconds(1.0f);
            _gameOverText.CrossFadeColor(_GameOverColor, 1.0f, true, false);
            yield return new WaitForSeconds(1.0f);
        }
    }
}
