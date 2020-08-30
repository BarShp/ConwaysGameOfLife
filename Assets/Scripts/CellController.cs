using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    // public bool NewState { get; set; }
    // Consider using "shouldBeAlive" and on stepToNew automatically update to alive/dead as needed
    // Thus allowing to update the state of only those who should be alive

    private bool oldState;
    // private bool newState;

    public void SetState(bool newState)
    {
        oldState = newState;
        // TODO: Make the colors serializeable
        spriteRenderer.color = newState ? Color.white : Color.black;
    }
}
