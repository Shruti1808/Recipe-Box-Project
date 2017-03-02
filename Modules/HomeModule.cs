using System;
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

            //For Recipes..............>

            Get["/recipes"] = _ => {
                List<Recipe> AllRecipes = Recipe.GetAll();
                return View["recipes.cshtml", AllRecipes];
            };

            Get["/recipe/new"] = _ => {
                List<Category> AllCategories = Category.GetAll();
                return View["recipe_form.cshtml",AllCategories];
            };

            Post["/recipes"] = _ => {
                Recipe newRecipe = new Recipe(Request.Form["recipe-name"], Request.Form["ingredients"], Request.Form["instructions"], Request.Form["cook-time"], Request.Form["rating"], Request.Form["recipe-url"]);
                newRecipe.Save();
                Console.WriteLine("The URL is: " + newRecipe.GetUrl());
                newRecipe.AddCategory(Category.Find(Request.Form["category-id"]));
                List<Recipe> AllRecipes = Recipe.GetAll();
                return View["recipes.cshtml", AllRecipes];
            };

            Get["/recipe/{id}"] = parameters => {
              Dictionary<string, object> model = new Dictionary<string, object>();
              var SelectedRecipe = Recipe.Find(parameters.id);
              var CategoryRecipe = SelectedRecipe.GetCategories();
              model.Add("recipe", SelectedRecipe);
              model.Add("categories", CategoryRecipe);
              return View["recipe_details.cshtml", model];
            };

            Get["/recipe/edit/{id}"] = parameters => {
                Recipe SelectedRecipe = Recipe.Find(parameters.id);
                return View["recipe_edit.cshtml", SelectedRecipe];
            };
            Patch["/recipe/edit/{id}"] = parameters => {
                Recipe SelectedRecipe = Recipe.Find(parameters.id);
                SelectedRecipe.Update(Request.Form["recipe-name"], Request.Form["ingredients"], Request.Form["instructions"], Request.Form["cook-time"], Request.Form["rating"], Request.Form["recipe-url"]);
                List<Recipe> AllRecipes = Recipe.GetAll();
                return View["recipes.cshtml", AllRecipes];
            };

            Post["/recipes/delete"] = _ => {
                Recipe.DeleteAll();
                List<Recipe> AllRecipes = Recipe.GetAll();
                return View["recipes.cshtml",AllRecipes];
            };

            Get["recipe/delete/{id}"] = parameters => {
                Recipe SelectedRecipe = Recipe.Find(parameters.id);
                return View["recipe_delete.cshtml", SelectedRecipe];
            };
            Delete["recipe/delete/{id}"] = parameters => {
                Recipe SelectedRecipe = Recipe.Find(parameters.id);
                SelectedRecipe.DeleteRecipe();
                List<Recipe> AllRecipes = Recipe.GetAll();
                return View["recipes.cshtml",AllRecipes];
            };

            //For Categories......................>

            Get["/categories"] = _ => {
                List<Category> AllCategories = Category.GetAll();
                return View["categories.cshtml", AllCategories];
            };

            Get["/category/new"] = _ => {
                List<Recipe> AllRecipes = Recipe.GetAll();
                return View["category_form.cshtml", AllRecipes];
            };

            Post["/categories"] = _ => {
                Category newCategory = new Category(Request.Form["category-name"]);
                newCategory.Save();
                newCategory.AddRecipe(Recipe.Find(Request.Form["recipe-id"]));
                List<Category> AllCategories = Category.GetAll();
                return View["categories.cshtml", AllCategories];
            };

            Get["/category/{id}"] = parameters => {
              Dictionary<string, object> model = new Dictionary<string, object>();
              var SelectedCategory = Category.Find(parameters.id);
              var CategoryRecipe = SelectedCategory.GetRecipe();
              model.Add("category", SelectedCategory);
              model.Add("recipe", CategoryRecipe);
              return View["categories_details.cshtml", model];
            };


            Get["/category/edit/{id}"] = parameters => {
                Category SelectedCategory = Category.Find(parameters.id);
                return View["category_edit.cshtml", SelectedCategory];
            };
            Patch["/category/edit/{id}"] = parameters => {
                Category SelectedCategory = Category.Find(parameters.id);
                SelectedCategory.Update(Request.Form["category-name"]);
                List<Category> AllCategories = Category.GetAll();
                return View["categories.cshtml", AllCategories];
            };

            Post["/categories/delete"] = _ => {
                Category.DeleteAll();
                List<Category> AllCategories = Category.GetAll();
                return View["categories.cshtml", AllCategories];
            };

            Get["category/delete/{id}"] = parameters => {
                Category SelectedCategory = Category.Find(parameters.id);
                return View["category_delete.cshtml", SelectedCategory];
            };
            Delete["category/delete/{id}"] = parameters => {
                Category SelectedCategory = Category.Find(parameters.id);
                SelectedCategory.DeleteCategory();
                List<Category> AllCategories = Category.GetAll();
                return View["categories.cshtml", AllCategories];
            };

        }
    }
}
