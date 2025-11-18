using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.ViewModels;
using AlbumApp1._0._1.ViewModels.Basic;
using AlbumApp1._0._1.ViewModels.Basic.Photos;
using AlbumApp1._0._1.ViewModels.SplashScreen;
using AlbumApp1._0._1.Views;
using AlbumApp1._0._1.Views.Basic;
using AlbumApp1._0._1.Views.Basic.Photos;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace AlbumApp1._0._1.Services;

public partial class PageService : IPageService
{
    private readonly Dictionary<Type, Type> _pages = new();

    public PageService()
    {
        Configure<TitleBarViewModel, TitleBarView>();
        Configure<MainViewModel, MainPageView>();
        Configure<AuthViewModel, AuthPageView>();
        Configure<RegisterViewModel, RegisterPageView>();
        Configure<SplashScreenViewModel, SplashScreenView>();
        Configure<QuestionViewModel, QuestionsView>();
        Configure<PhotosViewModel, PhotosView>();
        Configure<FeedbackViewModel, FeedbackView>();
        Configure<FavouritesViewModel, FavouritesView>();
        Configure<BasicViewModel, ShellView>();
        Configure<ArchiveViewModel, ArchiveView>();
        Configure<AlbumsViewModel, AlbumsView>();
        Configure<AboutProjectViewModel, AboutProjectView>();
        Configure<ProfileViewModel, ProfileView>();
        Configure<DetailedPhotosViewModel, DetailedPage>();
        Configure<InformationAboutPhotographyViewModel, InformationAboutPhotography>();
        Configure<AboutPhotoViewModel, AboutPhotoView>();

    }

    public Type GetPageType(Type type)
    {
        Type? pageType;
        lock (_pages)
        {
            if (!_pages.TryGetValue(type, out  pageType))
            {
                throw new ArgumentException($"Page not found: {type}. Did you forget to call PageService.Configure?");
            }
        }

        return pageType;
    }

    private void Configure<VM, V>()
        where VM : class
        where V : Page
   {
        lock (_pages)
        {
            var key = typeof(VM)!;
            if (_pages.ContainsKey(key))
            {
                throw new ArgumentException($"The key {key} is already configured in PageService");
            }

            var type = typeof(V);
            if (_pages.ContainsValue(type))
            {
                throw new ArgumentException($"This type is already configured with key {_pages.First(p => p.Value == type).Key}");
            }

            _pages.Add(key, type);
        }
    }
}
