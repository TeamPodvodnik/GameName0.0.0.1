using Unity.VisualScripting;
using UnityEngine;

public class HoldItem : MonoBehaviour
{



public bool hold;
public float distance = 1f;
RaycastHit2D hit;
    public Transform holdPoint;

    void Start()
    {

    }

    void Update()

    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!hold)
            {
                Physics2D.queriesStartInColliders = false;
                hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);

                if (hit.collider != null)
                {
                    hold = true;
                }


            }

            if (hold)
            {
                Vector2 targetPos = holdPoint.position;
                hit.collider.gameObject.transform.position = holdPoint.position;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
    }
 
}
