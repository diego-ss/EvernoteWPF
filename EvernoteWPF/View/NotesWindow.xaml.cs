using EvernoteWPF.ViewModel;
using EvernoteWPF.ViewModel.Helpers;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;

namespace EvernoteWPF.View
{
    /// <summary>
    /// Lógica interna para NotesWindow.xaml
    /// </summary>
    public partial class NotesWindow : Window
    {
        NotesViewModel viewModel;

        public NotesWindow()
        {
            InitializeComponent();

            viewModel = Resources["vm"] as NotesViewModel;
            viewModel.SelectedNoteChanged += ViewModel_SelectedNoteChanged;

            var fontFamilies = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            fontFamilyComboBox.ItemsSource = fontFamilies;

            var fontSizes = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 28, 48, 72 };
            fontSizeComboBox.ItemsSource = fontSizes;
        }

        private void ViewModel_SelectedNoteChanged(object sender, EventArgs e)
        {
            contentRichTextBox.Document.Blocks.Clear();

            if(viewModel.SelectedNote != null)
            {
                if (!string.IsNullOrEmpty(viewModel.SelectedNote.FileLocation))
                {
                    FileStream fileStream = new FileStream(viewModel.SelectedNote.FileLocation, FileMode.Open);
                    var content = new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd);
                    content.Load(fileStream, DataFormats.Rtf);
                    fileStream.Close();
                }
            }
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

            if((sender as ToggleButton).IsChecked.Value)
                selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
            else
                selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Normal);
        }

        private void contentRichTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var selectWeight = contentRichTextBox.Selection.GetPropertyValue(Inline.FontWeightProperty);
            var selectStyle = contentRichTextBox.Selection.GetPropertyValue(Inline.FontStyleProperty);
            var selectDecoration = contentRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty);

            boldButton.IsChecked = (selectWeight != DependencyProperty.UnsetValue) && selectWeight.Equals(FontWeights.Bold);
            italicButton.IsChecked = (selectStyle != DependencyProperty.UnsetValue) && selectStyle.Equals(FontStyles.Italic);
            underlineButton.IsChecked = (selectDecoration != DependencyProperty.UnsetValue) && selectDecoration.Equals(TextDecorations.Underline);

            fontFamilyComboBox.SelectedItem = contentRichTextBox.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            fontSizeComboBox.Text = contentRichTextBox.Selection.GetPropertyValue(Inline.FontSizeProperty).ToString();
        }

        private void italicButton_Click(object sender, RoutedEventArgs e)
        {
            var selection = contentRichTextBox.Selection;

            if ((sender as ToggleButton).IsChecked.Value)
                selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Italic);
            else
                selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Normal);
        }

        private void underlineButton_Click(object sender, RoutedEventArgs e)
        {
            var selection = contentRichTextBox.Selection;

            if ((sender as ToggleButton).IsChecked.Value)
                selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            else
                selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
        }

        private void fontFamilyComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(fontFamilyComboBox.SelectedItem != null)
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, fontFamilyComboBox.SelectedItem);
        }

        private void fontSizeComboBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (fontSizeComboBox.SelectedValue != null)
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontSizeProperty, fontSizeComboBox.Text);
        }

        private void SaveNoteButton_Click(object sender, RoutedEventArgs e)
        {
            string rtfFile = Path.Combine(Environment.CurrentDirectory, $"{viewModel.SelectedNote.Id}.rtf");
            viewModel.SelectedNote.FileLocation = rtfFile;

            DatabaseHelper.Update(viewModel.SelectedNote);

            FileStream fileStream = new FileStream(rtfFile, FileMode.Create);
            var content = new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd);
            content.Save(fileStream, DataFormats.Rtf);
            fileStream.Close();
        }
    }
}
