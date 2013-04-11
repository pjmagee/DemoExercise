using System.Collections.Generic;

namespace PurpleCubed.UI.Models
{
    /// <summary>
    /// THIS IS *NOT* PART OF THE IMPLEMENTATION
    /// THIS IS PROVIDED (OUT OF SCOPE OF PROVIDED SOLUTION)
    /// 
    /// The Team View Model
    /// 
    /// Provides ways to display view-specific information about the entity
    /// 
    /// For example, how many members are in the team, 
    /// if the application was to mature, specifics such as
    /// lead programmers, junior developers could be listed as properties
    /// and have AutoMapper return these view models 
    /// from a Service such that consumes a Repository
    ///  
    /// Can be used when we know that a List of Teams would not need to contain 
    /// a whole bunch of employee entities, so we know we can just display the 
    /// number of employees
    /// </summary>
    public class TeamSummaryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfEmployees { get; set; }
        public string DateCreated { get; set; }
    }

    public class TeamViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<EmployeeViewModel> EmployeeViewModels { get; set; }
        public string DateCreated { get; set; }
    }

}