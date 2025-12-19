using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct Dialogue
{
    [TextArea] public string dialogue;
    public UnityEvent onStart;
    public UnityEvent onFinish;
}

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/Conversation/Dialogue", order = 0)]
public class DialogueScriptable : ScriptableObject
{
    public bool isRandom = false;
    public bool disableMovement = true;
    public Dialogue[] dialogues;
    public UnityEvent onEnd;
}