using UnityEngine;

[CreateAssetMenu(fileName = "Admin", menuName = "Admin")]
public class Admin : ScriptableObject
{
    public delegate void AdminEvent();

    public static event AdminEvent OnAdminChanged;

    const string _password = "C0FF33!!";
    bool _active = false;

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