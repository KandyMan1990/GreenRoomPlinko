using UnityEngine;
using UnityEngine.UI;

public class PlayerGameobject : MonoBehaviour
{
    Text uiText;
    Rigidbody2D rb;
    bool toldGameManager;
    PlayerData pd;

    void Awake()
    {
        uiText = GetComponentInChildren<Text>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetName(string n)
    {
        uiText.text = n;
    }

    public void SetPlayerData(PlayerData playerData)
    {
        pd = playerData;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (rb.velocity == Vector2.zero && !toldGameManager)
        {
            toldGameManager = true;
            LandingZone ld = collision.gameObject.GetComponent<LandingZone>();
            bool winZone = ld.WinZone;
            bool instantWinZone = ld.InstantWinZone;
            GameManager.Instance.PlayerFinished(new FinishedPlayer(this, transform, winZone, instantWinZone));
        }
    }

    public void ResetObj()
    {
        toldGameManager = false;
    }
}