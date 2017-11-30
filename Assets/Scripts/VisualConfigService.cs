using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Visual Config Service", menuName = "Visual Config Service")]
public class VisualConfigService : ScriptableObject
{
    [SerializeField]
    VisualConfig Default;
    [SerializeField]
    VisualConfig Christmas;


    public VisualConfig GetVisualConfig
    {
        get
        {
            if (DateTime.Now.Month == 12 && DateTime.Now.Day <= 25)
            {
                return Christmas;
            }

            return Default;
        }
    }
}