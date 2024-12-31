namespace RoomService.DTOs
{
    public class RoomCreateDto
    {
        public string RoomType { get; set; }
        public double Price { get; set; }
        public bool IsAvailable { get; set; }
    }
}
