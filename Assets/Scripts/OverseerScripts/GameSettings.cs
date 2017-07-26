using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class holds the general game settings that are used  throughout the game
/// </summary>
public class GameSettings : MonoBehaviour {
    #region static variables
    public static GameSettings Instance
    {
        get
        {
            if (!instance)
            {
                instance = GameObject.FindObjectOfType<GameSettings>();
            }
            return instance;
        }
    }

    private static GameSettings instance;
    #endregion static variables

    #region main variables

    #endregion main variables


}
