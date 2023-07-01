using EvernoteWPF.Model;
using System;
using System.Windows;
using System.Windows.Controls;

namespace EvernoteWPF.View.UserControls
{
    /// <summary>
    /// Interação lógica para DisplayNotebookControl.xam
    /// </summary>
    public partial class DisplayNotebookControl : UserControl
    {
        public Notebook Notebook
        {
            get { return (Notebook)GetValue(NotebookProperty); }
            set { SetValue(NotebookProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NotebookProperty =
            DependencyProperty.Register("Notebook", typeof(Notebook), typeof(DisplayNotebookControl), new PropertyMetadata(null, SetValues));

        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DisplayNotebookControl notebookControl = (DisplayNotebookControl)d;
            
            if(notebookControl != null)
            {
                notebookControl.DataContext = notebookControl.Notebook;
            }
        }

        public DisplayNotebookControl()
        {
            InitializeComponent();
        }
    }
}
