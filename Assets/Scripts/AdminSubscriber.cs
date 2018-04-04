using UnityEngine;

public class AdminSubscriber : MonoBehaviour
{
    [SerializeField] Admin admin;
    [SerializeField] GameObject[] objects;

    void OnEnable()
    {
        Admin.OnAdminChanged += SetObjectsActive;
    }

    void OnDisable()
    {
        Admin.OnAdminChanged -= SetObjectsActive;
    }

    void Start()
    {
        SetObjectsActive();
    }

    void SetObjectsActive()
    {
        if (objects.Length > 0)
        {
            bool active = admin.isActive;

            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].SetActive(active);
            }
        }
    }
}