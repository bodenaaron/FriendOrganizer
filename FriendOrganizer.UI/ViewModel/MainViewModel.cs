using FriendOrganizer.UI.Event;
using FriendOrganizer.UI.View.Services;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FriendOrganizer.UI.ViewModel
{
    public class MainViewModel:ViewModelBase
    {
        private IFriendDetailViewModel friendDetailViewModel;
        public MainViewModel(INavigationViewModel navigationViewModel, Func<IFriendDetailViewModel> friendDetailViewModelCreator, IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
        {
            
            this.friendDetailViewModelCreator = friendDetailViewModelCreator;
            this.eventAggregator = eventAggregator;
            this.messageDialogService = messageDialogService;
            this.eventAggregator.GetEvent<OpenFriendDetailViewEvent>().Subscribe(OnOpenFriendDetailView);

            CreateNewFriendCommand = new DelegateCommand(OnCreateNewFriendExecute);

            NavigationViewModel = navigationViewModel;
        }

        

        public INavigationViewModel NavigationViewModel { get;}

        private Func<IFriendDetailViewModel> friendDetailViewModelCreator;

        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

        public ICommand CreateNewFriendCommand { get; }

        public IFriendDetailViewModel FriendDetailViewModel
        {
            get { return friendDetailViewModel; }
            private set { friendDetailViewModel = value; OnPropertyChanged(); }
        }


        private IEventAggregator eventAggregator;
        private IMessageDialogService messageDialogService;

        private async void OnOpenFriendDetailView(int? friendId)
        {
            if (FriendDetailViewModel != null && FriendDetailViewModel.HasChanges)
            {
                var result = messageDialogService.ShowOkCancelDialog("Änderungen nicht gespeichert, ohne speichern fortfahren?", "Nicht gespeicherte Änderungen");
                if (result==MessageDialogResult.Cancel)
                {
                    return;
                }
            }
            FriendDetailViewModel = friendDetailViewModelCreator();
            await FriendDetailViewModel.LoadAsync(friendId);

        }

        private void OnCreateNewFriendExecute()
        {
            OnOpenFriendDetailView(null);
        }

    }
}
