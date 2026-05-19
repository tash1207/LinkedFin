using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] GameObject hudCanvas;
    [SerializeField] Button linkedFinButton;

    public static HUDManager Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void showHUD()
    {
        hudCanvas.SetActive(true);
    }

    public void hideHUD()
    {
        hudCanvas.SetActive(false);
    }

    public void disableHUD()
    {
        linkedFinButton.enabled = false;
    }

    public void enableHUD()
    {
        linkedFinButton.enabled = true;
    }
}
