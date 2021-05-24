using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float _speed = 4;

    // Update is called once per frame.
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y<=-5.5f)
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
            Destroy(this.gameObject);
        }
        else if(other.tag=="Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }

}
