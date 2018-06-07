using UnityEngine;
using UnityEngine.UI;
using GameSparks.Api.Requests;
using UnityEngine.SceneManagement;

public class LoginPlayer : MonoBehaviour
{
    [SerializeField] Text userName;
    [SerializeField] InputField password;

    public void Login()
    {
        Debug.Log("Logging In");

        if (userName.text == string.Empty || password.text == string.Empty)
        {
            Debug.LogError("Empty username or password fields");
            return;
        }

        new AuthenticationRequest().SetUserName(userName.text).SetPassword(password.text).Send((authResponse) => {
            if (!authResponse.HasErrors)
            {
                Debug.Log(string.Format("{0} Authenticated", userName.text));
                SceneManager.LoadScene(1);
            }
            else
            {
                Debug.LogError("Error Authenticating Player");
                Debug.LogError(authResponse.Errors.JSON);
            }
        });
    }
}