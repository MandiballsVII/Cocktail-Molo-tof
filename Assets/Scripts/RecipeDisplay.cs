using UnityEngine;
using UnityEngine.UI;

public class RecipeDisplay : MonoBehaviour
{
    [SerializeField] private Image recipeImage;

    [SerializeField] private OrderManager orderManager;

    private void OnEnable()
    {
        orderManager.OnRecipeChanged += SetRecipe;
    }

    private void OnDisable()
    {
        orderManager.OnRecipeChanged -= SetRecipe;
    }

    private void SetRecipe(RecipeData recipe)
    {
        if (recipe == null)
        {
            recipeImage.sprite = null;
            recipeImage.enabled = false;
            return;
        }

        recipeImage.sprite = recipe.previewImage;
        recipeImage.enabled = true;
    }
}