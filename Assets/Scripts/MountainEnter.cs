using UnityEngine;

public class MountainEnter : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FindFirstObjectByType<SceneTransporter>().TransportToMountain();
        }
    }
}
