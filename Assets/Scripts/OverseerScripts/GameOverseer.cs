using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to hold the general information that is present in the current game
/// </summary>
public class GameOverseer : MonoBehaviour {
    #region static variables
    private static GameOverseer instance;

    public static GameOverseer Instance
    {
        get
        {
            if (!instance)
            {
                instance = GameObject.FindObjectOfType<GameOverseer>();
            }
            return instance;
        }
    }
    #endregion static variables

    #region main variable
    private PlayerStats playerStats;


    #endregion main variables

    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
    }


    private const string PLAYER_HEALTH = "Player_Health";

    public void SaveGame()
    {
        PlayerPrefs.SetFloat(PLAYER_HEALTH, playerStats.maxHealth);
    }

    public void LoadGame()
    {
        playerStats.maxHealth = PlayerPrefs.GetFloat(PLAYER_HEALTH);
    }
}
