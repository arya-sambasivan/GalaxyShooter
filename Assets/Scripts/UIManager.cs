using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText,_bestScoreText;
    [SerializeField] private Sprite[] _livesSprites;
    [SerializeField] private Image _livesImage;
    [SerializeField] private TextMeshProUGUI _gameOverText;
    [SerializeField] private TextMeshProUGUI _restartText;
    private int _score = 0, _bestScore = 0;
   

    private GameManager _gameManger;
    void Start()
    {
        _scoreText.text = "Score :" + 0;
        _bestScore = PlayerPrefs.GetInt("HighScore",0);
        _bestScoreText.text = "Best Score : " + _bestScore;
        _gameOverText.gameObject.SetActive(false);
        _gameManger = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void UpdateScore(int PlayerScore)
    {
        _score = PlayerScore;
        _scoreText.text = "Score :" + PlayerScore;
    }
    
    public void CheckForBestScore()
    {
        if(_score>_bestScore)
        {
            _bestScore = _score;
            PlayerPrefs.SetInt("HighScore", _bestScore);
            _bestScoreText.text = "Best Score : " + _bestScore;
        }
    }

    public void UpdateLives(int currentLives)
    {
        if (currentLives == -1)
            return;
        _livesImage.sprite = _livesSprites[currentLives];
        if(currentLives==0)
        {
            GameOverSequence();
        }
    }
    void GameOverSequence()
    {
        _gameManger.GameOver();
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }
    IEnumerator GameOverFlickerRoutine()
    {
        while(true)
        {
            _gameOverText.text = "Game Over";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void ResumePlay()
    {
        GameManager gm = GameObject.Find("GameManager").GetComponentInParent<GameManager>();
        gm.ResumeGame();
    }
    
    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main_Menu");
    }
}
