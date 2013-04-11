using System;
using System.Web.Mvc;

namespace PurpleCubed.UI.Models
{
    /// <summary>
    /// THIS IS *NOT* PART OF THE IMPLEMENTATION
    /// THIS IS PROVIDED (OUT OF SCOPE OF PROVIDED SOLUTION)
    ///  
    /// The Employee View Model
    /// 
    /// Provides ways to display view-specific information about the entity
    /// Here, Date of Birth is a string, also not found in the real entity
    /// is the property FullName, this can be created with tools such as
    /// AutoMapper which makes use of conventions and custom configuations
    /// 
    /// Examples of these mappings can be found in the PurpleCubed.CI.Mappings namespace
    /// </summary>
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}