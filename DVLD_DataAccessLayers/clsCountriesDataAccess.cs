using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayers
{
    public class clsCountriesDataAccess
    {
        public static bool FindCountry(short CountryID,  ref string CountryName)
        {
            SqlConnection con = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select countryname from Countries where countryid= @countryid";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@countryid", CountryID);

            bool isfound = false;
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.Read())
                {

                    CountryName = (string)reader["CountryName"];
                isfound = true;
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
            return isfound;
        }

    }
}
