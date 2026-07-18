using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameFlowManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private OrderManager orderManager;
    [SerializeField] private ZoneChange zoneChange;
    [SerializeField] private PreparationManager preparationManager;

    [Header("Buttons")]
    [SerializeField] private Button goToPreparationButton;
    [SerializeField] private Button returnToBarButton;

    [Header("Static Dialogues")]
    [SerializeField] private DialogueData introDialogue;
    [SerializeField] private DialogueData goodEndingDialogue;
    [SerializeField] private DialogueData badEndingDialogue;

    [Header("Dialogue Collections")]
    [SerializeField] private DialogueCollection orderIntroductions;
    [SerializeField] private DialogueCollection shakeInstructions;
    [SerializeField] private DialogueCollection stirInstructions;

    [SerializeField] private DialogueCollection badReactions;
    [SerializeField] private DialogueCollection mediumReactions;
    [SerializeField] private DialogueCollection excellentReactions;

    [Header("Game")]
    [SerializeField] private int totalRounds = 10;
    [SerializeField] private DrunkennessBar drunkennessBar;

    [Header("Ending")]
    [SerializeField] private GameObject endingPanel;

    [SerializeField] private GameObject winImage;

    [SerializeField] private GameObject loseImage;

    [SerializeField] private float endingDuration = 5f;

    private int currentRound;
    private int drunkennessPoints;

    private void Start()
    {
        DialogueFactory.Initialize(
            orderIntroductions,
            shakeInstructions,
            stirInstructions,
            badReactions,
            mediumReactions,
            excellentReactions);

        goToPreparationButton.interactable = false;
        returnToBarButton.interactable = false;

        currentRound = 0;
        drunkennessPoints = 0;

        dialogueManager.StartDialogue(
            introDialogue,
            StartNextRound);
    }

    private void StartNextRound()
    {
        
        currentRound++;

        if (currentRound > totalRounds)
        {
            FinishGame();
            return;
        }

        preparationManager.ResetPreparation();

        orderManager.StartNewOrder();

        dialogueManager.StartDialogue(
            DialogueFactory.CreateOrderDialogue(
                orderManager.CurrentRecipe),
            EnablePreparationButton);
    }

    private void EnablePreparationButton()
    {
        goToPreparationButton.interactable = true;
    }

    public void GoToPreparation()
    {
        goToPreparationButton.interactable = false;

        zoneChange.ChangeToCocktail();

        // Más adelante aquí llamaremos al PreparationManager.
    }

    public void CocktailFinished()
    {
        returnToBarButton.interactable = true;
    }

    public void ReturnToBar()
    {
        returnToBarButton.interactable = false;
        goToPreparationButton.interactable = false;
        zoneChange.ChangeToClient();

        CocktailQuality quality =
            orderManager.EvaluateOrder();

        switch (quality)
        {
            case CocktailQuality.Bad:
                drunkennessPoints--;
                break;

            case CocktailQuality.Medium:
                drunkennessPoints++;
                break;

            case CocktailQuality.Excellent:
                drunkennessPoints += 2;
                break;
        }

        drunkennessBar.SetPoints(drunkennessPoints);

        dialogueManager.StartDialogue(
            DialogueFactory.CreateReactionDialogue(quality),
            StartNextRound);
    }

    private void FinishGame()
    {
        bool victory = drunkennessPoints >= totalRounds;

        DialogueData endingDialogue =
            victory ? goodEndingDialogue : badEndingDialogue;

        dialogueManager.StartDialogue(
            endingDialogue,
            () => ShowEnding(victory));
    }
    private void ShowEnding(bool victory)
    {
        Time.timeScale = 0f;

        endingPanel.SetActive(true);
        if (victory)
            winImage.SetActive(true);
        else
            loseImage.SetActive(true);

        StartCoroutine(ReturnToMainMenuRealtime());
    }
    private IEnumerator ReturnToMainMenuRealtime()
    {
        yield return new WaitForSecondsRealtime(5f);

        Time.timeScale = 1f;

        GameManager.Instance.LoadScene("MainMenu");
    }
}