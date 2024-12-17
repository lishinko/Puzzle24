using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Diagnostics;

namespace Puzzle24.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _result = "还没有计算出来";
    [ObservableProperty]
    private string _data0 = "3";
    [ObservableProperty]
    private string _data1 = "8";
    [ObservableProperty]
    private string _data2 = "8";
    [ObservableProperty]
    private string _data3 = "8";
    public void RunSolver()
    {
        try
        {
            var items = new int[4];
            items[0] = int.Parse(Data0);
            items[1] = int.Parse(Data1);
            items[2] = int.Parse(Data2);
            items[3] = int.Parse(Data3);
            var p = new PuzzleSolver(items);
            if (p.Solve24(out var result, out var ops))
            {
                var txt = PuzzleSolver.PrintResult(result, ops);
                Result = txt;
            }
            else
            {
                Result = "计算失败！";
            }
        }
        catch (Exception ex)
        {
            Result = ex.Message;
        }
    }
}
