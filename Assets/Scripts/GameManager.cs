using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isgameover=false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)&& _isgameover == true)
        {
            SceneManager.LoadScene(0);
        }
    }
    public void GameOver()
    {
        _isgameover = true;
    }
}
