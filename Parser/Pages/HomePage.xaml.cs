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

namespace Parser.Pages
{
    public partial class HomePage : Page
    {
        public event Action<Page> OnNavigateTo = (Page page) => { };

        public HomePage()
        {
            InitializeComponent();
        }

        private void ImdButton_Click(object sender, RoutedEventArgs e)
        {
            GoToPage(new ImdbPage());
        }

        private void GoToPage(Page page)
        {
            NavigationService.Navigate(page);
        }
    }
}
