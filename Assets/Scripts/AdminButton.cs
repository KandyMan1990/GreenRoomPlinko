using UnityEngine;
using UnityEngine.UI;

public class AdminButton : MonoBehaviour
{
    [SerializeField] Admin admin;

    string enable = "Enable Admin";
    string disable = "Disable Admin";

    void OnEnable()
    {
        GetComponent<Text>().text = admin.isActive ? disable : enable;
    }
}