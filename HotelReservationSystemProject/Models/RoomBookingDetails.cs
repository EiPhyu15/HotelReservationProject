using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelReservationSystemProject.Models
{
    public class RoomBookingDetails
    {
        [Key]
        public int RoomBookingDetailsId {  get; set; }
        public string ServiceDescription {  get; set; }
        public double RoomPrice {  get; set; }
        public int RoomBookingId { get; set; }
        [ForeignKey("RoomBookingId")]
        public RoomBooking RoomBooking { get; set; }
        public int RoomId {  get; set; }
        [ForeignKey("RoomId")]
        public Room Room {  get; set; }

       
    }
}
