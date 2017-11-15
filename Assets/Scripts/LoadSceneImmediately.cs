using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneImmediately : MonoBehaviour
{
    AsyncOperation async;

    // Use this for initialization
    void Start()
    {
        async = SceneManager.LoadSceneAsync("Main");
        async.allowSceneActivation = false;
    }

    public void LoadMain()
    {
        async.allowSceneActivation = true;
    }
}