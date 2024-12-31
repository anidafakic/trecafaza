namespace GuestService.Models
{
    public class Guest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
    }
}
