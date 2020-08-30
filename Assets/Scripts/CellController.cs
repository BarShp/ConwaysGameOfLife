using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color aliveColor;
    [SerializeField] private Color deadColor;

    public bool IsAlive { get; set; } = false;
    public bool NewState { get; set; } = false;

    public void Start()
    {
        RenderSpriteColor();
    }

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
        spriteRenderer.color = IsAlive ? aliveColor : deadColor;
    }
}
