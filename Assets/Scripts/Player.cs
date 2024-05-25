using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.4f;
    [SerializeField]
    private GameObject _laserprefab;
    [SerializeField]
    private GameObject _TripleLaserPrefab;
    [SerializeField]
    private float _nextfiretime = 0.5f;
    [SerializeField]
    private float _canfire = -1f;
    [SerializeField]
    private int _health = 3;
    private Spawn _spawnmanager;
    [SerializeField]
    private bool _tripleLaserActive = false;
    void Start()
    {
        // take the current position = new position
        transform.position = new Vector3(0, 0, 0);
        _spawnmanager = GameObject.Find("Spawnobject").GetComponent<Spawn>();

        if (_spawnmanager == null)
        {
            Debug.LogError("spawn error");
        }

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canfire)
        {
            WeaponFire();
        }
    }

    void PlayerMovement()
    {
        float horizontalmoving = Input.GetAxis("Horizontal");
        float verticalmoving = Input.GetAxis("Vertical");


        // transform.Translate(Vector3.right * horizontalmoving *_speed * Time.deltaTime); // real time movement 
        // transform.Translate(Vector3.up * verticalmoving * _speed * Time.deltaTime);


        Vector3 direction = new Vector3(horizontalmoving, verticalmoving, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        /*  if (transform.position.y >= 0)
          {
              transform.position = new Vector3(transform.position.x, 0, 0);
          }

          else if (transform.position.y <= -3)
          {
              transform.position = new Vector3(transform.position.x, -3, 0);
          }*/

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.4f, 0), 0);

        if (transform.position.x >= 11.2)
        {
            transform.position = new Vector3(-11.2f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.2)
        {
            transform.position = new Vector3(11.2f, transform.position.y, 0);
        }
    }

    void WeaponFire()
    {
        _canfire = Time.time + _nextfiretime;
        if (_tripleLaserActive == true)
        {
            Instantiate(_TripleLaserPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserprefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);

        }
       
    }

    public void Damage()
    {
        _health -= 1;
        if (_health < 1)
        {
            _spawnmanager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleLaserActiver()
    {
        _tripleLaserActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        _tripleLaserActive = false;
    }
}
