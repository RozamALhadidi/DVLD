using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_DataAccessLayers
{
    public class clsPersonDataAccess
    {
        

        public static bool GetUserByID(short PersonId, ref string NationalNo,
            ref short NationalityCountryID, ref string FirstName,
            ref string SecondName, ref string ThirdName, ref string LastName,
            ref DateTime DateOfBirth,ref short gender, ref string email, ref string phone,
            ref string address, ref string ImagePath)
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from People where PersonId = @PersonId";
            SqlCommand cmd = new SqlCommand(query, Connection);

            cmd.Parameters.AddWithValue("@PersonId", PersonId);
            bool isfound = false;
            try
            {
                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                

                if(reader.Read())
                {                   
                    NationalNo = (string)reader["NationalNo"];
                    NationalityCountryID = Convert.ToInt16(reader["NationalityCountryID"]);
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    ThirdName = (string)reader["ThirdName"];
                    LastName = (string)reader["LastName"];
                   
                    DateOfBirth = reader["DateOfBirth"] != DBNull.Value ?
                     Convert.ToDateTime(reader["DateOfBirth"]) : DateTime.MinValue;

                    gender = Convert.ToInt16(reader["Gendor"]);
                    phone = (string)reader["Phone"];
                    email = (string)reader["Email"];
                    address = (string)reader["Address"];
                    ImagePath = reader["ImagePath"]==DBNull.Value?
                        "":reader["ImagePath"].ToString();
                    isfound = true;
                }
                else
                {
                    isfound = false;
                }
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                Connection.Close();
            }

            return isfound;
        }

        public static short AddNewPerson(string NationalNo,
             short NationalityCountryID,  string FirstName,
             string SecondName,  string ThirdName,  string LastName, DateTime DateOfBirth,
             short Gender,  string Email,  string Phone,
             string Address,  string ImagePath)

        {
            short NewPersonID = -1;
            SqlConnection con = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"insert into people (NationalNo,FirstName,
                SecondName,ThirdName,LastName,DateOfBirth,Gendor,Address,Phone,Email,
                NationalityCountryID,ImagePath) values(@NationalNo,@FirstName,
                @SecondName,@ThirdName,@LastName,@DateOfBirth,@Gender,@Address,@Phone,@Email,
                @NationalityCountryID,@ImagePath)
                select scope_identity()";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@NationalNo", NationalNo);
            cmd.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@SecondName", string.IsNullOrEmpty(SecondName) ? (object)DBNull.Value : SecondName);
            cmd.Parameters.AddWithValue("@ThirdName", string.IsNullOrEmpty(ThirdName) ? (object)DBNull.Value : ThirdName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            cmd.Parameters.AddWithValue("@Gendor", Gender);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@Phone", Phone);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@ImagePath", string.IsNullOrEmpty(ImagePath) 
                ? (object)DBNull.Value : ImagePath);
            try
            {
                con.Open();

                object result = cmd.ExecuteScalar();

                NewPersonID=( result != null && short.TryParse(result.ToString(), out short InsertedID) )?
                      InsertedID : (short)-1;

            }
            catch (Exception  ex)
            {
                System.Windows.Forms.MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }




            return NewPersonID;
        }
        public static bool DeletePerson(short PersonID)
        {
            short rowseffected = 0;
            SqlConnection con = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"delete People where PersonID=@PersonID";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@PersonID", PersonID);


            try
            {
                con.Open();

                rowseffected = (short)cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return (rowseffected > 0);
        }

        public static bool UpdatePerson(short PersonID, string NationalNo,
             short NationalityCountryID, string FirstName,
             string SecondName, string ThirdName, string LastName, DateTime DateOfBirth,
             short Gender, string Email, string Phone,
             string Address, string ImagePath)
        {
            SqlConnection con = new SqlConnection(clsDataAccessSettings.ConnectionString);
            short RowsEffected = 0;
            string query = @"UPDATE People 
                     SET FirstName = @FirstName,
                         SecondName = @SecondName,
                         ThirdName = @ThirdName,
                         LastName = @LastName,
                         DateOfBirth = @DateOfBirth,
                         Gender = @Gender,
                         Email = @Email,
                         Phone = @Phone,
                         Address = @Address,
                         ImagePath = @ImagePath
                     WHERE PersonID = @PersonID";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@PersonID", PersonID);

            cmd.Parameters.AddWithValue("@NationalNo", NationalNo);
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@SecondName", string.IsNullOrEmpty(SecondName) ? (object)DBNull.Value : SecondName);
            cmd.Parameters.AddWithValue("@ThirdName", string.IsNullOrEmpty(ThirdName) ? (object)DBNull.Value : ThirdName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            cmd.Parameters.AddWithValue("@Gendor", Gender);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@Phone", Phone);
            cmd.Parameters.AddWithValue("@Email", Email);

            cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(Email) ? (object)DBNull.Value : Email);
            cmd.Parameters.AddWithValue("@ImagePath", string.IsNullOrEmpty(ImagePath) ? (object)DBNull.Value : ImagePath);


            try
            {
                con.Open();
                RowsEffected = (short)cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return (RowsEffected > 0);
        }

        public static DataTable GetAllPeople()
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "select * from People";
            SqlCommand cmd = new SqlCommand(query, con);


            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error: " + ex.Message);

            }
            finally
            {
                con.Close();
            }
            return dt;
        }

    }
}
