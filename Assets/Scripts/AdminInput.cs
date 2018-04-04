using UnityEngine;
using UnityEngine.UI;

public class AdminInput : MonoBehaviour
{
    [SerializeField] Text enableAdminBtnText;
    [SerializeField] Admin admin;

    public void SubmitPassword(string p)
    {
        admin.Enable(p);

        enableAdminBtnText.gameObject.SetActive(false);
        enableAdminBtnText.gameObject.SetActive(true);
    }
}