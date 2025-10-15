using Microsoft.UI.Xaml;

namespace AlbumApp1._0._1.Interfaces;

public interface IActivationService
{
    Task ActivateAsync<W, V>(W window, V view, object activationArgs) where W : Window where V : UIElement;
}
