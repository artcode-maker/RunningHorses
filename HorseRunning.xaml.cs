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

namespace RunningHorses
{
    /// <summary>
    /// Логика взаимодействия для HorseRunning.xaml
    /// </summary>
    public partial class HorseRunning : UserControl
    {
        public HorseRunning()
        {
            this.InitializeComponent();
        }

        public int CurrentNumber
        {
            get => (int)GetValue(CurrentNumberProperty);
            set => SetValue(CurrentNumberProperty, value);
        }

        // Using a DependencyProperty as the backing store for CurrentNumber.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentNumberProperty =
            DependencyProperty.Register("CurrentNumber",
                typeof(int),
                typeof(HorseRunning),
                new UIPropertyMetadata(100, new PropertyChangedCallback(CurrentNumberChanged)),
                new ValidateValueCallback(ValidateCurrentNumber));

        public static bool ValidateCurrentNumber(object value)
        {
            // Just a simple business rule. Value must be between 0 and 500.
            return Convert.ToInt32(value) >= 0 && Convert.ToInt32(value) <= 500;
        }
        private static void CurrentNumberChanged(
            DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            // Cast the DependencyObject into ShowNumberControl.
            HorseRunning c = (HorseRunning)depObj;

            // Get the Label control in the ShowNumberControl.
            Label theLabel = c.HorseControl;

            // Set the Label with the new value.
            theLabel.Content = args.NewValue.ToString();
        }
    }
}
