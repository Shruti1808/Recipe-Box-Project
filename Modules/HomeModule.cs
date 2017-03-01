using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace RecipeBox
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] =_=> {
                return View ["index.cshtml"];
            };

            Get["/recipes"] = _ => {
                List<Recipe> AllRecipes = Recipe.GetAll();
                return View["recipes.cshtml", AllRecipes];
            };

            Get["/categories"] = _ => {
                List<Category> AllCategories = Category.GetAll();
                return View["categories.cshtml", AllCategories];
            };

            Get["/recipe/new"] = _ => {
                return View["recipe_form.cshtml"];
            };

            Post["/recipes"] = _ => {
                List<Recipe> AllRecipes = Recipe.GetAll();
                Recipe newRecipe = new Recipe(Request.Form["recipe-name"], Request.Form["ingredients"], Request.Form["instructions"], Request.Form["cook-time"], Request.Form["rating"]);
                newRecipe.Save();
                return View["recipes.cshtml", AllRecipes];
            };

            Get["/category/new"] = _ => {
                return View["category_form.cshtml"];
            };

            Post["/categories"] = _ => {
                Category newCategory = new Category(Request.Form["category-name"]);
                newCategory.Save();
                return View["categories.cshtml"];
            };

        }
    }
}
