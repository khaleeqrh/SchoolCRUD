using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolCRUD.Models
{
    public class AdmissionModel
    {
        public int ID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public int Gender { get; set; }
        public int Religion { get; set; }
        public int Nationality { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public String Email { get; set; }
        public int Country { get; set; }
        public int State { get; set; }
        public int City { get; set; }
        public String StreetAddress { get; set; }

    }
}