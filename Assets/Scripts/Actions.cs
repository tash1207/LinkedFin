using System;
using UnityEngine;

public static class Actions
{
    // Game
    public static Action<ConnectionSO> OnConnectionMade;

    public static Action<SpeakerSO, DialogueSO> SetDialogStep;

    public static Action OnPickUpPen;
    public static Action OnPickUpSeaweed;
    public static Action OnGivePen;
    public static Action OnGiveSeaweed;
    public static Action OnGameOver;

    // UI
    public static Action ToggleFinApp;
    public static Action<ConnectionSO> ShowFintroduceList;
    public static Action<ConnectionSO> Fintroduce;
    public static Action<ConnectionSO, ConnectionSO> UpdateProfile;
}
