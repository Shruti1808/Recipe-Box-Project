using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace RecipeBox
{
    public class RecipeTest : IDisposable
    {
        public RecipeTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=recipebox_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void Test_DatabaseIsEmptyAtFirst()
        {
            int result = Recipe.GetAll().Count;

            Assert.Equal(0, result);
        }

        [Fact]
        public void Test_Equal_ReturnsTrueIfDetailsAreTheSame()
        {
            string url = "www.epicodus.com";
            Recipe firstRecipe = new Recipe("Spaghetti", "Noodles, Sauce", "Boil noodles", "20 Minutes", 5, url);
            Recipe secondRecipe = new Recipe("Spaghetti", "Noodles, Sauce", "Boil noodles", "20 Minutes", 5, url);

            Assert.Equal(firstRecipe, secondRecipe);
        }

        [Fact]
        public void Test_Save_AssignsIdToObject()
        {
            string url = "www.epicodus.com";
            Recipe testRecipe = new Recipe("Spaghetti", "Noodles, Sauce", "Boil noodles", "20 Minutes", 5, url);

            testRecipe.Save();
            Recipe savedRecipe = Recipe.GetAll()[0];

            int result = savedRecipe.GetId();
            int testId = testRecipe.GetId();

            Assert.Equal(testId, result);
        }

        [Fact]
        public void Test_FindRecipe_ReturnsSameObject()
        {
            string url = "www.epicodus.com";
            Recipe newRecipe = new Recipe("Chicken Tandoori","Chicken,Onions,Tomato", "Roast Chicken", "30 minutes", 5, url);
            newRecipe.Save();

            Recipe testRecipe = Recipe.Find(newRecipe.GetId());
            Assert.Equal(testRecipe,newRecipe);
        }

        [Fact]
        public void Test_Update_UpdateRecipe()
        {
            string url = "www.epicodus.com";
            string name = "Spaghetti";
            Recipe testRecipe1 = new Recipe(name, "Noodles, Sauce", "Boil noodles", "20 Minutes", 5, url);
            testRecipe1.Save();

            string newName = "Chicken Tandoori";
            string newIngredients = "Chicken,Onions,Tomato";
            string newInstructions = "Roast Chicken";
            string newCookTime = "30 minutes";
            int newRating = 5;
            string newUrl = "www.google.com";

            testRecipe1.Update(newName, newIngredients, newInstructions, newCookTime, newRating, newUrl);
            Recipe actualResult = Recipe.GetAll()[0];

            Assert.Equal(newName, actualResult.GetName());
            Assert.Equal(newIngredients, actualResult.GetIngredients());
            Assert.Equal(newInstructions, actualResult.GetInstructions());
            Assert.Equal(newCookTime, actualResult.GetTime());
            Assert.Equal(newRating, actualResult.GetRating());
            Assert.Equal(newUrl, actualResult.GetUrl());
        }

        [Fact]
        public void Test_AddCategory_AddCategoryToRecipe()
        {
            string url = "www.epicodus.com";
            Recipe testRecipe = new Recipe("Chicken Tandoori","Chicken,Onions,Tomato", "Roast Chicken", "30 minutes", 5, url);
            testRecipe.Save();

            Category testCategory =  new Category("Mexican");
            testCategory.Save();

            Category testCategory2 = new Category("Italian");
            testCategory2.Save();

            testRecipe.AddCategory(testCategory);
            testRecipe.AddCategory(testCategory2);

            List<Category> testList = new List<Category> {testCategory, testCategory2};
            List<Category> result = testRecipe.GetCategories();

            Assert.Equal(result, testList);
        }


        [Fact]
        public void Test_Delete_DeleteSingleRecipe()
        {
            //Arrange
            string url = "www.epicodus.com";
            Recipe testRecipe1 = new Recipe("Spaghetti", "Noodles, Sauce", "Boil noodles", "20 Minutes", 5, url);
            testRecipe1.Save();

            Recipe testRecipe2 = new Recipe ("Spaghetti", "Noodles, Sauce", "Boil noodles", "20 Minutes", 5, url);
            testRecipe2.Save();

            //Act
            testRecipe1.DeleteRecipe();
            List<Recipe> result = Recipe.GetAll();
            List<Recipe> resultList = new List<Recipe> {testRecipe2};

            Assert.Equal(result, resultList);
        }

        public void Dispose()
        {
            Recipe.DeleteAll();
            Category.DeleteAll();
        }
    }
}
