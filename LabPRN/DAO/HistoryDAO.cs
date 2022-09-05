using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;

namespace LabPRN
{
    public class HistoryDAO
    {
        private static string SEARCH = "SELECT CodeID, Title , UserName , BorrowedQuantity, ReleaseDate, DueDate, History.Status FROM History " +
            "INNER JOIN Books ON History.BookID = Books.BookID INNER JOIN UserLibrary ON History.UserID = UserLibrary.UserID WHERE UserName LIKE @name";
        private static string BONUSSEARCH = " AND History.Status = @status ";
        private static string CONFIRMRETURN = "UPDATE History SET Status=@status WHERE CodeID=@id";


        public static bool confirmReturn(HistoryDTO htr)
        {
            bool result = false;
            using (SqlConnection connection = Utils.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CONFIRMRETURN, connection))
                {
                    cmd.Parameters.AddWithValue("@id", htr.Id);
                    cmd.Parameters.AddWithValue("@status", true);
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

        public static List<HistoryDTO> SearchBooks(string name, bool needStatus, bool status)
        {
            List<HistoryDTO> list = new List<HistoryDTO>();
            using (SqlConnection connection = Utils.GetConnection())
            {
                string sql = SEARCH;
                if (needStatus)
                {
                    sql += BONUSSEARCH;
                }
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@name", $"%{name}%");
                    if (needStatus)
                    {
                        cmd.Parameters.AddWithValue("@status", status);
                    }
                    connection.Open();
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bool sta = Convert.ToBoolean(reader["Status"]);
                            string staString = "Unpaid";
                            if (sta) {
                                staString = "Paid";
                            }

                            list.Add(new HistoryDTO(Convert.ToInt32(reader["CodeID"]), reader["Title"].ToString(), reader["UserName"].ToString(), 
                                Convert.ToInt32(reader["BorrowedQuantity"]), Convert.ToDateTime(reader["ReleaseDate"]), Convert.ToDateTime(reader["DueDate"]),staString ));
                        }
                    }
                }
            }
            return list;
        }

    }
}
