using System.Collections.Generic;
using UnityEngine;

public static class DialogueFactory
{
    private static DialogueCollection orderIntroductions;
    private static DialogueCollection shakeInstructions;
    private static DialogueCollection stirInstructions;

    private static DialogueCollection badReactions;
    private static DialogueCollection mediumReactions;
    private static DialogueCollection excellentReactions;

    public static void Initialize(
        DialogueCollection order,
        DialogueCollection shake,
        DialogueCollection stir,
        DialogueCollection bad,
        DialogueCollection medium,
        DialogueCollection excellent)
    {
        orderIntroductions = order;
        shakeInstructions = shake;
        stirInstructions = stir;

        badReactions = bad;
        mediumReactions = medium;
        excellentReactions = excellent;
    }

    public static List<string> CreateOrderDialogue(
        RecipeData recipe)
    {
        List<string> dialogue = new();

        dialogue.Add(
            orderIntroductions.GetRandomLine(
                recipe.recipeName));

        dialogue.Add(
            recipe.preparationMethod == PreparationMethod.Shake
                ? shakeInstructions.GetRandomLine()
                : stirInstructions.GetRandomLine());

        return dialogue;
    }

    public static List<string> CreateReactionDialogue(
        CocktailQuality quality)
    {
        DialogueCollection collection = quality switch
        {
            CocktailQuality.Bad => badReactions,
            CocktailQuality.Medium => mediumReactions,
            _ => excellentReactions
        };

        return new List<string>
        {
            collection.GetRandomLine()
        };
    }
}