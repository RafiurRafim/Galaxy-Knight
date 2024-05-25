using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{   
    [SerializeField]
    private float _powerupspeed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        float positionx = Random.Range(-8f, 8f);
        transform.position = new Vector3(positionx, 10, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _powerupspeed * Time.deltaTime);
        if (transform.position.y < -6)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player player = collision.transform.GetComponent<Player>();
            if (player != null)
            {
                player.TripleLaserActiver();
            }
            Destroy(this.gameObject);
        }
    }
}
