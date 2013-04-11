namespace PurpleCubed.UI.Models
{
    public class EditTeamViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int[] SelectedEmployeeIds { get; set; }
    }
}