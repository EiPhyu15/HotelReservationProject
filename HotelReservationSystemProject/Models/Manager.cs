using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystemProject.Models
{
    public class Manager
    {
        [Key]
        public int ManagerId {  get; set; }
        public string ManagerFName {  get; set; }
        public string ManagerLName {  get; set; }
        public string ManagerInfo {  get; set; }
        public ICollection<Report> Report { get; set; }
    }
}
