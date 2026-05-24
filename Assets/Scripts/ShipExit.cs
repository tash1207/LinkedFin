using UnityEngine;

public class ShipExit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FindFirstObjectByType<SceneTransporter>().TransportFromShip();
        }
    }
}
