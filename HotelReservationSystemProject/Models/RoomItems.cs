using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelReservationSystemProject.Models
{
    public class RoomItems
    {
        [Key]
        public int RoomItemsId {  get; set; }
        public string CartId { get; set; }
        public int Quantity {  get; set; }
        public double Price {  get; set; }
        public int RoomId {  get; set; }
        [ForeignKey("RoomId")]
        public Room Room { get; set; }
    }
}
