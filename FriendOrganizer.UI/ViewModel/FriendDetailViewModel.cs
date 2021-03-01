
using FriendOrganizer.Model;
using FriendOrganizer.UI.Data;
using FriendOrganizer.UI.Data.Repositories;
using FriendOrganizer.UI.Event;
using FriendOrganizer.UI.View.Services;
using FriendOrganizer.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FriendOrganizer.UI.ViewModel
{
    public class FriendDetailViewModel : ViewModelBase, IFriendDetailViewModel
    {
        private IFriendRepository friendRepository;
        private IEventAggregator eventAggregator;
        private IMessageDialogService messageDialogService;
        private FriendWrapper friend;
        private bool hasChanges;

        public FriendWrapper Friend
        {
            get { return friend; }
            private set
            {
                friend = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

        public async Task LoadAsync(int? friendId)
        {
            var friend = friendId.HasValue
               ? await friendRepository.GetByIdAsync(friendId.Value)
               :CreateNewFriend();

            Friend = new FriendWrapper(friend);
            Friend.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = friendRepository.HasChanges();
                }
                if (e.PropertyName == nameof(Friend.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            if (Friend.Id == 0)
            {
                //Validierung auslösen
                Friend.FirstName = "";
            }
        }

        

        public FriendDetailViewModel(IFriendRepository friendRepository, IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
        {
            this.friendRepository = friendRepository;
            this.eventAggregator = eventAggregator;
            this.messageDialogService = messageDialogService;

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);
        }

        private async void OnDeleteExecute()
        {
            var result = messageDialogService.ShowOkCancelDialog($"{Friend.FirstName} {Friend.LastName} wirklich löschen?","Wirklich löschen?");
            if (result == MessageDialogResult.OK)
            {
                friendRepository.Remove(Friend.Model);
                await friendRepository.SaveAsync();
                eventAggregator.GetEvent<AfterFriendDeletedEvent>().Publish(Friend.Id);
            }            
        }        

        private async void OnSaveExecute()
        {
           await friendRepository.SaveAsync();
            HasChanges = friendRepository.HasChanges();
            eventAggregator.GetEvent<AfterFriendSavedEvent>().Publish(new AfterFriendSavedEventArgs {Id=Friend.Id, DisplayMember=$"{Friend.FirstName} {Friend.LastName}"});
        }

        private bool OnSaveCanExecute()
        {

            return Friend!=null&&!Friend.HasErrors && HasChanges;
        }

        

        public bool HasChanges
        {
            get { return hasChanges; }
            set
            {
                if (hasChanges != value)
                {
                    hasChanges = value; 
                    OnPropertyChanged(); 
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }            
        }

        public Task<Friend> GetByIdAsync(int friendId)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(Friend friend)
        {
            throw new NotImplementedException();
        }

        private Friend CreateNewFriend()
        {
            var friend = new Friend();
            friendRepository.Add(friend);
            return friend;
        }
    }
}
