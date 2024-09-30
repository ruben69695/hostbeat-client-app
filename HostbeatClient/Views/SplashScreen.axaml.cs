using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using HostbeatClient.Features.Login;

namespace HostbeatClient.Views;

public partial class SplashScreen : Window
{
    public SplashScreen()
    {
        InitializeComponent();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);

        App.Current.TryGetResource("SplashScreenPath", ActualThemeVariant, out object? filename);
        
        RootImage.Source = new Bitmap(AssetLoader.Open(new Uri((string)filename!)));
    }

    protected override async void OnOpened(EventArgs e)
    {
        base.OnOpened(e);
        
        await Task.Delay(2500);

        var login = new LoginWindow();
        login.Show();
        
        Close();
    }
}