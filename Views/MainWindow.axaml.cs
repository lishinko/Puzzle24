using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Puzzle24.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            Result.Text = ex.Message;
        }
    }
}