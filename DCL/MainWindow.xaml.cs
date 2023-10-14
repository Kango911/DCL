using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DCL.Pages;

namespace DCL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            OpenPage(pages.start);
        }
    
        public enum pages
        {
            start,
            frak,
            bash
        }   
        
        public void OpenPage(pages pages)
        {
            if (pages == pages.start)
            {
                frame.Navigate(new start(this));
            } else if (pages == pages.frak)
                frame.Navigate(new frak(this));
            else if (pages == pages.bash)
                frame.Navigate(new bash(this));
        }
    }
}