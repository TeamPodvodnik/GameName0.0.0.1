using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public bool isFinalPortal = false;
    public Vector2 targetPosition = Vector2.zero;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isFinalPortal)
            {
                SceneManager.LoadScene("TheEnd");
            }
            else
            {
                other.transform.position = targetPosition;
            }
        }
    }
}