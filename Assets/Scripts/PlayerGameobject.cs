using UnityEngine;
using UnityEngine.UI;

public class PlayerGameobject : MonoBehaviour
{
    Text uiText;

    void Awake()
    {
        uiText = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetName(string n)
    {
        uiText.text = n;
    }
}