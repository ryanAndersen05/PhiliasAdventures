using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour {
    public RectTransform pauseMenu;

    private bool gamePaused = false;

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            PauseGame();
        }
    }

    #region main functions
    private void PauseGame()
    {
        gamePaused = !gamePaused;

        pauseMenu.gameObject.SetActive(gamePaused);
        if (gamePaused)
        {
            GameOverseer.Instance.currentGameState = GameOverseer.GameState.PAUSED;
            Time.timeScale = 0f;
        }
        else
        {
            GameOverseer.Instance.currentGameState = GameOverseer.GameState.PLAYING;
            Time.timeScale = 1f;
        }
    }
    #endregion main functions
}
