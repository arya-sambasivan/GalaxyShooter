using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0f;
    [SerializeField]
    private int powerUpID;
    void Start()
    {

    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= -5.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                if (powerUpID == 0)
                {
                    player.TrippleshotActive();
                }
                else if(powerUpID==1)
                {
                    Debug.Log("Collected speed boost");
                }
                else if (powerUpID == 2)
                {
                    Debug.Log("Collected shield");
                }
            }
            Destroy(this.gameObject);
        }
    }
}
