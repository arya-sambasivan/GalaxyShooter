using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Sprite[] _livesSprites;
    [SerializeField] private Image _livesImage;
    [SerializeField] private TextMeshProUGUI _gameOverText;
    [SerializeField] private TextMeshProUGUI _restartText;

    private GameManager _gameManger;
    void Start()
    {
        _scoreText.text = "Score :" + 0;
        _gameOverText.gameObject.SetActive(false);
        _gameManger = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void UpdateScore(int PlayerScore)
    {
        _scoreText.text = "Score :" + PlayerScore;
    }

    public void UpdateLives(int currentLives)
    {
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
}
