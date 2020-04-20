using FriendOrganizer.Model;
using System.Collections.Generic;

namespace FriendOrganizer.UI.Data
{
    public class FriendDataService : IFriendDataService
    {
        public IEnumerable<Friend> GetAll()
        {
            //todo: real database
            yield return new Friend { FirstName = "Aaron", LastName = "Boden" };
            yield return new Friend { FirstName = "Max", LastName = "Mustermann" };
        }
    }
}
