using UnityEngine;

public class SetTargetFPS : MonoBehaviour
{
    [SerializeField]
    bool halfFrameRate;

    void Awake()
    {
        QualitySettings.vSyncCount = halfFrameRate ? 2 : 1;
    }
}