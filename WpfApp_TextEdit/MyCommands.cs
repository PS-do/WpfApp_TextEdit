using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp_TextEdit
{
    internal class MyCommands
    {
        public static RoutedUICommand Info { get; set; }
        static MyCommands()
        {
            InputGestureCollection inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.H, ModifierKeys.Control, "Ctrl+H"));
            Info = new RoutedUICommand("_Справка", "Info", typeof(MyCommands), inputs);
        }
    }
}
