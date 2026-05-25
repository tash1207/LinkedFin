using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionProfile : MonoBehaviour
{
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text jobText;
    [SerializeField] TMP_Text lookingForText;
    [SerializeField] Image portrait;
    [SerializeField] GameObject fintroduceButton;

    public ConnectionSO profile;

    public void SetInfo(ConnectionSO connectionSO)
    {
        profile = connectionSO;
        nameText.text = connectionSO.connName;
        jobText.text = connectionSO.jobTitle;
        lookingForText.text = connectionSO.lookingFor;
        portrait.sprite = connectionSO.portrait;
        fintroduceButton.SetActive(!connectionSO.hideFintroduce);
    }

    public void ShowFintroduceList()
    {
        Actions.ShowFintroduceList(profile);
    }
}
