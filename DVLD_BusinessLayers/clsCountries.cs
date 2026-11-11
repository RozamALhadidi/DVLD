using DVLD_DataAccessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayers
{
    public class clsCountries
    {
        public short CountryID;
        public string CountryName;

         clsCountries(short CountryID,string CountryName)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;
        }
        public static clsCountries FindCountry(short CountryID)
        {
            string CountryName = "";
            if (clsCountriesDataAccess.FindCountry(CountryID, ref CountryName))
            {
                return new clsCountries(CountryID, CountryName);
            }
            else return null;
        }
    }
}
