using UnityEngine;

public class DialogueDatabase : MonoBehaviour
{
    [Header("Order")]
    [SerializeField] private DialogueCollection orderIntroductions;
    [SerializeField] private DialogueCollection shakeInstructions;
    [SerializeField] private DialogueCollection stirInstructions;

    [Header("Reactions")]
    [SerializeField] private DialogueCollection badReactions;
    [SerializeField] private DialogueCollection mediumReactions;
    [SerializeField] private DialogueCollection excellentReactions;

    private void Awake()
    {
        DialogueFactory.Initialize(
            orderIntroductions,
            shakeInstructions,
            stirInstructions,
            badReactions,
            mediumReactions,
            excellentReactions);
    }
}