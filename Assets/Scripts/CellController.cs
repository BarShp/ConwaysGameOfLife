using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    // public bool NewState { get; set; }
    // Consider using "shouldBeAlive" and on stepToNew automatically update to alive/dead as needed
    // Thus allowing to update the state of only those who should be alive

    public bool IsAlive { get; set; } = false;
    public bool NewState { get; set; } = false;

    public void StepNext()
    {
        IsAlive = NewState;
        RenderSpriteColor();
    }

    public void SwitchState()
    {
        IsAlive = !IsAlive;
        RenderSpriteColor();
    }

    private void RenderSpriteColor()
    {
        // TODO: Make the colors serializeable
        spriteRenderer.color = IsAlive ? Color.white : Color.black;
    }
}
