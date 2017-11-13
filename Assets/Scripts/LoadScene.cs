using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    public void LoadSceneByName(string name)
    {
        SceneManager.LoadSceneAsync(name);
    }

    public void Quit()
    {
        Application.Quit();
    }
}