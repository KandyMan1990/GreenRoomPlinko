using UnityEngine;
using UnityEngine.UI;
using GameSparks.Api.Requests;
using UnityEngine.SceneManagement;

public class RegisterPlayer : MonoBehaviour
{
    [SerializeField] InputField userName, password, repeatPassword, vertical, room;

    public void Register()
    {
        if (userName.text == string.Empty || password.text == string.Empty || repeatPassword.text == string.Empty || vertical.text == string.Empty || room.text == string.Empty)
        {
            return;
        }

        if (password.text != repeatPassword.text)
        {
            return;
        }

        new RegistrationRequest()
            .SetDisplayName(userName.text)
            .SetPassword(password.text)
            .SetUserName(userName.text)
            .Send((registrationResponse) => {
                if (!registrationResponse.HasErrors)
                {
                    new AuthenticationRequest()
                    .SetUserName(userName.text)
                    .SetPassword(password.text)
                    .Send((authResponse) => {
                        if (!authResponse.HasErrors)
                        {
                            new LogEventRequest()
                            .SetEventKey("SAVE_PLAYER")
                            .SetEventAttribute("PLAYED", 0)
                            .SetEventAttribute("WON", 0)
                            .SetEventAttribute("LOST", 0)
                            .SetEventAttribute("MADE", 0)
                            .SetEventAttribute("RECEIVED", 0)
                            .SetEventAttribute("AVERAGE", 0)
                            .SetEventAttribute("VERTICAL", vertical.text)
                            .SetEventAttribute("ROOM", room.text)
                            .Send((response) =>
                            {
                                if (!response.HasErrors)
                                {
                                    SceneManager.LoadScene(1);
                                }
                            });   
                        }
                    });
                }
            });
    }
}