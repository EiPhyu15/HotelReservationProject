using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystemProject.Models
{
    public class Guest
    {
        [Key]
        public int GuestId {  get; set; }
        public string GuestFName {  get; set; }
        public string GuestLName {  get; set; }
        public string PassportNo {  get; set; }
        public ICollection<RoomBooking> RoomBookings { get; set; }



    }
}
