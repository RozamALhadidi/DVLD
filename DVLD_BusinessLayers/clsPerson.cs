using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_DataAccessLayers;

namespace DVLD_BusinessLayers
{
    public class clsPerson
    {
        public enum enMode { AddNew=1,Update=2};
        public short PersonID { set; get; }
        public short NationalityCountryID { set; get; }
      public  string FirstName { set; get; }

        public string SecondName { set; get; }
        public string ThirdName { set; get; }
        public string LastName { set; get; }
        public DateTime DateOfBirth { set; get; }
        public string NationalityNo { set; get; }
        public short Gender { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string Address { set; get; }
        public string ImagePath { set; get; }
        public enMode Mode = enMode.AddNew;

        clsPerson(short personID, string NationalityNo, short nationalityCountryID,
            string firstName,string secondname, string thirdName, string lastName, DateTime DateOfBirth,
            short gender, string email, string phone, string address, string imagePath)
        {

            this.PersonID = personID;
            this.NationalityNo = NationalityNo;
            this.NationalityCountryID = nationalityCountryID;
            this.FirstName = firstName;
            this.SecondName = secondname;
            this.ThirdName = thirdName;
            this.LastName = lastName;
            this.DateOfBirth = DateOfBirth;
            this.Gender = gender;
            this.Email = email;
            this.Phone = phone;
            this.Address = address;
            this.ImagePath = imagePath;
            this.Mode = enMode.Update;

        }



        public static clsPerson GetUserByID(short PersonId)
        {
            short NationalityCountryID = -1,Gender = -1;
            string FirstName = "", SecondName = "", ThirdName = "", LastName = ""
              , Email = "", NationalityNo = "",
                Phone = "", Address = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;


            if (clsPersonDataAccess.GetUserByID(PersonId,ref NationalityNo, ref NationalityCountryID,
               ref FirstName,ref SecondName,ref ThirdName,ref LastName,ref DateOfBirth,
               ref Gender ,ref Email,ref Phone,ref Address,ref ImagePath))
            {
                return new clsPerson(PersonId, NationalityNo, NationalityCountryID,
                    FirstName, SecondName, ThirdName, LastName, DateOfBirth,Gender,
                    Email, Phone, Address, ImagePath);
            }
            else
            {

            return null;
            }
            
        }
        private bool _AddNewPerson()
        {
            int id = clsPersonDataAccess.AddNewPerson(this.NationalityNo, this.NationalityCountryID, this.FirstName
                , this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth, this.Gender, this.Email,this.Phone
                , this.Address, this.ImagePath);

            return (id != -1);
              
        }
        private bool _UpdatePerson()
        {
            return clsPersonDataAccess.UpdatePerson(this.PersonID,this.NationalityNo, this.NationalityCountryID, this.FirstName
                , this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth, this.Gender, this.Email, this.Phone
                , this.Address, this.ImagePath);
        }

        public static bool DeletePerson(short PersonID)
        {
            return clsPersonDataAccess.DeletePerson(PersonID);
        }

        public static DataTable GetAllPeople()
        {
            return clsPersonDataAccess.GetAllPeople();
        }

        public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdatePerson();
            }
            return false;
        }
    }
}
