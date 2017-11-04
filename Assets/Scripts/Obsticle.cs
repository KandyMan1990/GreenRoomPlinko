using System.Collections;
using UnityEngine;

public class Obsticle : MonoBehaviour
{
    [SerializeField]
    float timeUntilCheck = 1f;
    Collider2D thisCollider;
    WaitForSeconds wait;

    void Awake()
    {
        thisCollider = GetComponent<Collider2D>();
        wait = new WaitForSeconds(timeUntilCheck);
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            StartCoroutine(CheckCollision(col.gameObject));
        }
    }

    IEnumerator CheckCollision(GameObject go)
    {
        Vector3 currentPos = go.transform.position;

        yield return new WaitForSeconds(1f);

        if (go.transform.position == currentPos)
        {
            thisCollider.enabled = false;
            yield return wait;
            thisCollider.enabled = true;
        }
    }
}