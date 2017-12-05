using UnityEngine;
using UnityEngine.UI;

public class Config : MonoBehaviour
{
    [SerializeField]
    GameObject panel;
    [SerializeField]
    Toggle toggle;
    [SerializeField]
    TableOrderPrefs tableOrderPrefs;
    [SerializeField]
    UIManager uiManager;

    void Awake()
    {
        panel.SetActive(false);
    }

    void Start()
    {
        toggle.isOn = tableOrderPrefs.OrderByPercentage;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            panel.SetActive(!panel.activeInHierarchy);
        }
    }

    public void SetOrderPreference(bool value)
    {
        tableOrderPrefs.SetOrder(value);
        uiManager.CreatePlayersList();
    }
}