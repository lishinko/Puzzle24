﻿using CommunityToolkit.Mvvm.ComponentModel;

namespace Puzzle24.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _greeting = "Welcome to Avalonia!";
}
