using System;
using System.Collections.Generic;

namespace web_blazor.Services;
public class BurgerService
{
    public List<IngredientModel> lstIngre = new List<IngredientModel>()
    {
        new IngredientModel(){Name ="salad",UnitPrice= 10, Quantity=1},
        new IngredientModel(){Name ="cheese",UnitPrice= 20, Quantity=1},
        new IngredientModel(){Name ="beef",UnitPrice= 55, Quantity=1},
    };
    public void AddIngredient(string ingredientName)
    {
        var ingredient = lstIngre.Find(x => x.Name == ingredientName);
        if (ingredient != null)
        {
            ingredient.Quantity++;
            SetStateHasChange();
        }
    }
    public void RemoveIngredient(string ingredientName)
    {
        var ingredient = lstIngre.Find(x => x.Name == ingredientName);
        if (ingredient != null && ingredient.Quantity > 0)
        {
            ingredient.Quantity--;
            SetStateHasChange();
        } else ingredient.Quantity = 0;
    }
    public event Action OnChange;
    public void SetStateHasChange() => OnChange?.Invoke();
}