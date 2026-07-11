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

    private void Start()
    {
        ChangePhase(PreparationPhase.GlassSelection);
    }

    public void Confirm()
    {
        switch (currentPhase)
        {
            case PreparationPhase.GlassSelection:
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

    private void SetDragging(DraggableObject[] objects, bool enabled)
    {
        foreach (DraggableObject draggable in objects)
            draggable.CanDrag = enabled;
    }

    private void SetButtons(Button[] buttons, bool enabled)
    {
        foreach (Button button in buttons)
            button.interactable = enabled;
    }
}