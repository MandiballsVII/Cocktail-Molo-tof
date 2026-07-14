using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OrderManager : MonoBehaviour
{
    [SerializeField]
    private RecipeData[] recipes;

    private RecipeData currentRecipe;

    public RecipeData CurrentRecipe => currentRecipe;

    private GlassData selectedGlass;

    private PreparationMethod selectedMethod;

    private bool miniGameSucceeded;

    [SerializeField] private GlassSelector glassSelector;

    public event Action<RecipeData> OnRecipeChanged;

    public void StartNewOrder()
    {
        if (recipes == null || recipes.Length == 0)
        {
            Debug.LogError("No hay recetas asignadas al OrderManager.");
            return;
        }

        selectedGlass = null;
        selectedMethod = default;
        miniGameSucceeded = false;

        currentRecipe =
            recipes[UnityEngine.Random.Range(0, recipes.Length)];

        PrintRecipe();

        OnRecipeChanged?.Invoke(currentRecipe);
    }
    public void RestartOrder()
    {
        StartNewOrder();
    }
    public void SetSelectedGlass(GlassData glass)
    {
        selectedGlass = glass;
    }

    public void SetPreparationMethod(PreparationMethod method)
    {
        selectedMethod = method;
        Debug.Log("Método seleccionado: " + method);
    }

    public void SetMiniGameResult(bool success)
    {
        miniGameSucceeded = success;
        Debug.Log("MiniGameResult recibido: " + success);
    }

    private bool CheckGlass()
    {
        return selectedGlass == currentRecipe.requiredGlass;
    }

    private bool CheckIngredients()
    {
        Liquid liquid = glassSelector.CurrentGlass.Liquid;

        foreach (IngredientRequirement requirement
            in currentRecipe.ingredients)
        {
            int pouredUnits =
                liquid.GetUnits(requirement.ingredient);

            if (pouredUnits != requirement.units)
                return false;
        }

        return liquid.TotalUnits == GetRecipeTotalUnits();
    }

    private bool CheckPreparationMethod()
    {
        return selectedMethod == currentRecipe.preparationMethod;
    }

    private bool CheckMiniGame()
    {
        return miniGameSucceeded;
    }

    private bool CheckGarnishes()
    {
        IReadOnlyList<GarnishData> added =
            glassSelector.CurrentGlass.Garnishes;

        foreach (GarnishRequirement requirement in currentRecipe.garnishes)
        {
            int amount = CountGarnish(
                added,
                requirement.garnish);

            if (amount != requirement.amount)
                return false;
        }

        return added.Count == GetRecipeTotalGarnishes();
    }
    private int CountGarnish(
    IReadOnlyList<GarnishData> garnishes,
    GarnishData garnish)
    {
        int count = 0;

        foreach (GarnishData g in garnishes)
        {
            if (g == garnish)
                count++;
        }

        return count;
    }
    private int GetRecipeTotalGarnishes()
    {
        int total = 0;

        foreach (GarnishRequirement garnish in currentRecipe.garnishes)
        {
            total += garnish.amount;
        }

        return total;
    }
    private int GetRecipeTotalUnits()
    {
        int total = 0;

        foreach (IngredientRequirement ingredient
            in currentRecipe.ingredients)
        {
            total += ingredient.units;
        }

        return total;
    }
    public void EvaluateOrder()
    {
        int score = 0;

        bool glassOk = CheckGlass();
        bool ingredientsOk = CheckIngredients();
        bool methodOk = CheckPreparationMethod();
        bool miniGameOk = CheckMiniGame();
        bool garnishesOk = CheckGarnishes();

        Debug.Log("========== EVALUACIÓN ==========");

        Debug.Log($"Vaso: {(glassOk ? "Correcto" : "Incorrecto")}");
        Debug.Log($"Ingredientes: {(ingredientsOk ? "Correctos" : "Incorrectos")}");
        Debug.Log($"Método: {(methodOk ? "Correcto" : "Incorrecto")}");
        Debug.Log($"Minijuego: {(miniGameOk ? "Correcto" : "Incorrecto")}");
        Debug.Log($"Adornos: {(garnishesOk ? "Correctos" : "Incorrectos")}");

        if (glassOk)
            score++;

        if (ingredientsOk)
            score++;

        if (methodOk)
            score++;

        if (miniGameOk)
            score++;

        if (garnishesOk)
            score++;

        CocktailQuality quality = CalculateQuality(score);

        Debug.Log("--------------------------------");
        Debug.Log($"Resultado final: {quality} ({score}/5)");
        Debug.Log("================================");
    }
    private CocktailQuality CalculateQuality(int score)
    {
        if (score <= 1)
            return CocktailQuality.Bad;

        if (score <= 3)
            return CocktailQuality.Medium;

        return CocktailQuality.Excellent;
    }
    private void PrintRecipe()
    {
        Debug.Log("=================================");
        Debug.Log("NUEVO PEDIDO");
        Debug.Log("Cóctel: " + currentRecipe.recipeName);

        Debug.Log("Vaso:");
        Debug.Log("- " + currentRecipe.requiredGlass.glassName);

        Debug.Log("Ingredientes:");

        foreach (IngredientRequirement ingredient in currentRecipe.ingredients)
        {
            Debug.Log(
                $"- {ingredient.ingredient.bottleName}: {ingredient.units}");
        }

        Debug.Log("Método:");
        Debug.Log("- " + currentRecipe.preparationMethod);

        Debug.Log("Adornos:");

        foreach (GarnishRequirement garnish in currentRecipe.garnishes)
        {
            Debug.Log(
                $"- {garnish.garnish.garnishName}: {garnish.amount}");
        }

        Debug.Log("=================================");
    }
}
public enum CocktailQuality
{
    Bad,
    Medium,
    Excellent
}
