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
using Parser.Core;
using Parser.Core.Metacritic;

namespace Parser.Pages
{
    /// <summary>
    /// Interaction logic for MetacriticPage.xaml
    /// </summary>
    public partial class MetacriticPage : Page
    {
        private WebParser<List<string>> webParser;

        public MetacriticPage()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            int startPageNumber;
            int endPageNumber;
            try
            {
                TryParsePageParsingIds(out startPageNumber, out endPageNumber);
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }

            Parse(startPageNumber, endPageNumber);
        }

        private void AbortButton_Click(object sender, RoutedEventArgs e)
        {
            if (webParser != null)
            {
                webParser.Abort();
                webParser.OnDataLoaded -= ShowContent;
            }
        }

        private void Parse(int startPageNumber, int endPageNumber)
        {
            webParser = new WebParser<List<string>>(
                new MetacriticParser(),
                new MetacriticParserSettings(startPageNumber, endPageNumber)
            );
            webParser.OnDataLoaded += ShowContent;
            webParser.Start();
        }

        private void ShowContent(List<string> games)
        {
            if (games.Count() == 0)
                MessageBox.Show("Игры не найдены");

            Dispatcher.Invoke(() =>
            {
                foreach (var game in games)
                    contentBlock.AppendText($"{game}\n");
            });
        }

        private void TryParsePageParsingIds(out int startPageNumber, out int endPageNumber)
        {
            string stringStartPageNumber = startPageIdInput.Text;
            if (!int.TryParse(stringStartPageNumber, out startPageNumber))
                throw new Exception($"Start page id must be int, {stringStartPageNumber} given");

            string stringEndPageNumber = endPageIdInput.Text;
            if (!int.TryParse(stringEndPageNumber, out endPageNumber))
                throw new Exception($"End page id must be int, {stringEndPageNumber} given");
        }
    }
}
