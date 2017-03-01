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

    public void Dispose()
    {
        Recipe.DeleteAll();
    }
  }
}
