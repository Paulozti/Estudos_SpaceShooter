using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _gameIsOver;
    void Start()
    {
        _gameIsOver = false;    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _gameIsOver)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void GameOver()
    {
        _gameIsOver = true;
    }
}
