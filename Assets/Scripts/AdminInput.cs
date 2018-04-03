using UnityEngine;
using UnityEngine.UI;

public class AdminInput : MonoBehaviour
{
    [SerializeField] Text enableAdminBtnText;

    public void SubmitPassword(string p)
    {
        Admin.Instance.Enable(p);

        enableAdminBtnText.gameObject.SetActive(false);
        enableAdminBtnText.gameObject.SetActive(true);
    }
}