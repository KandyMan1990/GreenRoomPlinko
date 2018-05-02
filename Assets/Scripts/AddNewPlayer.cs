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

        uiManager.AddNewPlayerButtonOnClick(input.text);

        CloseAndClear();

        GameManager.Instance.RemoveAllPlayers();
    }

    public void OnEnter()
    {
        if (!Input.GetKeyDown(KeyCode.Return))
            return;

        OnClick();
    }

    public void CloseAndClear()
    {
        errorMessage.gameObject.SetActive(false);

        input.text = string.Empty;

        newPlayerUi.SetActive(false);
    }
}