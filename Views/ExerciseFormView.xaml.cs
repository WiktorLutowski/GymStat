using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GymStat.Views
{
    /// <summary>
    /// Interaction logic for ExerciseFormView.xaml
    /// </summary>
    public partial class ExerciseFormView : UserControl
    {
        public ExerciseFormView()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));

                if (new Regex("[^0-9]+").IsMatch(text))
                    e.CancelCommand();
            }
            else
                e.CancelCommand();
        }
    }
}
