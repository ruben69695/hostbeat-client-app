using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using HostbeatClient.Features.Login;
using HostbeatClient.Views;

namespace HostbeatClient;

public partial class App : Application
{
    private NativeMenu? _appMenu;
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Line below is needed to remove Avalonia data validation.
            // Without this line you will get duplicate validations from both Avalonia and CT
            BindingPlugins.DataValidators.RemoveAt(0);
            desktop.MainWindow = new SplashScreen()
            {
                DataContext = new SplashScreen(),
            };
        }

        base.OnFrameworkInitializationCompleted();
        
        _appMenu = NativeMenu.GetMenu(Current!);
    }
}