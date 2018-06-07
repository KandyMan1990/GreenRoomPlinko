using UnityEngine;
using UnityEngine.UI;
using GameSparks.Api.Requests;
using UnityEngine.SceneManagement;

public class RegisterPlayer : MonoBehaviour
{
    [SerializeField] Text userName, password, repeatPassword;

    public void Register()
    {
        Debug.Log("Registering");

        if (userName.text == string.Empty || password.text == string.Empty || repeatPassword.text == string.Empty)
        {
            Debug.LogError("Empty username or password fields");
            return;
        }

        if (password.text != repeatPassword.text)
        {
            Debug.LogError("Password does not match repeat password");
            return;
        }

        new RegistrationRequest()
            .SetDisplayName(userName.text)
            .SetPassword(password.text)
            .SetUserName(userName.text)
            .Send((registrationResponse) => {
                if (!registrationResponse.HasErrors)
                {
                    Debug.Log(string.Format("{0} Registered", userName.text));
                    Debug.Log(string.Format("Authenticating {0}", userName.text));

                    new AuthenticationRequest().SetUserName(userName.text).SetPassword(password.text).Send((authResponse) => {
                        if (!authResponse.HasErrors)
                        {
                            Debug.Log(string.Format("{0} Authenticated", userName.text));
                            SceneManager.LoadScene(1);
                        }
                        else
                        {
                            Debug.LogError("Error Authenticating Player");
                            Debug.LogError(authResponse.Errors);
                        }
                    });
                }
                else
                {
                    Debug.LogError("Error Registering Player");
                    Debug.LogError(registrationResponse.Errors);
                }
            });
    }
}