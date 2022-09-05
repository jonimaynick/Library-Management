using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;

namespace LabPRN
{
    public class BookDAO
    {
        private static string SEARCH = "SELECT BookID,Title ,Price,BookImage,BookDescribe, AuthorName,CategoryName,TotalQuantity,Status," +
            "Publisher,YearProduction " +
            "FROM Books INNER JOIN Category ON Books.CategoryID = Category.CategoryID " +
            "INNER JOIN Author ON Author.AuthorID = Books.AuthorID WHERE Title Like @title ";

        private static string BONUSEARCH = "AND Status=@status";

        private static string UPDATE = "UPDATE Books SET Title=@title ,Price= @price ,BookImage=@image,BookDescribe=@describe, AuthorID=@authorID, CategoryID=@category," +
            "TotalQuantity=@quantity,Status=@status,Publisher=@publisher,YearProduction=@year WHERE BookID=@id";

        private static string GETCATEGORYID = "SELECT CategoryID FROM Category WHERE CategoryName=@name";

        private static string GETAUTHORID = "SELECT AuthorID FROM Author WHERE AuthorName=@name";

        private static string GETAUTHORBIRTH = "SELECT AuthorBirthday FROM Author WHERE AuthorName=@name";

        private static string ADD = "INSERT INTO Books(BookID,Title ,Price, BookImage,BookDescribe,AuthorID, CategoryID, TotalQuantity, Publisher, YearProduction, Status) " +
        "VALUES(@id, @title, @price, @image, @describe,@author, @category, @quantity, @status, @publisher, @year)";

        private static string GETALLCATEGORYNAME = "SELECT CategoryName FROM Category";

        private static string GETALLAUTHORNAME = "SELECT AuthorName FROM Author";

        private static string SEARCHAUTHOR = "SELECT AuthorID, AuthorName, AuthorBirthday, BirthPlace FROM Author";

        private static string SEARCHCATEGORY = "SELECT CategoryID, CategoryName FROM Category";

        private static string UPDATEAUTHOR = "UPDATE Author SET AuthorName=@name, AuthorBirthday=@birth, BirthPlace=@place WHERE AuthorID=@id";

        private static string UPDATECATEGORY = "UPDATE Category SET CategoryName=@name  WHERE CategoryID=@id";

        private static string ADDAUTHOR = "INSERT INTO Author(AuthorID, AuthorName, AuthorBirthday, BirthPlace) " +
        "VALUES(@id, @name, @birth, @place)";

        private static string ADDCATEGORY = "INSERT INTO Category(CategoryID, CategoryName) " +
        "VALUES(@id, @name)";

        private static string GETMAXBOOK = "SELECT MAX(BookID) FROM Books";

        private static string GETMAXAUTHOR = "SELECT MAX(AuthorID) FROM Author";

        private static string GETMAXCATEGORY = "SELECT MAX(CategoryID) FROM Category";

        public static DateTime GetAuthorBirth(string AuthorName)
        {
            DateTime rs = DateTime.Now;
            using (SqlConnection connection = Utils.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(GETAUTHORBIRTH, connection))
                {
                    cmd.Parameters.AddWithValue("@name", AuthorName);
                    connection.Open();
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rs = reader.GetDateTime(0);
                        }
                    }
                }
            }
            return rs;
        }

        public static bool updateAuthor(AuthorDTO author)
        {
            bool result = false;
            using (SqlConnection connection = Utils.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(UPDATEAUTHOR, connection))
                {
                    cmd.Parameters.AddWithValue("@id", author.id);
                    cmd.Parameters.AddWithValue("@name", author.name);
                    cmd.Parameters.AddWithValue("@birth", author.birth);
                    cmd.Parameters.AddWithValue("@place", author.place);
                    connection.Open();
                    int rs = cmd.ExecuteNonQuery();
                    if (rs < 0)
                    {
                        Console.WriteLine("Error inserting data into Database!");
                    }
                    else
                    {
                        result = true;
                    }

                }
            }
            return result;
        }

        public static bool updateCategory(CategoryDTO category)
        {
            bool result = false;
            using (SqlConnection connection = Utils.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(UPDATECATEGORY, connection))
                {
                    cmd.Parameters.AddWithValue("@id", category.ID);
                    cmd.Parameters.AddWithValue("@name", category.Name);
                    connection.Open();
                    int rs = cmd.ExecuteNonQuery();
                    if (rs < 0)
                    {
                        Console.WriteLine("Error inserting data into Database!");
                    }
                    else
                    {
                        result = true;
                    }

                }
            }
            return result;
        }

        public static bool addAuthor(AuthorDTO author)
        {
            bool result = false;
            author.id = GetNewID(GETMAXAUTHOR);
            using (SqlConnection connection = Utils.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(ADDAUTHOR, connection))
                {
                    cmd.Parameters.AddWithValue("@id", author.id);
                    cmd.Parameters.AddWithValue("@name", author.name);
                    cmd.Parameters.AddWithValue("@birth", author.birth);
                    cmd.Parameters.AddWithValue("@place", author.place);
                    connection.Open();
                    int rs = cmd.ExecuteNonQuery();
                    if (rs < 0)
                    {
                        Console.WriteLine("Error inserting data into Database!");
                    }
                    else
                    {
                        result = true;
                    }

                }
            }
            return result;
        }

        public static bool addCategory(CategoryDTO category)
        {
            bool result = false;
            category.ID = GetNewID(GETMAXCATEGORY);
            using (SqlConnection connection = Utils.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(ADDCATEGORY, connection))
                {
                    cmd.Parameters.AddWithValue("@id", category.ID);
                    cmd.Parameters.AddWithValue("@name", category.Name);
                    connection.Open();
                    int rs = cmd.ExecuteNonQuery();
                    if (rs < 0)
                    {
                        Console.WriteLine("Error inserting data into Database!");
                    }
                    else
                    {
                        result = true;
                    }

                }
            }
            return result;
        }

        private static string GetNewID(string sql)
        {
            string result = null;
            using (SqlConnection connection = Utils.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = reader.GetString(0);
                        }
                    }
                }
            }
            int newID = Convert.ToInt32(result) + 1;
            result = newID.ToString();
            return result;
        }

        public static List<AuthorDTO> GetAllAuthor()
        {
            List<AuthorDTO> list = new List<AuthorDTO>();
            using (SqlConnection connection = Utils.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SEARCHAUTHOR, connection))
                {
                    connection.Open();
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new AuthorDTO(reader["AuthorID"].ToString(), reader["AuthorName"].ToString(), 
                                Convert.ToDateTime(reader["AuthorBirthday"]), reader["BirthPlace"].ToString()));
                        }
                    }
                }
            }
            return list;
        }

        public static List<CategoryDTO> GetAllCategory()
        {
            List<CategoryDTO> list = new List<CategoryDTO>();
            using (SqlConnection connection = Utils.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SEARCHCATEGORY, connection))
                {
                    connection.Open();
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new CategoryDTO(reader["CategoryID"].ToString(), reader["CategoryName"].ToString()));
                        }
                    }
                }
            }
            return list;
        }
        public static bool addBook(BookDTO book)
        {
            bool result = false;
            book.BookID = GetNewID(GETMAXBOOK);
            using (SqlConnection connection = Utils.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(ADD, connection))
                {
                    cmd.Parameters.AddWithValue("@id", book.BookID);
                    cmd.Parameters.AddWithValue("@title", book.Title);
                    cmd.Parameters.AddWithValue("@price", book.Price);
                    cmd.Parameters.AddWithValue("@image", book.Image);
                    cmd.Parameters.AddWithValue("@describe", book.Describe);
                    cmd.Parameters.AddWithValue("@author", GetAuthorByName(book.AuthorName));
                    cmd.Parameters.AddWithValue("@category", GetCategoryByName(book.Category));
                    cmd.Parameters.AddWithValue("@quantity", book.Quantity);
                    cmd.Parameters.AddWithValue("@status", true);
                    cmd.Parameters.AddWithValue("@publisher", book.Publisher);
                    cmd.Parameters.AddWithValue("@year", book.YearProduction);
                    connection.Open();
                    int rs = cmd.ExecuteNonQuery();
                    if (rs < 0)
                    {
                        Console.WriteLine("Error inserting data into Database!");
                    }
                    else
                    {
                        result = true;
                    }

                }
            }
            return result;
        }


        public static bool updateBook(BookDTO book)
        {
            bool result = false;
            using (SqlConnection connection = Utils.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(UPDATE, connection))
                {
                    cmd.Parameters.AddWithValue("@title", book.Title);
                    cmd.Parameters.AddWithValue("@price", book.Price);
                    cmd.Parameters.AddWithValue("@image", book.Image);
                    cmd.Parameters.AddWithValue("@describe", book.Describe);
                    cmd.Parameters.AddWithValue("@authorID", GetAuthorByName(book.AuthorName));
                    cmd.Parameters.AddWithValue("@category", GetCategoryByName(book.Category));
                    cmd.Parameters.AddWithValue("@quantity", book.Quantity);
                    cmd.Parameters.AddWithValue("@status", book.Status);
                    cmd.Parameters.AddWithValue("@publisher", book.Publisher);
                    cmd.Parameters.AddWithValue("@year", book.YearProduction);
                    cmd.Parameters.AddWithValue("@id", book.BookID);

                    connection.Open();
                    int rs = cmd.ExecuteNonQuery();
                    if (rs < 0)
                    {
                        Console.WriteLine("Error inserting data into Database!");
                    }
                    else
                    {
                        result = true;
                    }

                }
            }
            return result;
        }

        public static List<BookDTO> SearchBooks(string title, bool needStatus,bool status)
        {
            List<BookDTO> list = new List<BookDTO>();
            using (SqlConnection connection = Utils.GetConnection())
            {
                string sql = SEARCH;
                if(needStatus)
                {
                    sql += BONUSEARCH;
                }
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@title", $"%{title}%");
                    if (needStatus)
                    {
                        cmd.Parameters.AddWithValue("@status", status);
                    }                    
                    connection.Open();
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            double price = Convert.ToDouble(reader["Price"]);
                            int quantity = Convert.ToInt32(reader["TotalQuantity"]);
                            DateTime date = Convert.ToDateTime(reader["YearProduction"]);
                            list.Add(new BookDTO(reader["BookID"].ToString(),reader["Title"].ToString(),price,reader["BookImage"].ToString(),
                                reader["BookDescribe"].ToString(), reader["AuthorName"].ToString(), reader["CategoryName"].ToString(), quantity, reader["Publisher"].ToString(),date,
                                Convert.ToBoolean(reader["Status"])));
                        }
                    }
                }
            }
            return list;
        }

        private static string GetCategoryByName(string categoryName)
        {
            string result = null;
            using (SqlConnection connection = Utils.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(GETCATEGORYID, connection))
                {
                    cmd.Parameters.AddWithValue("@name", categoryName);
                    connection.Open();
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = reader.GetString(0);
                        }
                    }
                }
            }
            return result;
        }
        private static string GetAuthorByName(string AuthorName)
        {
            string result = null;
            using (SqlConnection connection = Utils.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(GETAUTHORID, connection))
                {
                    cmd.Parameters.AddWithValue("@name", AuthorName);
                    connection.Open();
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = reader.GetString(0);
                        }
                    }
                }
            }
            return result;
        }

        public static List<string> GetListAuthorCategory(string type)
        {
            string sql = GETALLAUTHORNAME;
            if (type.Equals("category"))
            {
                sql = GETALLCATEGORYNAME;
            }
            List<string> list = new List<string>();
            using (SqlConnection connection = Utils.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(reader.GetString(0));
                        }
                    }
                }
            }
            return list;
        }
    }
    class Utils
    {
        static SqlConnection databaseConnection = null;
        public static SqlConnection GetConnection()
        {
            if (databaseConnection == null)
            {
                databaseConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString);
            }
            else
            {
                databaseConnection.Close();
                databaseConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString);
            }
            return databaseConnection;
        }
    }
}
