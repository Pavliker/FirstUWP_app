using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Services;
using AlbumApp1._0._1.Views;
using AlbumApp1._0._1.Views.Basic;
using AlbumApp1._0._1.WindowsViews;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.ViewModels.Basic
{
    public partial class ShellViewModel:BasedViewModelContext
    {
        private readonly INavigationService _navigationService;
        private readonly IAuthenticationService authentication;
        private AboutProjectViewModel aboutProjectViewModel;
        private AllPhotoViewModel allPhotoViewModel;
        public BasicViewModel basicViewModel;
        //public BasicWindow basicWindow;
        private MainWindow mainWindow;
        private readonly IActivationService activation;
        private BasicWindow BasicWindow { get; set; }
        public ShellViewModel(INavigationService navigationService, IAuthenticationService authentication, IActivationService activation)
        {
            this.authentication = authentication;
            this.activation = activation;
            _navigationService = navigationService;
           
        }
        public string Username
        {
            get
            {
                return authentication.AuthenticationName;
            }
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

        }
        [RelayCommand]
        public void NavigateToFavourites()
        {

        }
        [RelayCommand]
        public void RandomPhoto()
        {

        }
        [RelayCommand]
        public void NavigateToFeedback()
        {

        }
        [RelayCommand]
        public void NavigateToArchive()
        {

        }
        [RelayCommand]
        public void NavigateToQuestions()
        {

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
