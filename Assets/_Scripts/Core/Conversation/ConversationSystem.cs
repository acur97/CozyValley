using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class ConversationSystem : MonoBehaviour
{
    public static ConversationSystem instance;

    [Header("References")]
    [SerializeField] private GameObject root;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject btnExit;
    [SerializeField] private GameObject btnNext;

    private DialogueScriptable currentDialogue;
    [HideInInspector] public bool movementDisabled = false;

    public DialogueScriptable test;

    private string title;
    [HideInInspector] private string titlePart;
    [HideInInspector] private int titleLength;
    private string[] titleParts;
    private int titleIndex;

    [Space]
    private bool inDialogue = false;
    private bool isAnimating = false;
    private bool isFinished = true;
    private bool skipText = false;
    private bool waitForNext = false;

    private int dialogueIndex = 0;

    private void Awake()
    {
        instance = this;

        DisablePanel();
    }

    private void DisablePanel()
    {
        root.SetActive(false);
        canvasGroup.alpha = 0;
        btnExit.SetActive(false);
        btnNext.SetActive(false);
    }

    private void Update()
    {
        if (!inDialogue)
            return;

        if (Input.GetButtonDown(InputStrings.Fire1))
        {
            if (isAnimating)
            {
                skipText = true;
            }
            else if (waitForNext || isFinished)
            {
                NextConversation();
            }
        }
    }

    [ContextMenu("TEST")]
    public void InitConversationTest()
    {
        InitConversation(test);
    }

    public void InitConversation(DialogueScriptable dialogueScriptable)
    {
        inDialogue = true;

        currentDialogue = dialogueScriptable;

        movementDisabled = currentDialogue.disableMovement;

        btnExit.SetActive(false);
        btnNext.SetActive(false);
        text.text = string.Empty;

        root.SetActive(true);
        LeanTween.alphaCanvas(canvasGroup, 1, 0.5f).setOnComplete(InitPanel);
    }

    private void InitPanel()
    {
        if (!currentDialogue.isRandom)
        {
            dialogueIndex = -1;
            waitForNext = true;

            NextConversation();
        }
        else
        {
            dialogueIndex = currentDialogue.dialogues.Length - 1;
            SetText(currentDialogue.dialogues[Random.Range(0, currentDialogue.dialogues.Length)].dialogue);
        }
    }

    private void NextConversation()
    {
        btnExit.SetActive(false);
        btnNext.SetActive(false);

        dialogueIndex++;

        if (dialogueIndex > currentDialogue.dialogues.Length - 1)
        {
            currentDialogue.onEnd?.Invoke();
            movementDisabled = false;
            inDialogue = false;

            LeanTween.alphaCanvas(canvasGroup, 0, 0.5f).setOnComplete(DisablePanel);

            return;
        }

        currentDialogue.dialogues[dialogueIndex].onStart?.Invoke();

        SetText(currentDialogue.dialogues[dialogueIndex].dialogue);
    }

    private void SetText(string newText)
    {
        isAnimating = true;
        isFinished = false;

        title = newText;
        titlePart = string.Empty;
        text.text = titlePart;
        titleLength = newText.Length;
        titleParts = new string[titleLength];

        for (int i = 0; i < titleLength; i++)
        {
            titlePart += newText[i];
            titleParts[i] = titlePart;
        }

        titlePart = string.Empty;
        titleLength = 0;

        AnimateText().Forget();
    }

    private async UniTaskVoid AnimateText()
    {
        titleIndex = 0;

        while (!skipText && text.text.Length < title.Length)
        {
            text.text = titleParts[titleIndex];
            titleIndex++;

            await UniTask.Delay(50);
        }

        skipText = false;
        isAnimating = false;
        isFinished = true;

        text.text = title;

        if (dialogueIndex == currentDialogue.dialogues.Length - 1)
        {
            btnExit.SetActive(true);
        }
        else
        {
            btnNext.SetActive(true);
        }

        currentDialogue.dialogues[dialogueIndex].onFinish?.Invoke();
    }
}