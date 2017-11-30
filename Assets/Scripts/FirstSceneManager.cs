using UnityEngine;
using UnityEngine.UI;

public class FirstSceneManager : MonoBehaviour
{
    [SerializeField]
    VisualConfigService visualConfigService;
    [SerializeField]
    Image background;

    VisualConfig visualConfig;

    void Awake()
    {
        visualConfig = visualConfigService.GetVisualConfig;
        background.sprite = visualConfig.Background;
    }
}