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
            int startPageId;
            int endPageId;
            try
            {
                TryParsePageParsingIds(out startPageId, out endPageId);
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }

            Parse(startPageId, endPageId);
        }

        private void AbortButton_Click(object sender, RoutedEventArgs e)
        {
            if (webParser != null)
            {
                webParser.Abort();
                webParser.OnDataLoaded -= ShowContent;
            }
        }

        private void Parse(int startPageId, int endPageId)
        {
            webParser = new WebParser<List<string>>(
                new MetacriticParser(),
                new MetacriticParserSettings(startPageId, endPageId)
            );

            webParser.OnDataLoaded += ShowContent;

            webParser.Start();
        }

        private void ShowContent(List<string> games)
        {
            foreach (var game in games)
                contentBlock.AppendText($"{game}\n");
        }

        private void TryParsePageParsingIds(out int startPageId, out int endPageId)
        {
            string stringStartPageId = startPageIdInput.Text;
            if (!int.TryParse(stringStartPageId, out startPageId))
                throw new Exception($"Start page id must be int, {stringStartPageId} given");

            string stringEndPageId = endPageIdInput.Text;
            if (!int.TryParse(stringEndPageId, out endPageId))
                throw new Exception($"End page id must be int, {stringEndPageId} given");
        }
    }
}
