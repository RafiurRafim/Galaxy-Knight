using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{   
    [SerializeField]
    private float _powerupspeed = 3f;
    [SerializeField]
    private int powerUpId;
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
               /* if (powerUpId == 0)
                {
                    player.TripleLaserActiver();
                }
                else if (powerUpId == 1)
                {
                    Debug.Log("powerup2");
                }
                else if (powerUpId == 2)
                {
                    Debug.Log("powerup2");
                }
*/
                switch (powerUpId)
                {
                    case 0:
                        player.TripleLaserActiver();
                        break;
                    case 1:
                        Debug.Log("speedboost");
                        player.SpeedPowerupActive();
                        break;
                    case 2:
                        player.ShieldsActive();
                        Debug.Log("shield");
                        break;
                    default:
                        Debug.Log("default");
                        break;
                         
                }
            }


            Destroy(this.gameObject);
        }
    }
}
