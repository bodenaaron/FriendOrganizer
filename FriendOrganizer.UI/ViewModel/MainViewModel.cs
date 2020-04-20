using FriendOrganizer.Model;
using FriendOrganizer.UI.Data;
using System.Collections.ObjectModel;

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
        public void Load()
        {
            var friends = friendDataService.GetAll();
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
