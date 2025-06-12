using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HotelReservationSystemProject.Models
{
    public class Invoice
    {
        [Key]
        public int InvoiceId {  get; set; }
        public double TotalAmount {  get; set; }
        public string PaymentStatus {  get; set; }
        public DateOnly InvoiceDate { get; set; }
        public int RoomBookingId { get; set; }
        [ForeignKey("RoomBookingId")]
         public RoomBooking RoomBooking { get; set; }
        //public ICollection<Payment> Payment { get; set; }


    }
}
