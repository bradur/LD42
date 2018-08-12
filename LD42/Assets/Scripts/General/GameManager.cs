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

    private PlayerMovement player;

    [SerializeField]
    private LevelLoader levelLoader;

    private bool gameEnd = false;

    private void Awake()
    {
        main = this;
    }

    public void SetToolCounts(int bombCount, int dynamiteCount, int blockCount)
    {
        PlayerUseTool playerUseTool = GetPlayerUseTool();
        playerUseTool.SetToolCounts(bombCount, dynamiteCount, blockCount);
        UIManager.main.SetToolCounts(bombCount, dynamiteCount, blockCount);
        UIManager.main.SelectTool(ToolType.Bomb);
    }

    public void OpenNextLevel()
    {
        levelLoader.OpenNextLevel();
    }

    public void TheEnd()
    {
        UIManager.main.ShowPopup(
            "The end",
            "You managed to stop the slime from taking over the world!\n" +
            "Thanks for playing!\n" +
            "Press Q to quit the game\n"
        );
        gameEnd = true;
    }

    public void SetPlayer(PlayerMovement player)
    {
        this.player = player;
    }

    public PlayerMovement GetPlayer()
    {
        return player;
    }

    public PlayerUseTool GetPlayerUseTool()
    {
        return player.GetComponent<PlayerUseTool>();
    }

    public void KillPlayer()
    {
        UIManager.main.ShowPopup(
            "You died!",
            "Press R to retry level\n" + "Press Q to quit the game"
        );
        playerIsDead = true;
    }

    public void RestartLevel()
    {
        levelLoader.RestartLevel();
    }

    public void Quit()
    {
        Application.Quit();
    }

    bool escapeMenuOpen = false;

    void Update()
    {
        if (playerIsDead || escapeMenuOpen)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                playerIsDead = false;
                escapeMenuOpen = false;
                UIManager.main.HidePopup();
                RestartLevel();
            }
        }
        if (gameEnd || playerIsDead || escapeMenuOpen)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                gameEnd = false;
                escapeMenuOpen = false;
                playerIsDead = false;
                UIManager.main.HidePopup();
                Quit();
            }
        }
        if (!escapeMenuOpen)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                escapeMenuOpen = true;
                UIManager.main.ShowPopup(
                    "Game paused",
                    "Press R to restart\n" +
                    "Press Q to quit"
                );
            }
        }
    }
}
