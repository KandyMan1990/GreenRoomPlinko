using System.Collections;
using UnityEngine;

public class Obsticle : MonoBehaviour
{
    CircleCollider2D circleCollider;

    void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
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
            circleCollider.enabled = false;
            yield return new WaitForSeconds(1f);
            circleCollider.enabled = true;
        }
    }
}