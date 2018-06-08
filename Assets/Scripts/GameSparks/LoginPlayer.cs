using UnityEngine;
using UnityEngine.UI;
using GameSparks.Api.Requests;
using UnityEngine.SceneManagement;

public class LoginPlayer : MonoBehaviour
{
    [SerializeField] InputField userName, password;

    public void Login()
    {
        if (userName.text == string.Empty || password.text == string.Empty)
        {
            return;
        }

        new AuthenticationRequest().SetUserName(userName.text).SetPassword(password.text).Send((authResponse) => {
            if (!authResponse.HasErrors)
            {
                SceneManager.LoadScene(1);
            }
        });
    }
}