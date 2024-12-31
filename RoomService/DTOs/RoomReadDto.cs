namespace RoomService.DTOs
{
    public class RoomReadDto
    {
        public int Id { get; set; }
        public string RoomType { get; set; }
        public double Price { get; set; }
        public bool IsAvailable { get; set; }
    }
}
