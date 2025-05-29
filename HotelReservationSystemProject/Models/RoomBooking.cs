using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelReservationSystemProject.Models
{
    public class RoomBooking
    {
        [Key]
        public int RoomBookingId {  get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public string Status {  get; set; }
        public int GuestId {  get; set; }
        [ForeignKey("GuestId")]
        public Guest Guest { get; set; }
        public int ReceptionistId {  get; set; }
        [ForeignKey("ReceptionistId")]
        public Receptionist Receptionist { get; set; }
        public ICollection<RoomBookingDetails>RoomBookingDetails { get; set; }
        
    }
}
