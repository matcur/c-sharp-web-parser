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
using Parser.Core.Imdb;

namespace Parser.Pages
{
    /// <summary>
    /// Interaction logic for ImdbPage.xaml
    /// </summary>
    public partial class ImdbPage : Page
    {
        private WebParser<Dictionary<string, string>> webParser;

        public ImdbPage()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            int pageId;
            string stringPageId = startPageIdInput.Text;
            if (!int.TryParse(stringPageId, out pageId))
            {
                MessageBox.Show($"You writed {stringPageId}, integer must be writed");
                return;
            }

            Parse(pageId);
        }

        private void AbortButton_Click(object sender, RoutedEventArgs e)
        {
            if (webParser != null)
            {
                webParser.OnDataLoaded -= AddContent;
                webParser.Abort();
            }
        }

        private void Parse(int pageId)
        {
            webParser = new WebParser<Dictionary<string, string>>(
                new ImdbParser(),
                MakeImdbParserSettings(pageId)
            );

            webParser.OnDataLoaded += AddContent;

            webParser.Start();
        }

        private void AddContent(Dictionary<string, string> aTags)
        {
            Dispatcher.Invoke(() =>
            {
                int i = 0;
                foreach (var item in aTags)
                {
                    var text = $"{++i}){item.Key} {item.Value}\n";
                    contentBlock.AppendText(text);
                }

                MessageBox.Show("Successfuly loaded");
            });
        }

        private Paragraph MakeParagraphFromInline(Inline inline)
        {
            var paragraph = new Paragraph();
            paragraph.Inlines.Add(inline);

            return paragraph;
        }

        private ImdbParserSettings MakeImdbParserSettings(int startPageId)
        {
            var dict = new Dictionary<string, string[]>();
            dict.Add("title_type", new[] { "tv_series", "tv_miniseries" });

            return new ImdbParserSettings(startPageId, dict);
        }
    }
}
