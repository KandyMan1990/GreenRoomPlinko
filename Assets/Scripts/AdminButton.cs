using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdminButton : MonoBehaviour
{
    string enable = "Enable Admin";
    string disable = "Disable Admin";

    void OnEnable()
    {
        GetComponent<Text>().text = Admin.Instance.isActive ? disable : enable;
    }
}