using UnityEngine;

public class MountainExit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FindFirstObjectByType<SceneTransporter>().TransportFromMountain();
        }
    }
}
