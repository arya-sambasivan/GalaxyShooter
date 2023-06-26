using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public float speed = 3.5f;
    private float _speedMultiplier = 2;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] private GameObject tripleShotPrefab;
    [SerializeField] float _fireRate = 0.5f;
    [SerializeField] private  GameObject ShieldVisualizer;
    float _canFire = -1;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldActive = false;
    [SerializeField]
    private GameObject _rightEngine, _leftEngine;

    [SerializeField]
    private int _score;
    private UIManager _uiManager;
    private GameManager _gameManger;

    [SerializeField]
    private AudioClip _laserSoundClip;
    private AudioSource _audioSource;

    void Start()
    {
        
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManger = GameObject.Find("GameManager").GetComponent<GameManager>();
        _audioSource = GetComponent<AudioSource>();
        transform.position = new Vector3(0, 0, 0);
        
        if(_spawnManager == null)
        {
            Debug.Log("SpawnManager is NULL");
        }
        if (_uiManager == null)
        {
            Debug.Log("UIManager is NULL");
        }
        if (_audioSource == null)
        {
            Debug.Log("Audio Source is NULL");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
        }
    }

    void Update()
    {
        CalculatePlayerMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }

    }
    void CalculatePlayerMovement()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalAxis, verticalAxis, 0);
        transform.Translate(direction * speed * Time.deltaTime);
        if (transform.position.y >= 6)
        {
            transform.position = new Vector3(transform.position.x, 6, 0);
        }
        else if (transform.position.y <= -3.4f)
        {
            transform.position = new Vector3(transform.position.x, -3.4f, 0);
        }
        if (transform.position.x >= 8)
        {
            transform.position = new Vector3(8, transform.position.y, 0);
        }
        else if (transform.position.x <= -9)
        {
            transform.position = new Vector3(-9, transform.position.y, 0);
        }
    }
    
    void FireLaser()
    {
       
        _canFire = Time.time + _fireRate;
        if(_isTripleShotActive == true)
        {
            Instantiate(tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(laserPrefab, transform.position + new Vector3(0, 0.9f, 0), Quaternion.identity);
        }

        _audioSource.Play();   
    }
    
    public void Damage()
    {
        if(_isShieldActive==true)
        {
            ShieldVisualizer.SetActive(false);
            _isShieldActive = false;
            return;
        }
        _lives--;

        if (_lives == 2)
            _leftEngine.SetActive(true);
        else if (_lives == 1)
            _rightEngine.SetActive(true);

        _uiManager.UpdateLives(_lives);
        if(_lives<1)
        {
            _spawnManager.OnPlayerDeath();
            _uiManager.CheckForBestScore();
            Destroy(this.gameObject);
        }
    }

    public void TrippleshotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TrippleshotPowerDownRoutine());
    }

    IEnumerator TrippleshotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }
    public void SpeedBoostActive()
    {
        if(_isSpeedBoostActive==true)
            speed /= _speedMultiplier;
        _isSpeedBoostActive = true;
        speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }
    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        if(_isSpeedBoostActive==false)
            speed *= _speedMultiplier;
        _isSpeedBoostActive = false;
        speed /= _speedMultiplier;

    }
    public void ShieldsActive()
    {
        _isShieldActive = true;
        ShieldVisualizer.SetActive(true);
    }
    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}
 