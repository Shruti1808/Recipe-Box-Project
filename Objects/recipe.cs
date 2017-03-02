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
        private string _url;

        public Recipe(string Name, string Ingredients, string Instructions, string CookTime, int Rating, string URL, int Id = 0)
        {
            _id = Id;
            _name = Name;
            _ingredients = Ingredients;
            _instructions = Instructions;
            _cooktime = CookTime;
            _rating = Rating;
            _url = URL;
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

        public string GetUrl()
        {
            return _url;
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
                bool urlEquality = (this.GetUrl() == newRecipe.GetUrl());
                return (idEquality && nameEquality && ingredientEquality && instructionEquality && cookTimeEquality && ratingEquality && urlEquality);
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
                string recipeUrl = rdr.GetString(6);

                Recipe newRecipe = new Recipe(recipeName, recipeIngredients, recipeInstructions, recipeCookTime, recipeRating, recipeUrl, recipeId);
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

            SqlCommand cmd = new SqlCommand("INSERT into recipe(name, ingredients, instructions, cook_time, rating, url) OUTPUT INSERTED.id VALUES (@Name, @Ingredients, @Instructions, @CookTime, @Rating, @Url);", conn);

            SqlParameter recipeNameParam = new SqlParameter("@Name", this.GetName());
            SqlParameter recipeIngredientsParam = new SqlParameter("@Ingredients", this.GetIngredients());
            SqlParameter recipeInstructionsParam = new SqlParameter("@Instructions", this.GetInstructions());
            SqlParameter recipeCookTimeParam = new SqlParameter("@CookTime", this.GetTime());
            SqlParameter recipeRatingParam = new SqlParameter("@Rating", this.GetRating());
            SqlParameter recipeUrlParam = new SqlParameter("@Url", this.GetUrl());

            cmd.Parameters.Add(recipeNameParam);
            cmd.Parameters.Add(recipeIngredientsParam);
            cmd.Parameters.Add(recipeInstructionsParam);
            cmd.Parameters.Add(recipeCookTimeParam);
            cmd.Parameters.Add(recipeRatingParam);
            cmd.Parameters.Add(recipeUrlParam);

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

        public static Recipe Find(int id)
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
            string foundUrl = null;

            while(rdr.Read())
            {
                foundRecipeId = rdr.GetInt32(0);
                foundName = rdr.GetString(1);
                foundIngredients = rdr.GetString(2);
                foundInstructions = rdr.GetString(3);
                foundCookTime = rdr.GetString(4);
                foundRating = rdr.GetInt32(5);
                foundUrl = rdr.GetString(6);
            }

            Recipe foundRecipe = new Recipe(foundName, foundIngredients, foundInstructions, foundCookTime, foundRating, foundUrl, foundRecipeId);
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

    public void Update(string newName, string newIngredients, string newInstructions, string newCookTime, int newRating, string newUrl)
    {
        SqlConnection conn = DB.Connection();
        conn.Open();

        SqlCommand cmd = new SqlCommand("UPDATE recipe SET name = @NewName, ingredients = @NewIngredients, instructions = @NewInstructions, cook_time = @NewCookTime, rating = @NewRating, url=@NewUrl OUTPUT INSERTED.* WHERE id = @RecipeId;", conn);

        SqlParameter newNameParam = new SqlParameter("@NewName", newName);
        SqlParameter newIngredientsParam = new SqlParameter("@NewIngredients", newIngredients);
        SqlParameter newInstructionsParam = new SqlParameter("@NewInstructions", newInstructions);
        SqlParameter newCookTimeParam = new SqlParameter("@NewCookTime", newCookTime);
        SqlParameter newRatingParam = new SqlParameter("@NewRating", newRating);
        SqlParameter newUrlParam = new SqlParameter("@NewUrl", newUrl);
        SqlParameter idParam = new SqlParameter("@RecipeId", this.GetId());

        cmd.Parameters.Add(newNameParam);
        cmd.Parameters.Add(newIngredientsParam);
        cmd.Parameters.Add(newInstructionsParam);
        cmd.Parameters.Add(newCookTimeParam);
        cmd.Parameters.Add(newRatingParam);
        cmd.Parameters.Add(newUrlParam);
        cmd.Parameters.Add(idParam);

        SqlDataReader rdr = cmd.ExecuteReader();

        while (rdr.Read())
        {
            this._id = rdr.GetInt32(0);
            this._name = rdr.GetString(1);
            this._ingredients = rdr.GetString(2);
            this._instructions = rdr.GetString(3);
            this._cooktime = rdr.GetString(4);
            this._rating = rdr.GetInt32(5);
            this._url = rdr.GetString(6);
        }
            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
    }

    public void AddCategory(Category newCategory)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd  = new SqlCommand("INSERT INTO categories_recipe (recipe_id, category_id) VALUES (@RecipeId, @CategoryId);",conn);

            SqlParameter recipeParameter = new SqlParameter("@RecipeId", this.GetId());
            SqlParameter categoryParameter = new SqlParameter("@CategoryId", newCategory.GetId());

            cmd.Parameters.Add(recipeParameter);
            cmd.Parameters.Add(categoryParameter);

            cmd.ExecuteNonQuery();

            if (conn != null)
            {
                conn.Close();
            }
        }

        public List<Category> GetCategories()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT categories.* FROM recipe JOIN categories_recipe ON (recipe.id= categories_recipe.recipe_id) JOIN categories ON (categories_recipe.category_id = categories.id) WHERE recipe.id = @RecipeId;",conn);

            SqlParameter recipeIdParam = new SqlParameter("@RecipeId", this.GetId().ToString());
            cmd.Parameters.Add(recipeIdParam);

            SqlDataReader rdr = cmd.ExecuteReader();

            List<Category> CategoryList = new List<Category>{};

            while(rdr.Read())
            {
                int matchedCategoryId =  rdr.GetInt32(0);
                string categoryName = rdr.GetString(1);
                Category newCategory = new Category(categoryName, matchedCategoryId);
                CategoryList.Add(newCategory);
            }

            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            return CategoryList;
        }

    public void DeleteRecipe()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM recipe WHERE id = @RecipeId;", conn);

      cmd.Parameters.Add(new SqlParameter("@RecipeId", this.GetId()));
      cmd.ExecuteNonQuery();
      conn.Close();
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
