using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace RecipeBox
{
    public class Category
    {
        private int _id;
        private string _name;

        public Category(string Name, int Id = 0)
        {
            _id = Id;
            _name = Name;
        }

        public int GetId()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public static List<Category> GetAll()
        {
            List<Category> CategoryList = new List<Category>{};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM categories;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int categoryId = rdr.GetInt32(0);
                string categoryName = rdr.GetString(1);
                Category newCategory = new Category(categoryName, categoryId);
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

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM categories;", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
