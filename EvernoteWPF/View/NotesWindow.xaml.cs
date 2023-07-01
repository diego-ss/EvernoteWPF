using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
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

        private async void SpeechButton_Click(object sender, RoutedEventArgs e)
        {
            string region = "eastus";
            string key = "fef5e4c5d27c43d5a8495e78e671463d";

            var speechConfig = SpeechConfig.FromSubscription(key, region);

            using (var audioConfig = AudioConfig.FromDefaultMicrophoneInput())
            {

                using (var recognizer = new SpeechRecognizer(speechConfig, audioConfig))
                {
                    var result = await recognizer.RecognizeOnceAsync();
                    contentRichTextBox.Document.Blocks.Add(new Paragraph(new Run(result.Text)));
                }
            }
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
