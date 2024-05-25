using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    [SerializeField]
    private float _asteroidSpeed = 4f;
    // Start is called before the first frame update
    void Start()
    {
        float x = Random.Range(-9, 9);
        transform.position = new Vector3(x, 8, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _asteroidSpeed);
        if (transform.position.y <= -5.2)
        {
            Start();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
            
            Debug.Log("hit laser");
        }

        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
                Debug.Log("hit player");
            }
            Destroy(this.gameObject);
        }
    }
}
