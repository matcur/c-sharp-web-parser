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
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Parser.Core;
using Parser.Core.Imdb;

namespace Parser.Pages
{
    /// <summary>
    /// Interaction logic for ImdbPage.xaml
    /// </summary>
    public partial class ImdbPage : Page
    {
        private WebParser webParser;

        public ImdbPage()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            int startPageNumber;
            try
            {
                TryParsePageNumber(out startPageNumber);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }

            Parse(startPageNumber);
        }

        private void AbortButton_Click(object sender, RoutedEventArgs e)
        {
            if (webParser != null)
            {
                webParser.OnDataLoaded -= ShowContent;
                webParser.Abort();
            }
        }

        private void Parse(int pageNumber)
        {
            webParser = new WebParser(
                new ImdbParser(),
                MakeImdbParserSettings(pageNumber)
            );

            webParser.OnDataLoaded += ShowContent;
            webParser.Start();
        }

        private void ShowContent(IHtmlCollection<IElement> elements)
        {
            Dispatcher.Invoke(() =>
            {
                int i = 0;

                var aTags = elements.OfType<IHtmlAnchorElement>();
                foreach (var item in aTags)
                {
                    var text = $"{++i}){item.TextContent} - {item.Href}\n";
                    contentBlock.AppendText(text);
                }

                MessageBox.Show("Successfuly loaded");
            });
        }

        private ImdbParserSettings MakeImdbParserSettings(int startPageId)
        {
            var dict = new Dictionary<string, string[]>();
            dict.Add("title_type", new[] { "tv_series", "tv_miniseries" });

            return new ImdbParserSettings(startPageId, dict);
        }

        private void TryParsePageNumber(out int startPageId)
        {
            string stringStartPageNumber = startPageNumberInput.Text;
            if (!int.TryParse(stringStartPageNumber, out startPageId))
            {
                MessageBox.Show($"You writed {stringStartPageNumber}, integer must be writed");
                throw new Exception($"String must be int, {stringStartPageNumber} given");
            }
        }
    }
}
