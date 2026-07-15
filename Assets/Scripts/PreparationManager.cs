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

    [Header("Garnishes")]
    [SerializeField] private DraggableObject[] garnishes;

    [SerializeField] private OrderManager orderManager;

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
                ChangePhase(PreparationPhase.MixingMethod);
                break;

            case PreparationPhase.MixingMethod:
                ChangePhase(PreparationPhase.Garnishes);
                break;

            case PreparationPhase.Garnishes:
                ChangePhase(PreparationPhase.Finished);
                orderManager.EvaluateOrder();
                break;
        }
    }

    public void ChangePhase(PreparationPhase newPhase)
    {
        currentPhase = newPhase;

        switch (currentPhase)
        {
            case PreparationPhase.GlassSelection:

                SetButtons(glassButtons, true);

                SetDragging(bottles, false);

                SetDragging(garnishes, false);

                stirButton.interactable = false;
                shakeButton.interactable = false;

                break;

            case PreparationPhase.Mixing:

                SetButtons(glassButtons, false);

                SetDragging(bottles, true);

                SetDragging(garnishes, false);

                stirButton.interactable = false;
                shakeButton.interactable = false;

                break;

            case PreparationPhase.MixingMethod:

                SetDragging(bottles, false);

                stirButton.interactable = true;
                shakeButton.interactable = true;

                break;

            case PreparationPhase.Garnishes:

                stirButton.interactable = false;
                shakeButton.interactable = false;

                SetDragging(garnishes, true);

                break;

            case PreparationPhase.Finished:

                SetDragging(garnishes, false);

                break;
        }
    }
    public void ResetPreparation()
    {
        glassSelector.CurrentGlass.Clear();
        glassSelector.ResetSelection();

        ChangePhase(PreparationPhase.GlassSelection);
        orderManager.StartNewOrder();
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

    private void SetButtons(Button[] buttons, bool enabled)
    {
        foreach (Button button in buttons)
            button.interactable = enabled;
    }
}