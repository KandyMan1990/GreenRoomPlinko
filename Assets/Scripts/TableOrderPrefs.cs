using UnityEngine;

public class TableOrderPrefs : MonoBehaviour
{
    string key = "TableOrder";

    void Awake()
    {
        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetInt(key, 1);
        }
    }

    public bool OrderByAscending
    {
        get { return PlayerPrefs.GetInt(key) == 1; }
    }

    public void SetOrder(bool ascending)
    {
        int value = ascending ? 1 : 0;

        PlayerPrefs.SetInt(key, value);
    }
}