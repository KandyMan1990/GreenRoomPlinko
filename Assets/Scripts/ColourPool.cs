using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourPool : MonoBehaviour
{
    [SerializeField]
    Color[] Colours;

    List<Color> colourPool = new List<Color>();

    public static ColourPool Instance;

    void Awake()
    {
        colourPool.AddRange(Colours);
        Instance = this;
    }

    public Color GetColour
    {
        get
        {
            int i = Random.Range(0, colourPool.Count);
            Color c = colourPool[i];
            colourPool.RemoveAt(i);

            return c;
        }
    }

    public void ReturnColour(Color c)
    {
        colourPool.Add(c);
    }
}