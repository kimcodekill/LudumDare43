using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public enum GameState { Playing, Dead, Win };

    public static GameController instance;

    public float time;
    public float targets;
    public float objectives;

    public GameObject player;

    public Text hudTime;
    public Text targetsLeft;
    public Text objectivesLeft;
    public Text timeLeft;
    public GameObject hud;
    public GameObject winScreen;
    public GameObject deathScreen;

    private float counter;

    private GameState currentState;

    public GameState CurrentState {
        get { return currentState; }
        set { return; }
    }

    void Start()
    {
        instance = this;
        counter = 0;
        player.GetComponent<PlayerController>().time = time;
        hud.SetActive(true);
        hudTime.text = FormatTime();
        currentState = GameState.Playing;

        targets = GameObject.FindGameObjectsWithTag("Enemy").Length;
        objectives = GameObject.FindGameObjectsWithTag("Objective").Length;
    }

    void Update()
    {


        if (currentState == GameState.Playing)
        {
            time = player.GetComponent<PlayerController>().time;
            hudTime.text = FormatTime();
        }

        if (time <= 0 && currentState == GameState.Playing)
        {
            Destroy(player);
            currentState = GameState.Dead;
            hud.SetActive(false);
            deathScreen.SetActive(true);
            targetsLeft.text = targets.ToString();
            objectivesLeft.text = objectives.ToString();
            time = 0;
        }

        if (targets <= 0 && objectives <= 0 && currentState == GameState.Playing)
        {
            player.GetComponent<PlayerController>().enabled = false;
            currentState = GameState.Win;
            hud.SetActive(false);
            winScreen.SetActive(true);
            timeLeft.text = FormatTime();
        }
    }

    void FixedUpdate()
    {
        if (currentState == GameState.Playing)
        {
            if (time > 0)
            {
                player.GetComponent<PlayerController>().time-= Time.fixedDeltaTime;
            }
        }
    }

    string FormatTime()
    {
        return (time % 60).ToString("00") + ":" + Mathf.Floor((time * 100) % 100).ToString("00");
    }

    public void Restart()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void Continue()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextScene < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadSceneAsync(nextScene);
        }
        else
        {
            Debug.Log("Change to go to credits");
            SceneManager.LoadScene(0);
        }
    }
}