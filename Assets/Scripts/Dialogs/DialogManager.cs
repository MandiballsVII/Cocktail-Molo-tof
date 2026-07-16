using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Image continueArrow;

    [Header("Settings")]
    [SerializeField]
    private float typingSpeed = 0.05f;

    private readonly Queue<string> dialogueLines = new();

    private string currentLine;

    private bool lineFinished;

    private Action dialogueFinishedCallback;

    public bool IsDialogueRunning { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (!IsDialogueRunning)
            return;

        if (!Input.GetMouseButtonDown(0))
            return;

        if (!lineFinished)
        {
            StopAllCoroutines();

            dialogueText.text = currentLine;

            continueArrow.gameObject.SetActive(true);

            lineFinished = true;

            return;
        }

        NextLine();
    }

    public void StartDialogue(
        DialogueData dialogue,
        Action finishedCallback = null)
    {
        List<string> lines = new();

        foreach (DialogueLine line in dialogue.lines)
            lines.Add(line.text);

        StartDialogue(lines, finishedCallback);
    }

    public void StartDialogue(
        List<string> lines,
        Action finishedCallback = null)
    {
        dialogueFinishedCallback = finishedCallback;

        dialoguePanel.SetActive(true);

        IsDialogueRunning = true;

        dialogueLines.Clear();

        foreach (string line in lines)
            dialogueLines.Enqueue(line);

        NextLine();
    }

    private void NextLine()
    {
        if (dialogueLines.Count == 0)
        {
            EndDialogue();
            return;
        }

        StopAllCoroutines();

        StartCoroutine(ShowLine(dialogueLines.Dequeue()));
    }

    private IEnumerator ShowLine(string line)
    {
        continueArrow.gameObject.SetActive(false);

        dialogueText.text = "";

        currentLine = line;

        lineFinished = false;

        foreach (char character in currentLine)
        {
            dialogueText.text += character;

            // TODO:
            // AudioManager.Instance.PlayDialogueSound();

            yield return new WaitForSeconds(typingSpeed);
        }

        continueArrow.gameObject.SetActive(true);

        lineFinished = true;
    }
    public void ContinueDialogue()
    {
        if (!IsDialogueRunning)
            return;

        if (!lineFinished)
        {
            StopAllCoroutines();

            dialogueText.text = currentLine;

            continueArrow.gameObject.SetActive(true);

            lineFinished = true;
        }
        else
        {
            NextLine();
        }
    }

    private void EndDialogue()
    {
        StopAllCoroutines();

        dialoguePanel.SetActive(false);

        IsDialogueRunning = false;

        dialogueFinishedCallback?.Invoke();

        dialogueFinishedCallback = null;
    }
}