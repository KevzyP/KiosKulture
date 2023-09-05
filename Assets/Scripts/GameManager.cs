using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Timer timer;
    public CustomerController customerController;
    public Score score;
    public List<GameObject> gameoverScreens;

    public bool suspended = false;
    public bool menuOpened = false;

    bool gameover = false;


    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        StartCoroutine(GameoverCheck());
    }

    IEnumerator GameoverCheck()
    {

        Debug.Log("gameovercheck coroutune activated");
        while (gameover == false)
        {
            if (timer.time != 0 && score.scoreValue == customerController.customerAmount && !gameover && !suspended)
            {
                WinScreen();
            }
            else if (timer.time == 0 && !gameover && !suspended)
            {
                LoseScreen();
            }
            yield return null;
        }
    }

    void WinScreen()
    {
        Debug.Log("win screen activated");
        gameover = true;
        suspended = true;
        gameoverScreens[0].SetActive(true);
    }

    void LoseScreen()
    {
        Debug.Log("lose screen activated");
        gameover = true;
        suspended = true;
        gameoverScreens[1].SetActive(true);
    }

    public IEnumerator WaitForTrivia()
    {
        if (GameManager.Instance.suspended == true)
        {
            yield return new WaitUntil(() => GameManager.Instance.suspended == false);
        }
    }
}
