using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelReservationSystemProject.Models
{
    public class Report
    {
        [Key]
        public int ReportId {  get; set; }
        public DateOnly GeneratedDate {  get; set; }
        public string ReportTitle {  get; set; }
        public int ManagerId {  get; set; }
        [ForeignKey("ManagerId")]
        public Manager Manager { get; set; }
        public int ReceptionistId {  get; set; }
        [ForeignKey("ReceptionistId")]
        public Receptionist Receptionist { get; set; }
    }
}
