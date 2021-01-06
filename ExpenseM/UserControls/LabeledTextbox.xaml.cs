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
using ExpenseM.Utilities;

namespace ExpenseM.UserControls
{
    /// <summary>
    /// Interaction logic for LabeledTextbox.xaml
    /// </summary>
    public partial class LabeledTextbox : UserControl
    {

      

        public LabeledTextbox()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public string LabelName { get; set; }
        public string TextboxInput { get; set; }
        
        public string TitleLength { get; set; }

        public int MaxLength { get; set; }

        public LostFocusTextbox lost1 { set; get; }

       
        private void mybutton_Click(object sender, RoutedEventArgs e)
        {

        }

    private void Textbox_MouseEnter(object sender, MouseEventArgs e)
    {
      this.UserInputbox.Background = Brushes.AliceBlue;
    }

    private void Textbox_MouseLeave(object sender, MouseEventArgs e)
    {
      this.UserInputbox.Background = Brushes.White;

    }
  }
}
