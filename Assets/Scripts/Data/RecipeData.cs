using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Cocktails/Recipe")]
public class RecipeData : ScriptableObject
{
    [Header("General")]

    public string recipeName;

    public Sprite previewImage;

    public int price;

    [Header("Glass")]

    public GlassData requiredGlass;

    [Header("Ingredients")]

    public List<IngredientRequirement> ingredients = new();

    [Header("Preparation")]

    public PreparationMethod preparationMethod;

    [Header("Garnishes")]

    public List<GarnishRequirement> garnishes = new();
}