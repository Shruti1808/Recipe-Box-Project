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

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT into recipe(name, ingredients, instructions, cook_time, rating) OUTPUT INSERTED.id VALUES (@Name, @Ingredients, @Instructions, @CookTime, @Rating);", conn);

            SqlParameter recipeNameParam = new SqlParameter("@Name", this.GetName());
            SqlParameter recipeIngredientsParam = new SqlParameter("@Ingredients", this.GetIngredients());
            SqlParameter recipeInstructionsParam = new SqlParameter("@Instructions", this.GetInstructions());
            SqlParameter recipeCookTimeParam = new SqlParameter("@CookTime", this.GetTime());
            SqlParameter recipeRatingParam = new SqlParameter("@Rating", this.GetRating());

            cmd.Parameters.Add(recipeNameParam);
            cmd.Parameters.Add(recipeIngredientsParam);
            cmd.Parameters.Add(recipeInstructionsParam);
            cmd.Parameters.Add(recipeCookTimeParam);
            cmd.Parameters.Add(recipeRatingParam);

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this._id = rdr.GetInt32(0);
            }
            if (rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
        }

        public static  Recipe Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd  = new SqlCommand("SELECT * FROM recipe WHERE id= @RecipeId;", conn);

            SqlParameter idParam = new SqlParameter();
            idParam.ParameterName = "@RecipeId";
            idParam.Value = id.ToString();
            cmd.Parameters.Add(idParam);

            SqlDataReader rdr = cmd.ExecuteReader();

            int foundRecipeId = 0;
            string foundName = null;
            string foundIngredients = null;
            string foundInstructions = null;
            string foundCookTime = null;
            int foundRating = 0;

            while(rdr.Read())
            {
                foundRecipeId = rdr.GetInt32(0);
                foundName = rdr.GetString(1);
                foundIngredients = rdr.GetString(2);
                foundInstructions = rdr.GetString(3);
                foundCookTime = rdr.GetString(4);
                foundRating = rdr.GetInt32(5);
            }

            Recipe foundRecipe = new Recipe(foundName, foundIngredients, foundInstructions, foundCookTime, foundRating, foundRecipeId);
            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }

            return foundRecipe;
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
