using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] player;
    public GameObject title;

    public AudioSource bgMusic;

    public bool gameStarted = false;

    public void StartGame()
    {
        title.gameObject.SetActive(false);

        bgMusic.Play();

        gameStarted = true;

    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.Log("Game Closed");

            Application.Quit();
        }

    }

    public void CheckWinState()
    {
        int aliveCount = 0;

        foreach(GameObject player in player)
        {
            if (player.activeSelf)
            {
                aliveCount++;

            }

        }

        if (aliveCount <= 1)
        {
            Invoke(nameof(NewRound), 3f);

        }

    }

    private void NewRound()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

}
