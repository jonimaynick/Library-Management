using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace LabPRN
{
    public class UserDB
    {
        string connectionSrt;
        public UserDB()
        {
            getConnectionString();
        }

        public string getConnectionString()
        {
            connectionSrt = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            return connectionSrt;
        }


        public DataTable getUser()
        {
            string sql = "select l.UserID, l.UserPassword, r.RoleName, l.UserName, " +
                "l.UserBirthday, l.Address, l.Phone, l.Email,  (CASE WHEN l.status = '1' THEN 'Active' ELSE 'Deactivate' END) AS Status " +
                "from UserLibrary l, Role r " +
                "where l.RoleID = r.RoleID";
            SqlConnection cn = new SqlConnection(connectionSrt);
            SqlCommand cmd = new SqlCommand(sql, cn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtProduct = new DataTable();
            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }
                da.Fill(dtProduct);
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
            finally
            {
                cn.Close();
            }
            return dtProduct;
        }

        public DataTable ShowRoleComboBox()
        {
            string sql = "Select RoleName from Role";
            SqlConnection cn = new SqlConnection(connectionSrt);
            DataTable dtRole = new DataTable();
            SqlCommand cmd = new SqlCommand(sql, cn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cn.Open();
            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }
                da.Fill(dtRole);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cn.Close();
            }
            return dtRole;
        }

        public DataTable ShowStatusComboBox()
        {
            string sql = "Select DISTINCT (CASE WHEN Status = '1' THEN 'Active' ELSE 'Deactivate' END) AS Status from UserLibrary";
            SqlConnection cn = new SqlConnection(connectionSrt);
            DataTable dtStatus = new DataTable();
            SqlCommand cmd = new SqlCommand(sql, cn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cn.Open();
            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }
                da.Fill(dtStatus);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cn.Close();
            }
            return dtStatus;
        }

        private string ReturnRoleID(string roleName)
        {
            string sql = "SELECT RoleID FROM Role WHERE RoleName=@name";
            string result = null;
            using (SqlConnection connection = new SqlConnection(connectionSrt))
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@name", roleName);
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

        public bool addNewUser(UserDTO user)
        {
            bool result;
            SqlConnection cn = new SqlConnection(connectionSrt);
            string sql = "INSERT INTO UserLibrary (UserID, UserPassword, RoleID, UserName, UserBirthday, Address, Phone, Email, Status) " +
                "VALUES(@id, @pass, @role, @user, @birth,@address, @phone, @email, @status)";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@id", user.userID);
            cmd.Parameters.AddWithValue("@pass", user.password);
            cmd.Parameters.AddWithValue("@role", this.ReturnRoleID(user.roleName));
            //cmd.Parameters.AddWithValue("@Role", user.Role);
            cmd.Parameters.AddWithValue("@user", user.name);
            cmd.Parameters.AddWithValue("@birth", user.birthday);
            cmd.Parameters.AddWithValue("@address", user.address);
            cmd.Parameters.AddWithValue("@phone", user.phone);
            cmd.Parameters.AddWithValue("@email", user.email);
            cmd.Parameters.AddWithValue("@status", user.status);
            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }
                result = cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
            finally
            {
                cn.Close();
            }
            return result;
        }


        public bool updateUser(UserDTO user)
        {
            bool result;
            SqlConnection cn = new SqlConnection(connectionSrt);
            string sql = "update UserLibrary set UserPassword=@pass,RoleID=@role,UserName=@user, UserBirthday=@birth, Address=@address," +
                " Phone=@phone, Email=@email, Status = @status where UserID=@id";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@id", user.userID);
            cmd.Parameters.AddWithValue("@pass", user.password);
            cmd.Parameters.AddWithValue("@role", this.ReturnRoleID(user.roleName));
            cmd.Parameters.AddWithValue("@user", user.name);
            cmd.Parameters.AddWithValue("@birth", user.birthday);
            cmd.Parameters.AddWithValue("@address", user.address);
            cmd.Parameters.AddWithValue("@phone", user.phone);
            cmd.Parameters.AddWithValue("@email", user.email);
            cmd.Parameters.AddWithValue("@status", user.status);
            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }
                result = cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
            finally
            {
                cn.Close();
            }
            return result;
        }


        public bool deleteUser(string userId)
        {
            bool result;
            SqlConnection cn = new SqlConnection(connectionSrt);
            string sql = "delete UserLibrary where UserID=@id";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@id", userId);
            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }
                result = cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
            finally
            {
                cn.Close();
            }
            return result;
        }
    }
}
