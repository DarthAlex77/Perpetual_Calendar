using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Perpetual_Calendar.ViewModels;
using Perpetual_Calendar.Views;

namespace Perpetual_Calendar
{
    public class App : Application
    {
        public override void Initialize() { AvaloniaXamlLoader.Load(this); }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                        DataContext = new AppViewModel(),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}