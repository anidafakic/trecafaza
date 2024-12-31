namespace RoomService.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomType { get; set; }
        public double Price { get; set; }
        public bool IsAvailable { get; set; }
    }
}
