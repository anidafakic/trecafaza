using GuestService.Models;

namespace GuestService.Data
{
    public class GuestRepo(GuestDbContext context) : IGuestRepo
    {
        public void CreateGuest(Guest guest)
        {
            if (guest == null) throw new ArgumentNullException(nameof(guest));

            context.Guests.Add(guest);
        }

        public IEnumerable<Guest> GetAllGuests()
        {
            return context.Guests.ToList();
        }

        public Guest GetGuestById(int id)
        {
            var guest = context.Guests.FirstOrDefault(g => g.Id == id);
            if (guest == null) throw new ArgumentNullException(nameof(guest));
            return guest;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
