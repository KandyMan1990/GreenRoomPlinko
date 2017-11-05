using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorManager : MonoBehaviour
{ 
    public static ErrorManager Instance { get; private set; }
    [SerializeField]
    GameObject errorObject;
    [SerializeField]
    Text errorText;


    void Awake()
    {
        Instance = this;
    }

    public void ShowError(string error)
    {
        errorText.text = error;
        errorObject.SetActive(true);
    }
}