using GuestService.Models;
using System.Collections;

namespace GuestService.Data
{
    public interface IGuestRepo
    {
        bool SaveChanges();

        IEnumerable<Guest> GetAllGuests();
        Guest GetGuestById(int id);
        void CreateGuest(Guest guest);
    }
}
