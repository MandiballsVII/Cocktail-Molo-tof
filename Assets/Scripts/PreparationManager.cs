using UnityEngine;
using UnityEngine.UI;

public class PreparationManager : MonoBehaviour
{
    [Header("Current Phase")]
    [SerializeField] private PreparationPhase currentPhase;

    [Header("Glass Selection")]
    [SerializeField] private Button[] glassButtons;
    [SerializeField] private GlassSelector glassSelector;

    [Header("Bottles")]
    [SerializeField] private DraggableObject[] bottles;

    [Header("Preparation Buttons")]
    [SerializeField] private Button stirButton;
    [SerializeField] private Button shakeButton;
    [SerializeField] private Button confirmButton;

    [Header("Garnishes")]
    [SerializeField] private DraggableObject[] garnishes;

    [Header("Managers")]
    [SerializeField] private OrderManager orderManager;
    [SerializeField] private GameFlowManager gameFlowManager;
    [SerializeField] private HighlightArea glassHighlightArea;
    [SerializeField] private HighlightArea bottleHighlightArea;
    [SerializeField] private HighlightArea mixingMethodHighlightArea;
    [SerializeField] private HighlightArea garnishHighlightArea;

    private void Start()
    {
        ChangePhase(PreparationPhase.GlassSelection);
    }

    public void Confirm()
    {
        switch (currentPhase)
        {
            case PreparationPhase.GlassSelection:
                orderManager.SetSelectedGlass(glassSelector.CurrentGlass.Data);
                ChangePhase(PreparationPhase.Mixing);
                break;

            case PreparationPhase.Mixing:
                glassSelector.CurrentGlass.FirstIngredientAdded -= EnableConfirmButton;
                ChangePhase(PreparationPhase.MixingMethod);
                break;

            case PreparationPhase.MixingMethod:
                ChangePhase(PreparationPhase.Garnishes);
                break;

            case PreparationPhase.Garnishes:
                ChangePhase(PreparationPhase.Finished);
                //orderManager.EvaluateOrder();
                gameFlowManager.CocktailFinished();
                break;
        }
    }

    public void ChangePhase(PreparationPhase newPhase)
    {
        currentPhase = newPhase;

        switch (currentPhase)
        {
            case PreparationPhase.GlassSelection:

                glassHighlightArea.Show();

                SetButtons(glassButtons, true);

                SetDragging(bottles, false);

                SetDragging(garnishes, false);

                stirButton.interactable = false;
                shakeButton.interactable = false;

                break;

            case PreparationPhase.Mixing:

                glassHighlightArea.Hide();
                bottleHighlightArea.Show();

                SetButtons(glassButtons, false);

                SetDragging(bottles, true);

                SetDragging(garnishes, false);

                stirButton.interactable = false;
                shakeButton.interactable = false;

                DisableConfirmButton();

                glassSelector.CurrentGlass.FirstIngredientAdded -= EnableConfirmButton;
                glassSelector.CurrentGlass.FirstIngredientAdded += EnableConfirmButton;

                break;

            case PreparationPhase.MixingMethod:

                bottleHighlightArea.Hide();
                mixingMethodHighlightArea.Show();

                DisableConfirmButton();

                SetDragging(bottles, false);

                stirButton.interactable = true;
                shakeButton.interactable = true;

                break;

            case PreparationPhase.Garnishes:

                mixingMethodHighlightArea.Hide();
                garnishHighlightArea.Show();
                stirButton.interactable = false;
                shakeButton.interactable = false;

                SetDragging(garnishes, true);

                break;

            case PreparationPhase.Finished:

                garnishHighlightArea.Hide();
                DisableConfirmButton();
                SetDragging(garnishes, false);

                break;
        }
    }
    public void ResetPreparation()
    {
        if (glassSelector.CurrentGlass != null)
            glassSelector.CurrentGlass.FirstIngredientAdded -= EnableConfirmButton;
        EnableConfirmButton();
        glassSelector.CurrentGlass.Clear();
        glassSelector.ResetSelection();

        ChangePhase(PreparationPhase.GlassSelection);
        //orderManager.StartNewOrder();
    }

    private void SetDragging(DraggableObject[] objects, bool enabled)
    {
        if (objects == null)
        {
            Debug.LogError("El array de DraggableObjects es NULL.");
            return;
        }

        Debug.Log($"Array con {objects.Length} elementos.");

        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] == null)
            {
                Debug.LogError($"Elemento {i} es NULL.");
                continue;
            }

            objects[i].CanDrag = enabled;
        }
    }

    public void SetMixingMethodButtonsInteractable()
    {
        stirButton.interactable = true;
        shakeButton.interactable = true;
    }

    public void SetMixingMethodButtonsNonInteractable()
    {
        stirButton.interactable = false;
        shakeButton.interactable = false;
    }
    private void SetButtons(Button[] buttons, bool enabled)
    {
        foreach (Button button in buttons)
            button.interactable = enabled;
    }
    public void EnableConfirmButton()
    {
        confirmButton.interactable = true;
    }

    public void DisableConfirmButton()
    {
        confirmButton.interactable = false;
    }
}