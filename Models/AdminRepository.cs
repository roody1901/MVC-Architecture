using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace BankingSystem.Models
{
    public class AdminRepository:IAdminRepository
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        const string connectionString = "Data Source =.;Initial Catalog = master; integrated security = true";
        
        public bool AddNewAdmin(Admin admins)
        {

            try
            {

                con = new SqlConnection(connectionString);
                cmd = new SqlCommand("insertadmin", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", admins.Id);
                cmd.Parameters.AddWithValue("@passwords", admins.Passwords);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return true;

            

        }

        public bool CheckLogin(Admin admins)
        {
            var result = false;
            con = new SqlConnection(connectionString);
            cmd = new SqlCommand("CheckLogin", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", admins.Id);
            cmd.Parameters.AddWithValue("@passwords", admins.Passwords);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if(dr.HasRows)
                {
                    result = true;
                }

            }
            return result;

        }

        public bool CheckIdAdmin(int id)
        {
            var result = false;
            con = new SqlConnection(connectionString);
            cmd = new SqlCommand("CheckId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
         
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (dr.HasRows)
                {
                    result = true;
                }

            }
            return result;

        }
    }
}
