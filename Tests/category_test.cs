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

        [Fact]
        public void Test_FindCategory_ReturnsSameObject()
        {
            Category newCategory = new Category("Italian");
            newCategory.Save();

            Category testCategory = Category.Find(newCategory.GetId());
            Assert.Equal(testCategory,newCategory);
        }

        [Fact]
       public void Test_Update_UpdateCategory()
       {
           //Arrange
           string name = "Mexican";
           Category testCategory = new Category(name);
           testCategory.Save();
           string newName = "Indian";

           //Act
           testCategory.Update(newName);
           string result = testCategory.GetName();

           //Assert
           Assert.Equal(result, newName);
       }

       [Fact]
           public void Test_Delete_DeleteSingleCategory()
           {
             //Arrange
             Category testCategory1 = new Category("Italian");
             testCategory1.Save();

             Category testCategory2 = new Category("Indian");
             testCategory2.Save();

             //Act
             testCategory1.DeleteCategory();
             List<Category> result = Category.GetAll();
             List<Category> resultList = new List<Category> {testCategory2};

             Assert.Equal(result, resultList);
           }

        public void Dispose()
        {
            Category.DeleteAll();
        }
    }
}
