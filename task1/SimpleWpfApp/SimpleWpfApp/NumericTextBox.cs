using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SimpleWpfApp
{
    public class NumericTextBox : TextBox
    {

        public static readonly DependencyProperty VisibleOneTimeProperty = 
            DependencyProperty.Register("VisibleOneTime", typeof(bool), typeof(NumericTextBox));

        public bool VisibleOneTime
        {
            get
            {
                return (bool)this.GetValue(VisibleOneTimeProperty);
            }
            set
            {
                this.SetValue(VisibleOneTimeProperty, value);
            }
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);
            e.Handled = e.Key == Key.Space;
        }

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            base.OnPreviewTextInput(e);
            e.Handled = !IsTextAllowed(this.Text + e.Text);
        }

        private bool IsTextAllowed(string text)
        {
            Regex regex = new Regex(@"^[1-9]\d*$"); //allow only positive number
            return regex.IsMatch(text);
        }
    }
}
