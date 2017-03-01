using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace RecipeBox
{
    public class CategoryTest : IDisposable
    {
        public CategoryTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=recipebox_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void Test_DatabaseIsEmptyAtFirst()
        {
            int result = Category.GetAll().Count;

            Assert.Equal(0, result);
        }

        [Fact]
        public void Test_Equal_ReturnsTrueIfDetailsAreTheSame()
        {
            //Arrange, Act
            Category firstCategory = new Category("Italian");
            Category secondCategory = new Category("Italian");

            //Assert
            Assert.Equal(firstCategory, secondCategory);
        }

        [Fact]
        public void Test_Save_AssignsIdToObject()
        {
            //Arrange

            Category testCategory = new Category("Italian");

            //Act
            testCategory.Save();
            Category savedCategory = Category.GetAll()[0];

            int result = savedCategory.GetId();
            int testId = testCategory.GetId();

            //Assert
            Assert.Equal(testId, result);
        }


        public void Dispose()
        {
            Category.DeleteAll();
        }
    }
}