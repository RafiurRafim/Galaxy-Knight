using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField]
    private Text _text;
    [SerializeField]
    private Sprite[] _livesprite;
    [SerializeField]
    private Image _livesimage;
    [SerializeField]
    private Text _gameover;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private GameManager _gamemanager;
    void Start()

    {
        _text.text = "Score: " + 0;
        _gameover.gameObject.SetActive(false);
        _gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void ScoreUpdate(int playerscore)
    {
        _text.text = "Score: " + playerscore.ToString();
    }

    public void UpdateLives(int currentlives)
    {
        _livesimage.sprite = _livesprite[currentlives];
        if (currentlives == 0)
        {
            GameOverSequence();
        }

    }

    public void GameOverSequence()
    {


        _gamemanager.GameOver();
        _gameover.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlikerRoutine());
    }
    IEnumerator GameOverFlikerRoutine()

    {
        while (true)
        {
            _gameover.text = "GAME OVER!!";
            yield return new WaitForSeconds(0.5f);
            _gameover.text = "";
            yield return new WaitForSeconds(0.5f);
        }



    }
}
