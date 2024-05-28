using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.4f;
    [SerializeField]
    private int _speedMultiplier = 2;
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
    [SerializeField]
    private bool _speedPowerupActive = false;
    [SerializeField]
    private bool _shieldActive = false;
    [SerializeField]
    private GameObject _shieldVisual;
    [SerializeField]
    private int _score;
    private UI _ui;
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnmanager = GameObject.Find("Spawnobject").GetComponent<Spawn>();
        _ui = GameObject.Find("Canvas").GetComponent<UI>();
        if (_spawnmanager == null)
        {
            Debug.LogError("spawn error");
        }

        if (_ui == null)
        {
            Debug.LogError("Ui manager");
        }

    }

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
        if (_shieldActive == true)
        {
            _shieldActive = false;
            _shieldVisual.SetActive(false);
            return;
        }

        _health -= 1;
        _ui.UpdateLives(_health);

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

    public void SpeedPowerupActive()
    {
        _speedPowerupActive = true;
        _speed = _speed * _speedMultiplier;
        StartCoroutine(SpeedPowerDownCoRoutine());
    }

    IEnumerator SpeedPowerDownCoRoutine()
    {
        yield return new WaitForSeconds(5f);

        _speedPowerupActive = false;
        _speed = _speed / _speedMultiplier;
    }

    public void ShieldsActive()
    {
        _shieldActive = true;
        _shieldVisual.SetActive(true);
    }

    public void AddScore(int points)
    {
        _score += points;
        _ui.ScoreUpdate(_score);
    }
}
