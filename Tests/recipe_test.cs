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

    public void Dispose()
    {
        Recipe.DeleteAll();
    }
  }
}
