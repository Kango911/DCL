using System.Windows;
using System.Windows.Controls;


namespace DCL.Pages;

public partial class start : Page
{
    public MainWindow mainWindow;
    public start(MainWindow _mainWindow)
    {
        InitializeComponent();

        mainWindow = _mainWindow;
            
    }

    private void frak_Click(object sender, RoutedEventArgs e)
    {
        mainWindow.OpenPage(MainWindow.pages.frak); 
    }

    
    
    private void bash_Click(object sender, RoutedEventArgs e) 
    {     
        mainWindow.OpenPage(MainWindow.pages.Towers); 
    }
}