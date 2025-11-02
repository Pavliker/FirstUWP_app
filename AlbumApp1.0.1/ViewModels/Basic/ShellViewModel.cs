using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Models;
using AlbumApp1._0._1.Services;
using AlbumApp1._0._1.Views;
using AlbumApp1._0._1.Views.Basic;
using AlbumApp1._0._1.WindowsViews;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.WindowsAppSDK.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.ViewModels.Basic
{
    public partial class ShellViewModel:BasedViewModelContext
    {
        private bool _isPaneOpen;
        public bool IsPaneOpen
        {
            get
            {
                return _isPaneOpen;
            }
            set
            {
                _isPaneOpen = value;
                OnPropertyChanged(nameof(IsPaneOpen));
            }
        }
        private readonly INavigationService _navigationService;
        private readonly IAuthenticationService authentication;
        private AboutProjectViewModel aboutProjectViewModel;
        private AllPhotoViewModel allPhotoViewModel;
        public BasicViewModel basicViewModel;
        private AlbumsViewModel albumsViewModel;
        private FavouritesViewModel FavouritesViewModel;
        private FeedbackViewModel feedbackViewModel;
        private ArchiveViewModel archiveViewModel;
        private QuestionViewModel questionViewModel;
        private TitleBarViewModel TitleBarViewModel;

        //public BasicWindow basicWindow;
        private MainWindow mainWindow;
        private readonly IActivationService activation;
        public ShellViewModel(INavigationService navigationService, IAuthenticationService authentication, IActivationService activation)
        {
            TitleBarViewModel = App.GetService<TitleBarViewModel>();
            this.authentication = authentication;
            this.activation = activation;
            _navigationService = navigationService;
           
        }
       
        [RelayCommand]
        public void NavigateToAll()
        {
            allPhotoViewModel = App.GetService<AllPhotoViewModel>();
            _navigationService.NavigateTo(allPhotoViewModel.GetType());

        }
        [RelayCommand]
        public void NavigateToAlbums()
        {
            albumsViewModel = App.GetService<AlbumsViewModel>();
            _navigationService.NavigateTo(albumsViewModel.GetType());
        }
        [RelayCommand]
        public void NavigateToFavourites()
        {
            FavouritesViewModel = App.GetService<FavouritesViewModel>();
            _navigationService.NavigateTo(FavouritesViewModel.GetType());
        }
        [RelayCommand]
        public void RandomPhoto()
        {

        }
        [RelayCommand]
        public void NavigateToFeedback()
        {
            feedbackViewModel = App.GetService<FeedbackViewModel>();
            _navigationService.NavigateTo(feedbackViewModel.GetType());
        }
        [RelayCommand]
        public void NavigateToArchive()
        {
            archiveViewModel = App.GetService<ArchiveViewModel>();
            _navigationService.NavigateTo(archiveViewModel.GetType() );
        }
        [RelayCommand]
        public void NavigateToQuestions()
        {
            questionViewModel = App.GetService<QuestionViewModel>();
            _navigationService.NavigateTo(questionViewModel.GetType());
        }
        [RelayCommand]
        public void NavigateToAboutProject()
        {
            aboutProjectViewModel = App.GetService<AboutProjectViewModel>();
            _navigationService.NavigateTo(aboutProjectViewModel.GetType());

        }
        public BasicWindow basic;
        [RelayCommand]
        public  void Exit()
        {
            var identityRole = App.GetService<IdentityRolePrincipal>();
            identityRole.IdentityRole =  new IdentityRole(false, string.Empty, string.Empty, string.Empty);
            ApplicationPrincipal.SwitchCurrentPrincipal(() => identityRole);

            //TitleBarViewModel.authentication._identityRole = new Models.IdentityRole(string.Empty, string.Empty, string.Empty);
            //App.GetService<ProfileViewModel>().authentication._identityRole = new Models.IdentityRole(string.Empty, string.Empty, string.Empty);

            //basicViewModel = App.GetService<BasicViewModel>();
            ////basicWindow = App.GetService<BasicWindow>();
            //BasicWindow = App.GetService<BasicWindow>();
            //var basic = App.GetService<BasicViewModel>();
            var view = App.GetService<MainPageView>(); 
            var main = App.GetService<MainViewModel>();
            //var main1 = App.GetService<MainWindow>();

            //activation.UnregisterInstance(basicWindow.GetType());

            //activation.RegisterInstance(basicWindow.GetType(), basicWindow);
            //activation.RegisterInstance(BasicWindow.GetType(), BasicWindow);
            //basic = App.GetService<BasicWindow>();
            //App.GetService<IActivationService>().RegisterMapping<BasicViewModel, BasicWindow>(basic);
            mainWindow = App.GetService<MainWindow>();
            App.GetService<IActivationService>().RegisterMapping<MainViewModel, MainWindow>(mainWindow);


            activation.OpenWindow(main, view);
            //activation.RegisterInstance(main1.GetType(), main1);
           

            activation.CloseWindow<BasicViewModel>();
        }
    }
}
