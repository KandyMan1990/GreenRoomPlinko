using UnityEngine;

public class Admin : MonoBehaviour
{
    public static Admin Instance { get; private set; }

    const string _password = "C0FF33!!";
    bool _active = false;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public bool isActive
    {
        get { return _active; }
    }

    public void Enable(string password)
    {
        _active = password == _password;
    }


}