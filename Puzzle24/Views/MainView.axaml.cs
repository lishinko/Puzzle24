using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace Puzzle24.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }
    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            var items = new int[4];
            items[0] = int.Parse(T0.Text!);
            items[1] = int.Parse(T1.Text!);
            items[2] = int.Parse(T2.Text!);
            items[3] = int.Parse(T3.Text!);
            var p = new PuzzleSolver(items);
            if (p.Solve24(out var result, out var ops))
            {
                var txt = PuzzleSolver.PrintResult(result, ops);
                Result.Text = txt;
            }
            else
            {
                Result.Text = "计算失败！";
            }
        }
        catch (Exception ex)
        {
            Result.Text = ex.Message;
        }
    }
}