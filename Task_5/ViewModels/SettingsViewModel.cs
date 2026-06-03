using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Task_5.ViewModels;

public partial class SettingsViewModel : ViewModelBase
{
    [ObservableProperty] private string _userName = string.Empty;
    [ObservableProperty] private bool _useDarkTheme;
    [ObservableProperty] private bool _showHints;
    [ObservableProperty] private string _statusMessage = string.Empty;

    [RelayCommand]
    private void Apply()
    {
        StatusMessage = $"Username: {UserName}\n" +
                        $"Using dark theme: {(UseDarkTheme ? "Yes" : "No")}\n" +
                        $"Show hints: {(ShowHints ? "Yes" : "No")}";
    }

    [RelayCommand]
    private void Reset()
    {
        UserName = string.Empty;
        UseDarkTheme = false;
        ShowHints = false;
        StatusMessage = string.Empty;
    }
}