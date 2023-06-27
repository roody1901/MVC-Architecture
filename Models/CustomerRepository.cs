using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
namespace BankingSystem.Models
{
    public class CustomerRepository:ICustomerRepository
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        const string connectionString = "Data Source =.;Initial Catalog = master; integrated security = true";
        public static List<Customer> _customerList;

    
        public static string ConnectionString => connectionString;


        //ADD EMPLOYEE CODE
        public bool AddCustomer(Customer cust)
        {
            try
            {

                con = new SqlConnection(connectionString);
                cmd = new SqlCommand("InsertCustomer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", cust.Id);
                cmd.Parameters.AddWithValue("@fullname", cust.Fullname);
                cmd.Parameters.AddWithValue("@address", cust.Adress);
                cmd.Parameters.AddWithValue("@email", cust.Email);
                cmd.Parameters.AddWithValue("@contact", cust.Contact);
                cmd.Parameters.AddWithValue("@atmpin", cust.Atmpin);
                cmd.Parameters.AddWithValue("@acnttype", cust.AccountType);
                cmd.Parameters.AddWithValue("@acntnum", cust.AccountNumber);
                cmd.Parameters.AddWithValue("@bal", cust.Balance);

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

       
        //GET ALL EMPLOYEE CODE
        public List<Customer> GetAll()
        {
           
            con = new SqlConnection(connectionString);
            List<Customer> CustList = new List<Customer>();
            try
            {
                con.Open();

                cmd = new SqlCommand("GetAllCustomer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CustList.Add(new Customer
                    {
                        Id = Convert.ToInt16(dr["Id"]),
                        Fullname = Convert.ToString(dr["Fullname"]),
                        Adress = Convert.ToString(dr["Adress"]),
                        Email = Convert.ToString(dr["Email"]),
                        Contact = Convert.ToString(dr["Contact"]),
                        Atmpin = Convert.ToInt32(dr["Atmpin"]),
                        AccountType = Convert.ToString(dr["AcntType"]),
                        AccountNumber = Convert.ToInt32(dr["AcntNum"]),
                        Balance = Convert.ToString(dr["Balance"])


                    });

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

            return CustList;
        }

        //GET EMPLOYEE BY ID CODE
        public List<Customer> GetCustomerById(int id)
        {
            con = new SqlConnection(connectionString);           
            List<Customer> CustList = new List<Customer>();
            try
            {
               con.Open();
                SqlCommand cmd = new SqlCommand("GetById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);               
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CustList.Add(new Customer
                    {
                        Id = Convert.ToInt16(dr["Id"]),
                        Fullname = Convert.ToString(dr["Fullname"]),
                        Adress = Convert.ToString(dr["Adress"]),
                        Email = Convert.ToString(dr["Email"]),
                        Contact = Convert.ToString(dr["Contact"]),
                        Atmpin = Convert.ToInt32(dr["Atmpin"]),
                        AccountType = Convert.ToString(dr["AcntType"]),
                        AccountNumber = Convert.ToInt32(dr["AcntNum"]),
                        Balance = Convert.ToString(dr["Balance"])


                    });
                }
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
             return CustList;
        }


        public bool UpdateCustomer(Customer cust)
        {
            try
            {
                con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("UpdateCustomer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", cust.Id);
                cmd.Parameters.AddWithValue("@fullname", cust.Fullname);
                cmd.Parameters.AddWithValue("@address", cust.Adress);
                cmd.Parameters.AddWithValue("@email", cust.Email);
                cmd.Parameters.AddWithValue("@contact", cust.Contact);
                cmd.Parameters.AddWithValue("@atmpin", cust.Atmpin);
                cmd.Parameters.AddWithValue("@acnttype", cust.AccountType);
                cmd.Parameters.AddWithValue("@acntnum", cust.AccountNumber);
                cmd.Parameters.AddWithValue("@bal", cust.Balance);
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


        //DELETE EMPLOYEEE  CODE
        public bool DeleteCustomer(int Id)
        {
            try
            {
                con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("DeleteCustomer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", Id);
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

        public bool CheckCustomer(Customer cust)
        {
            var result = false;
            con = new SqlConnection(connectionString);
            cmd = new SqlCommand("CheckCustomer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", cust.Id);
            cmd.Parameters.AddWithValue("@acntnum", cust.AccountNumber);
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

        public bool CheckIdCust(Customer cust)
        {
            var result = false;
            con = new SqlConnection(connectionString);
            cmd = new SqlCommand("CheckIdCust", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", cust.Id);          
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

        public bool CheckAcnt(Customer cust)
        {

            var result = false;
            con = new SqlConnection(connectionString);
            cmd = new SqlCommand("CheckAcnt", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@acnt", cust.AccountNumber);
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


        public bool WithDrawal(Customer cust)
        {
            try
            {
                con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("UpdateBal", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@acntnum", cust.AccountNumber);
                cmd.Parameters.AddWithValue("@bal", cust.Balance);
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

        //ADMINS CODES
        public List<Customer> GetCustomerByAcnt(int acnt)
        {
            con = new SqlConnection(connectionString);
            List<Customer> CustList = new List<Customer>();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetByAcnt", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@acnt", acnt);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CustList.Add(new Customer
                    {
                        Id = Convert.ToInt16(dr["Id"]),
                        Fullname = Convert.ToString(dr["Fullname"]),
                        Adress = Convert.ToString(dr["Adress"]),
                        Email = Convert.ToString(dr["Email"]),
                        Contact = Convert.ToString(dr["Contact"]),
                        Atmpin = Convert.ToInt32(dr["Atmpin"]),
                        AccountType = Convert.ToString(dr["AcntType"]),
                        AccountNumber = Convert.ToInt32(dr["AcntNum"]),
                        Balance = Convert.ToString(dr["Balance"])


                    });
                }
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return CustList;
        }


    }
}
