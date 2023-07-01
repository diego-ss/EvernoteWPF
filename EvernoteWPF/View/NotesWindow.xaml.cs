using System.Windows;
using System.Windows.Documents;

namespace EvernoteWPF.View
{
    /// <summary>
    /// Lógica interna para NotesWindow.xaml
    /// </summary>
    public partial class NotesWindow : Window
    {
        public NotesWindow()
        {
            InitializeComponent();
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void SpeechButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void contentRichTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            int ammountCharacters = (new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd)).Text.Trim().Length;
            statusTextBlock.Text = $"Document length: {ammountCharacters} characters";

        }

        private void boldButton_Click(object sender, RoutedEventArgs e)
        {
            var selection = contentRichTextBox.Selection;
            selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
        }
    }
}
