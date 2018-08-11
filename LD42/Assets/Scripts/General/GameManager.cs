// Date   : 11.08.2018 15:52
// Project: LD42
// Author : bradur

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager main;

    private bool playerIsDead = false;

    private void Awake()
    {
        main = this;
    }

    public void KillPlayer()
    {
        UIManager.main.ShowPopup(
            "You died!",
            "Press R to retry level\n" + "Press Q to quit the game"
        );
        playerIsDead = true;
    }

    void Start()
    {

    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
    }

    void Update()
    {
        if (playerIsDead)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                playerIsDead = false;
                UIManager.main.HidePopup();
                RestartLevel();
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                playerIsDead = false;
                UIManager.main.HidePopup();
                Quit();
            }
        }
    }
}
