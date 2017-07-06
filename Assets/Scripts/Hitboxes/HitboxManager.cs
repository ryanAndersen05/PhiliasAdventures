using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Since we will be using multiple hitboxes in some animations, it will be important to have a manager so that we are not
/// unecessarily attacking the same target with a different hit box from the same attack. This hitbox manager will keep track of all active
/// hit boxes in an animation or object
/// </summary>
public class HitboxManager : MonoBehaviour {
    public Hitbox[] availableHitboxes;
}
