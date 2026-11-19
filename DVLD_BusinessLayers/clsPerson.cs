using DVLD_DataAccessLayers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public    clsPerson()
        {
            this.Mode = enMode.AddNew;

        }


        public static clsPerson GetPersonByID(short PersonId)
        {
            short NationalityCountryID = -1,Gender = -1;
            string FirstName = "", SecondName = "", ThirdName = "", LastName = ""
              , Email = "", NationalityNo = "",
                Phone = "", Address = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;

            if (clsPersonDataAccess.GetPersonByID(PersonId,ref NationalityNo, ref NationalityCountryID,
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
            this.PersonID = clsPersonDataAccess.AddNewPerson(this.NationalityNo, this.NationalityCountryID, this.FirstName
                , this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth, this.Gender, this.Email,this.Phone
                , this.Address, this.ImagePath);

            return (this.PersonID != -1);
              
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
        public static DataTable GetAllPeopleForDGV()
        {
            return clsPersonDataAccess.GetAllPeopleForDGV();
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
        public static bool IsValidName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            string pattern = @"^[a-zA-Z\s]{2,50}$";
            return Regex.IsMatch(name.Trim(), pattern);
        }
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return true;

            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email.Trim(), pattern);
        }
        public static bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            string pattern = @"^\+?[0-9]{7,15}$";
            return Regex.IsMatch(phone.Trim(), pattern);
        }
        public  bool CopyandSaveImg(string PeopleImagesFolder, string PersonSourceImgPath)
        {
            try
            {
                 if (!Directory.Exists(PeopleImagesFolder))
                        Directory.CreateDirectory(PeopleImagesFolder);

                    string NewFileName = Guid.NewGuid().ToString() + Path.GetExtension(PersonSourceImgPath);
                string DestinationPath = Path.Combine(PeopleImagesFolder, NewFileName);
                File.Copy(PersonSourceImgPath, DestinationPath,true);

                this.ImagePath = NewFileName;

                 return true;
            }
            catch
            {
                return false;
            }
            
        }
    }
}
