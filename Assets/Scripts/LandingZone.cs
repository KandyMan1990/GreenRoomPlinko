using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class LandingZone : MonoBehaviour
{
    [SerializeField]
    Color WinColour;
    [SerializeField]
    Color LoseColour;
    [SerializeField]
    Color InstantWinColour;
    [SerializeField]
    bool winZone;

    static bool instantWin;

    SpriteRenderer Renderer;
    BoxCollider2D Collider;

    void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();
        Collider = GetComponent<BoxCollider2D>();

        if (winZone && !instantWin && Random.Range(0, 100) <= 1)
        {
            instantWin = true;
            Renderer.color = InstantWinColour;
            return;
        }

        Renderer.color = winZone ? WinColour : LoseColour;
    }
}