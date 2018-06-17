using UnityEngine;

public class EnableButtons : MonoBehaviour
{
    void Start()
    {
        User user = UserData.Load();
        
        if (!user.isChallenger)
        {
            gameObject.SetActive(false);
        }
    }
}