using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace RecipeBox
{
    public class Recipe
    {
        private int _id;
        private string _name;
        private string _ingredients;
        private string _instructions;
        private string _cooktime;
        private int _rating;

        public Recipe(string Name, string Ingredients, string Instructions, string CookTime, int Rating, int Id = 0)
        {
            _id = Id;
            _name = Name;
            _ingredients = Ingredients;
            _instructions = Instructions;
            _cooktime = CookTime;
            _rating = Rating;
        }

        public int GetId()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public string GetIngredients()
        {
            return _ingredients;
        }
        public string GetInstructions()
        {
            return _instructions;
        }

        public string GetTime()
        {
            return _cooktime;
        }

        public int GetRating()
        {
            return _rating;
        }

        public override bool Equals(System.Object otherRecipe)
        {
            if (!(otherRecipe is Recipe))
            {
                return false;
            }
            else
            {
                Recipe newRecipe = (Recipe) otherRecipe;
                bool idEquality = (this.GetId() == newRecipe.GetId());
                bool nameEquality = (this.GetName()== newRecipe.GetName());
                bool ingredientEquality = (this.GetIngredients() == newRecipe.GetIngredients());
                bool instructionEquality = (this.GetInstructions() == newRecipe.GetInstructions());
                bool cookTimeEquality = (this.GetTime() == newRecipe.GetTime());
                bool ratingEquality = (this.GetRating() == newRecipe.GetRating());
                return (idEquality && nameEquality && ingredientEquality && instructionEquality && cookTimeEquality && ratingEquality);
            }
        }

        public static List<Recipe> GetAll()
        {
            List<Recipe> RecipeList = new List<Recipe>{};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM recipe;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int recipeId = rdr.GetInt32(0);
                string recipeName = rdr.GetString(1);
                string recipeIngredients = rdr.GetString(2);
                string recipeInstructions = rdr.GetString(3);
                string recipeCookTime = rdr.GetString(4);
                int recipeRating = rdr.GetInt32(5);
                Recipe newRecipe = new Recipe(recipeName, recipeIngredients, recipeInstructions, recipeCookTime, recipeRating, recipeId);
                RecipeList.Add(newRecipe);
            }

            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            return RecipeList;
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM recipe;", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }



    }
}
