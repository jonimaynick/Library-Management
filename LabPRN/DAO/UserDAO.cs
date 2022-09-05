using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data.SqlClient;

namespace LabPRN
{
    public class UserDAO
    {
        private static string CHECKLOGIN = "SELECT UserID, UserPassword, RoleName, UserName, UserBirthday, Address, Phone, Email, Status FROM UserLibrary INNER JOIN " +
            "Role ON UserLibrary.RoleID = Role.RoleID WHERE UserID =@id  AND UserPassword =@pass ";

        public UserDTO CheckLogin(string id , string pass)
        {
            UserDTO user = null;
            using (SqlConnection connection = Utils.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CHECKLOGIN, connection))
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@pass", pass);
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user = new UserDTO(reader["UserID"].ToString(), reader["UserPassword"].ToString(), reader["RoleName"].ToString(), reader["UserName"].ToString(),
                                Convert.ToDateTime(reader["UserBirthday"]), reader["Address"].ToString(), reader["Phone"].ToString(), 
                                reader["Email"].ToString(), Convert.ToBoolean(reader["Status"]));
                        }
                    }
                }
            }
            return user;
        }
    }
}
