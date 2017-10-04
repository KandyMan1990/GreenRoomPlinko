using UnityEngine;
using UnityEngine.UI;

public class AddNewPlayer : MonoBehaviour
{
    [SerializeField]
    UIManager uiManager;
    [SerializeField]
    Text errorMessage;
    [SerializeField]
    GameObject newPlayerUi;
    [SerializeField]
    InputField input;

    public void OnClick()
    {
        if (input.text == string.Empty)
        {
            errorMessage.gameObject.SetActive(true);
            return;
        }

        errorMessage.gameObject.SetActive(false);

        uiManager.AddNewPlayerButtonOnClick(input.text);

        input.text = string.Empty;

        newPlayerUi.SetActive(false);
    }
}