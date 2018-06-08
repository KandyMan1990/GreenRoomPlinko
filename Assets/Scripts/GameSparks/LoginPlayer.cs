using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GameSparks.Api.Requests;

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