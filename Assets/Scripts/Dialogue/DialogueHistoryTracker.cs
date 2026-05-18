using System.Collections.Generic;
using UnityEngine;

public class DialogueHistoryTracker : MonoBehaviour
{
    public static DialogueHistoryTracker Instance;
    private readonly List<SpeakerSO> spokenNPCs = new List<SpeakerSO>();
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void RecordNPC(SpeakerSO speakerSO)
    {
        spokenNPCs.Add(speakerSO);
        Debug.Log("Just spoke to " + speakerSO.speakerName);
    }

    public bool HasSpokenWith(SpeakerSO speakerSO)
    {
        return spokenNPCs.Contains(speakerSO);
    }
}
