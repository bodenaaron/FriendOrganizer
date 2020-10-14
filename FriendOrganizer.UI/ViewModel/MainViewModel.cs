using FriendOrganizer.Model;
using FriendOrganizer.UI.Data;
using FriendOrganizer.UI.Event;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace FriendOrganizer.UI.ViewModel
{
    public class MainViewModel:ViewModelBase
    {
        private IFriendDetailViewModel friendDetailViewModel;
        public MainViewModel(INavigationViewModel navigationViewModel, Func<IFriendDetailViewModel> friendDetailViewModelCreator, IEventAggregator eventAggregator)
        {
            
            this.friendDetailViewModelCreator = friendDetailViewModelCreator;
            this.eventAggregator = eventAggregator;

            this.eventAggregator.GetEvent<OpenFriendDetailViewEvent>().Subscribe(OnOpenFriendDetailView);

            NavigationViewModel = navigationViewModel;
        }

        public INavigationViewModel NavigationViewModel { get;}

        private Func<IFriendDetailViewModel> friendDetailViewModelCreator;

        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

        

        public IFriendDetailViewModel FriendDetailViewModel
        {
            get { return friendDetailViewModel; }
            private set { friendDetailViewModel = value; OnPropertyChanged(); }
        }


        private IEventAggregator eventAggregator;

        private async void OnOpenFriendDetailView(int friendId)
        {
            if (FriendDetailViewModel != null && FriendDetailViewModel.HasChanges)
            {
                var result = MessageBox.Show("Änderungen nicht gespeichert, ohne speichern fortfahren?","Nicht gespeicherte Änderungen",MessageBoxButton.OKCancel);
                if (result==MessageBoxResult.Cancel)
                {
                    return;
                }
            }
            FriendDetailViewModel = friendDetailViewModelCreator();
            await FriendDetailViewModel.LoadAsync(friendId);

        }
    }
}
