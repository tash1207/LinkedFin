using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] GameObject hudCanvas;
    [SerializeField] GameObject penButton;
    [SerializeField] GameObject seaweedButton;

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

    void OnEnable()
    {
        Actions.OnPickUpPen += showPenButton;
        Actions.OnPickUpSeaweed += showSeaweedButton;
        Actions.OnGivePen += hidePenButton;
        Actions.OnGiveSeaweed += hideSeaweedButton;
    }

    void OnDisable()
    {
        Actions.OnPickUpPen -= showPenButton;
        Actions.OnPickUpSeaweed += showSeaweedButton;
        Actions.OnGivePen -= hidePenButton;
        Actions.OnGiveSeaweed -= hideSeaweedButton;
    }

    public void showHUD()
    {
        hudCanvas.SetActive(true);
        LinkedFinApp.Instance.isAvailable = true;
    }

    public void hideHUD()
    {
        hudCanvas.SetActive(false);
        LinkedFinApp.Instance.isAvailable = false;
    }

    private void showPenButton()
    {
        penButton.SetActive(true);
    }

    private void showSeaweedButton()
    {
        seaweedButton.SetActive(true);
    }

    private void hidePenButton()
    {
        penButton.SetActive(false);
    }

    private void hideSeaweedButton()
    {
        seaweedButton.SetActive(false);
    }
}
