using FriendOrganizer.Model;
using FriendOrganizer.UI.Data;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.ViewModel
{
    public class MainViewModel:ViewModelBase
    {

        private Friend selectedFriend;

        private IFriendDataService friendDataService;

        public MainViewModel(IFriendDataService friendDataService)
        {
            Friends = new ObservableCollection<Friend>();
            this.friendDataService = friendDataService;
        }
        public async Task LoadAsync()
        {
            var friends =await friendDataService.GetAllAsync();
            Friends.Clear();
            foreach (var friend in friends)
            {
                Friends.Add(friend);
            }
        }
        public ObservableCollection<Friend> Friends {get;set;}


        public Friend SelectedFriend
        {
            get { return selectedFriend; }
            set { selectedFriend = value;
                OnPropertyChanged();
            }
        }               
    }
}
