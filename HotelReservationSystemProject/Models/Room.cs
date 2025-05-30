using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystemProject.Models
{
    public class Room
    {
        [Key]
        public int RoomId {  get; set; }
        public string RoomType {  get; set; }

        public string ImageUrl {  get; set; }
        public string Status {  get; set; }
        public double Price {  get; set; }
        public ICollection<RoomItems> RoomItems { get; set; }
        public ICollection<RoomBookingDetails> RoomBookingDetails { get; set; }
        

    }
}
