using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FintroduceListItem : MonoBehaviour
{
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text jobText;
    [SerializeField] Image portrait;

    public ConnectionSO profile;

    public void SetInfo(ConnectionSO connectionSO)
    {
        profile = connectionSO;
        nameText.text = connectionSO.connName;
        jobText.text = connectionSO.jobTitle;
        portrait.sprite = connectionSO.portrait;
    }

    public void Fintroduce()
    {
        Actions.Fintroduce(profile);
    }
}
