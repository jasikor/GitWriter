using System.Windows;
using GitWriter.ViewModels;

namespace GitWriter;

public partial class MainWindow : Window
{
    
    public MainWindow(BinderViewModel binder)
    {
        InitializeComponent();
        BinderControl.DataContext = binder;

    }


}