using System;
using UnityEngine;

public static class Actions
{
    // Game
    public static Action<ConnectionSO> OnConnectionMade;

    public static Action<SpeakerSO, DialogueSO> SetDialogStep;

    // UI
    public static Action ToggleFinApp;
    
}
