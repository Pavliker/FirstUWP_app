using AlbumApp1._0._1.WindowsViews;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;

namespace AlbumApp1._0._1.Interfaces;

public interface IActivationService
{
    void ActivateAsync<W, V>(W window, V view, object activationArgs) where W : Window where V : UIElement;
    void OpenWindow<WM, V>(WM viewModel, V view) where WM : class where V : UIElement;
    void CloseWindow<T>() where T : class;
    MainWindow _MainWindow { get; set; }
    void RegisterMapping<TViewModel, TWindow>(TWindow window) where TViewModel : class where TWindow : Window;
    Window? GetWindowTypeForViewModel(Type viewModelType);
}
