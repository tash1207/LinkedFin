using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageInfo : MonoBehaviour
{
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text messageText;
    [SerializeField] Image portrait;

    public MessageSO message;

    public void SetInfo(MessageSO messageSO)
    {
        message = messageSO;
        ConnectionSO conn = messageSO.connection;
        nameText.text = conn.connName;
        portrait.sprite = conn.portrait;
        messageText.text = messageSO.messageText;
    }
}
