using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystemProject.Models
{
    public class Receptionist
    {
        [Key]
        public int ReceptionistId {  get; set; }
        public string ReceptionistFName { get; set; }
        public string ReceptionistLName {  get; set; }
        public string ReceptionistInfo {  get; set; }
        public ICollection<RoomBooking> RoomBooking { get; set; }
        public ICollection<Payment>Payment { get; set; }
        public ICollection<Report> Report { get; set; }

    }
}
