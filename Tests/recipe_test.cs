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
        Recipe firstRecipe = new Recipe("Spaghetti", "Noodles, Sauce", "Boil noodles", "20 Minutes", 5);
        Recipe secondRecipe = new Recipe("Spaghetti", "Noodles, Sauce", "Boil noodles", "20 Minutes", 5);

        Assert.Equal(firstRecipe, secondRecipe);
    }

    [Fact]
    public void Test_Save_AssignsIdToObject()
    {
        Recipe testRecipe = new Recipe("Spaghetti", "Noodles, Sauce", "Boil noodles", "20 Minutes", 5);

        testRecipe.Save();
        Recipe savedRecipe = Recipe.GetAll()[0];

        int result = savedRecipe.GetId();
        int testId = testRecipe.GetId();

        Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_FindRecipe_ReturnsSameObject()
    {
        Recipe newRecipe = new Recipe("Chicken Tandoori","Chicken,Onions,Tomato", "Roast Chicken", "30 minutes", 5);
        newRecipe.Save();

        Recipe testRecipe = Recipe.Find(newRecipe.GetId());
        Assert.Equal(testRecipe,newRecipe);
    }

    [Fact]
    public void Test_Update_UpdateRecipe()
    {
        string name = "Spaghetti";
        Recipe testRecipe1 = new Recipe(name, "Noodles, Sauce", "Boil noodles", "20 Minutes", 5);
        testRecipe1.Save();

        string newName = "Chicken Tandoori";
        string newIngredients = "Chicken,Onions,Tomato";
        string newInstructions = "Roast Chicken";
        string newCookTime = "30 minutes";
        int newRating = 5;

        testRecipe1.Update(newName, newIngredients, newInstructions, newCookTime, newRating);
        Recipe actualResult = Recipe.GetAll()[0];

        Assert.Equal(newName, actualResult.GetName());
        Assert.Equal(newIngredients, actualResult.GetIngredients());
        Assert.Equal(newInstructions, actualResult.GetInstructions());
        Assert.Equal(newCookTime, actualResult.GetTime());
        Assert.Equal(newRating, actualResult.GetRating());
    }

    public void Dispose()
    {
        Recipe.DeleteAll();
    }
  }
}
