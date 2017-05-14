using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace SimpleWpfApp
{
    public class AppViewModel : INotifyPropertyChanged
    {
        private bool _checkboxChecked;
        private ICommand _setBindingCommand;
        private bool _canExecute;

        public bool CheckboxChecked
        {
            get { return _checkboxChecked; }
            set
            {
                _checkboxChecked = value;
                NotifyPropertyChanged(nameof(CheckboxChecked));
            }
        }        

        public ICommand SetBindingCommand
        {
            get
            {
                if (_setBindingCommand == null)
                {
                    _canExecute = true;
                    _setBindingCommand = new CustomCommand(p => _canExecute, a => SetBinding());
                }
                return _setBindingCommand;
            }
        }

        /// <summary>
        /// Set OneTime binding to TextBox
        /// </summary>
        private void SetBinding()
        {
            foreach (NumericTextBox ntb in FindVisualChildren<NumericTextBox>(Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive)))
            {
                if (ntb.VisibleOneTime)
                {
                    Binding b = new Binding()
                    {
                        Mode = BindingMode.OneTime,
                        Source = CheckboxChecked,
                        Converter = new BooleanToCollapsedConverter()
                    };

                    BindingOperations.SetBinding(ntb, TextBox.VisibilityProperty, b);
                }

                _canExecute = false;
            }
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
