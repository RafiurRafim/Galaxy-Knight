using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    [SerializeField]
    private float _asteroidSpeed = 5f;
    private Player _player;
    private Animator _anim;
    void Start()
    {
        _player = GameObject.Find("SpaceShip").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("_player is null");
        }
        _anim = GetComponent<Animator>();

        if (_anim == null)
        {
            Debug.LogError("_anim is null");
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _asteroidSpeed);
        if (transform.position.y <= -5.2)
        {
            float x = Random.Range(-9, 9);
            transform.position = new Vector3(x, 8, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {

            Destroy(other.gameObject);

            Debug.Log("hit laser");
            if (_player != null)
            {
                _player.AddScore(10);

            }
            _anim.SetTrigger("OnEnemyDeath");
            _asteroidSpeed = 0;
            Destroy(this.gameObject,2.8f);
        }

        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
                Debug.Log("hit player");
            }
            if(player==null)
            {
                Debug.LogError("player is null");
            }
            _anim.SetTrigger("OnEnemyDeath");
            _asteroidSpeed = 0;
            Destroy(this.gameObject,2.8f);
        }
    }
}
