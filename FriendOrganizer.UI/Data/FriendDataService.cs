using FriendOrganizer.DataAccess;
using FriendOrganizer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.Data
{
    public class FriendDataService : IFriendDataService
    {
        private Func<FriendOrganizerDbContext> contextCreator; //Im Autofac gegistriert, gibt den Typen zurück

        public FriendDataService(Func<FriendOrganizerDbContext> contextCreator) //Wenn der typ generiert wird, übergibt autofac automatisch den Typ
        {
            this.contextCreator = contextCreator;
        }
        public async Task<Friend> GetByIdAsync(int friendId)
        {
            using (var ctx = contextCreator())
            {
                return await ctx.Friends.AsNoTracking().SingleAsync(f => f.Id == friendId);
        }
        }
    }
}
