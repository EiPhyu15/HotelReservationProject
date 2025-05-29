using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelReservationSystemProject.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId {  get; set; }
        public DateOnly PaymentDate {  get; set; }
        public double PaymentAmount { get; set; }
        public string paymentType {  get; set; }
        public int ReceptionistId {  get; set; }
        [ForeignKey("ReceptionistId")]
        public Receptionist Receptionist { get; set; }
        public int RoomBookingId {  get; set; }
        [ForeignKey("RoomBookingId")]
        public RoomBooking RoomBooking { get; set; }
    }
}
