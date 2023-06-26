using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    [SerializeField]
    float _speed = 4;
    private GameManager _gameManger;
    private Player _player;
    private Animator _anim;
    private AudioSource _audioSource;
    [SerializeField] private GameObject _laserPrefab;
    private float _fireRate = 3f;
    private float _canFire = -1;
    private void Start()
    {
        _gameManger = GameObject.Find("GameManager").GetComponent<GameManager>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent <AudioSource>();
        _anim = GetComponent<Animator>();
    }
    void Update()
    {
        CalculateMovements();
        if(Time.time>_canFire)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
           GameObject enemyLaser =  Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            for(int i=0;i<lasers.Length;i++)
            {
                lasers[i].AssignEnemyLaser();
            }
        }
    }
    void CalculateMovements()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= -5.5f)
        {
            float randomNumber = Random.Range(-9, 8);
            transform.position = new Vector3(randomNumber, 7, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
             Player player = other.GetComponent<Player>();
            if(player!=null)
            {
                player.Damage();
            }
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject,2.8f);
            
        }
        else if(other.tag=="Laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(10);
            }
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject,2.8f);
            
        }
    }

}
