using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Acts as a reference for all mechanics that the player will have. Will also store any stats about the player that UI
/// other game managers may need. 
/// </summary>
/// 

[RequireComponent(typeof(CharacterMovement))]
public class PlayerStats : MonoBehaviour {
    [Tooltip("The maximum health that this character can have at any one time")]
    public float maxHealth = 100;
    public float currentHealth { get; private set; }
    [HideInInspector]
    public CharacterMovement characterMovement;

    private void Start()
    {
        characterMovement = GetComponent<CharacterMovement>();
    }
}
