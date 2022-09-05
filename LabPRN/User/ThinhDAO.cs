using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace LabPRN
{
    public class Book
    {
        public string BookID { get; set; }
        public string BookName { get; set; }
        public double Price { get; set; }
        public string BookImage { get; set; }
        public string Describe { get; set; }
        public int TotalQuantity { get; set; }
        public bool status { get; set; }
        public string Publisher { get; set; }
        public DateTime YearProduction { get; set; }
        public string AuthorID { get; set; }
        public string CategoryID { get; set; }
    }
    public class HistoryBorrowedBook
    {
        public int CodeID { get; set; }
        public decimal BookQuantity { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime DueDate { get; set; }
        public string BookID { get; set; }
        public string UserID { get; set; }
        public bool Status { get; set; }

    }
    public class SmartLibrayDAO
    {
        string ConnectionString;

        public SmartLibrayDAO(string Str)
        {
            ConnectionString = Str;
        }

        //--------------------------------------------------------------------
        //Dao Category Table

        public string GetCategoryNameByID(string id)
        {
            string result = "";
            SqlConnection conn = new SqlConnection(ConnectionString);
            string sql = "Select CategoryName From Category Where CategoryID = @ID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ID", id);
            SqlDataReader reader;
            conn.Open();
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    result = reader.GetString(0);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return result;

        }

        public List<string> GetListCategory(string str)
        {
            List<string> list = new List<string>();
            list.Add(str);
            SqlConnection conn = new SqlConnection(ConnectionString);
            string sql = "SELECT CategoryName FROM Category";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader;
            conn.Open();
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        list.Add(reader[0].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return list;
        }
        //-----------------------------------------------------------------------
        //Dao Books Table

        public DataTable GetBooks()
        {
            DataTable books = new DataTable();
            SqlConnection conn = new SqlConnection(ConnectionString);
            string sql = "SELECT  BookID, Title, Price, BookImage, BookDescribe, AuthorName, CategoryName," +
                "TotalQuantity, Status, Publisher, YearProduction\n" +
                "FROM Books B INNER JOIN Category C ON B.CategoryID = C.CategoryID \n" +
                             "Inner JOIN Author A ON B.AuthorID = A.AuthorID";

            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            adapter.Fill(books);

            return books;
        }

        public string getStringImageResource(string ID)
        {
            string result = "";
            SqlConnection conn = new SqlConnection(ConnectionString);
            string sql = "Select BookImage from Books where BookID = @ID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ID", ID);
            SqlDataReader reader;
            conn.Open();
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    result = reader[0].ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public DataTable SearchByName(string Name)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(ConnectionString);
            string sql = "SELECT  BookID, Title, Price, BookImage, BookDescribe, AuthorName, CategoryName," +
                "TotalQuantity, Status, Publisher, YearProduction\n" +
                "FROM Books B INNER JOIN Category C ON B.CategoryID = C.CategoryID \n" +
                             "Inner JOIN Author A ON B.AuthorID = A.AuthorID \n" +
                "Where Title LIKE N'%" + Name + "%'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            adapter.Fill(dt);
            return dt;
        }



        public DataTable GetListBookByCategory(string CateName)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(ConnectionString);
            string sql = "SELECT  BookID, Title, Price, BookImage, BookDescribe, AuthorName, CategoryName," +
                "TotalQuantity, Status, Publisher, YearProduction\n" +
                "FROM Books B INNER JOIN Category C ON B.CategoryID = C.CategoryID \n" +
                             "Inner JOIN Author A ON B.AuthorID = A.AuthorID \n" +
                "Where CategoryName LIKE N'%" + CateName + "%'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return dt;
        }

        public Book getBookByID(string id)
        {
            Book result = null;
            SqlConnection conn = new SqlConnection(ConnectionString);
            string sql = "Select * from Books where BookID = @ID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ID", id);
            SqlDataReader reader;
            conn.Open();
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    string bookID = reader["BookID"].ToString();
                    string bookName = reader["Title"].ToString();
                    double price = double.Parse(reader["Price"].ToString());
                    string bookImage = reader["BookImage"].ToString();
                    string bookDescribe = reader["BookDescribe"].ToString();
                    int Quantity = int.Parse(reader["totalQuantity"].ToString());
                    bool status = bool.Parse(reader["Status"].ToString());
                    string publisher = reader["Publisher"].ToString();
                    DateTime yaerPublic = DateTime.Parse(reader["YearProduction"].ToString());
                    string authorID = reader["AuthorID"].ToString();
                    string categoryID = reader["CategoryID"].ToString();

                    result = new Book()
                    {
                        BookID = bookID,
                        BookName = bookName,
                        BookImage = bookImage,
                        AuthorID = authorID,
                        CategoryID = categoryID,
                        Describe = bookDescribe,
                        Price = price,
                        Publisher = publisher,
                        status = status,
                        TotalQuantity = Quantity,
                        YearProduction = yaerPublic
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        //-----------------------------------------------------------
        //Dao Author Table

        public string GetAuthorNameByID(string id)
        {
            string result = "";
            SqlConnection conn = new SqlConnection(ConnectionString);
            string sql = "Select AuthorName From Author Where AuthorID = @ID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ID", id);
            SqlDataReader reader;
            conn.Open();
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    result = reader.GetString(0);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return result;

        }
        //----------------------------------------------------------------------
        //Dao for History table

        public bool addnewBorrowing(string BookID, string UserID, decimal Quantity, DateTime StartDate, DateTime DueDate)
        {
            bool result = false;
            SqlConnection conn = new SqlConnection(ConnectionString);
            string sql = "Insert into History(BookID, UserID, BorrowedQuantity, ReleaseDate, DueDate) values(@BookID, @UserID, @Quantity, @StartDate, @DueDate)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@BookID", BookID);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@Quantity", Quantity);
            cmd.Parameters.AddWithValue("@StartDate", StartDate);
            cmd.Parameters.AddWithValue("@DueDate", DueDate);
            conn.Open();
            try
            {
                result = cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public DataTable GetListHistoryBorrowed(string ID)
        {
            DataTable result = new DataTable();
            SqlConnection conn = new SqlConnection(ConnectionString);
            string sql = "SELECT CodeID ,B.BookID, Title, BorrowedQuantity, ReleaseDate, DueDate, H.Status AS Return_Status \n" +
                "FROM History H INNER JOIN Books B ON H.BookID = B.BookID \n" +
                        "INNER JOIN UserLibrary U ON H.UserID = U.UserID \n" +
                "Where U.UserID = @ID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ID", ID);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                adapter.Fill(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return result;
        }

        public HistoryBorrowedBook GetHistoryBorrowedBook(string ID)
        {
            HistoryBorrowedBook result = null;
            SqlConnection conn = new SqlConnection(ConnectionString);
            string sql = "Select * From History Where CodeID = @ID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ID", ID);
            SqlDataReader reader;
            conn.Open();
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    int id = int.Parse(reader["CodeID"].ToString());
                    string bookID = reader["BookID"].ToString();
                    string userID = reader["UserID"].ToString();
                    decimal quantity = decimal.Parse(reader["BorrowedQuantity"].ToString());
                    DateTime startDate = DateTime.Parse(reader["ReleaseDate"].ToString());
                    DateTime dueDate = DateTime.Parse(reader["DueDate"].ToString());
                    bool status = bool.Parse(reader["Status"].ToString());
                    result = new HistoryBorrowedBook()
                    {
                        CodeID = id,
                        BookID = bookID,
                        UserID = userID,
                        BookQuantity = quantity,
                        DueDate = dueDate,
                        ReleaseDate = startDate,
                        Status = status
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public string GetStatus(string ID)
        {

            if (ID.Length > 0)
            {
                HistoryBorrowedBook h = this.GetHistoryBorrowedBook(ID);
                if (h.Status == true)
                {
                    return "Done";
                }
                else
                {
                    TimeSpan t = h.DueDate - DateTime.Now;
                    double diff = t.TotalDays;
                    if (diff <= 0)
                    {
                        return "Expired";
                    }
                    else if (diff <= 7)
                    {
                        return "Due soon";
                    }
                    else
                    {
                        return "Available";
                    }
                }

            }
            return "";
        }

        public DataTable SearchHistoryByName(string name, string ID)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(ConnectionString);
            string sql = "SELECT CodeID ,B.BookID, Title, BorrowedQuantity, ReleaseDate, DueDate \n" +
                "FROM History H INNER JOIN Books B ON H.BookID = B.BookID \n" +
                        "INNER JOIN UserLibrary U ON H.UserID = U.UserID \n" +
                "Where U.UserID = '" + ID + "' AND Title LIKE N'%" + name + "%'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ID", ID);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return dt;
        }

        public bool UpdateQuantityBook(string ID, decimal Quantity)
        {
            bool result = false;
            SqlConnection conn = new SqlConnection(ConnectionString);
            string sql = "Update Books Set TotalQuantity -= @Quantity Where BookID = @ID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Parameters.AddWithValue("@Quantity", Quantity);
            conn.Open();
            try
            {
                result = cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        
    }
}
