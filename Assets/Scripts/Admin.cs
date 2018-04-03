using UnityEngine;

public class Admin : MonoBehaviour
{
    public delegate void AdminEvent();

    public static event AdminEvent OnAdminChanged;
    public static Admin Instance { get; private set; }

    const string _password = "C0FF33!!";
    bool _active = false;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

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

        if (OnAdminChanged != null)
            OnAdminChanged();
    }
}