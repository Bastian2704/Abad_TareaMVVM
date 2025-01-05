using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace AbadTareaMVVM.SAViewModels;

internal class SAAboutViewModel
{
    public string SATitle => AppInfo.Name;
    public string SAVersion => AppInfo.VersionString;
    public string SAMoreInfoUrl => "https://aka.ms/maui";
    public string SAMessage => "This app is written in XAML and C# with .NET MAUI.";
    public ICommand SAShowMoreInfoCommand { get; }

    public SAAboutViewModel()
    {
        SAShowMoreInfoCommand = new AsyncRelayCommand(SAShowMoreInfo);
    }

    async Task SAShowMoreInfo() =>
        await Launcher.Default.OpenAsync(SAMoreInfoUrl);
}