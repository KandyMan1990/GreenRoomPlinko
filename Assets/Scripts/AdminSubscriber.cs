using UnityEngine;

public class AdminSubscriber : MonoBehaviour
{
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
            bool active = Admin.Instance.isActive;

            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].SetActive(active);
            }
        }
    }
}