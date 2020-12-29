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

namespace ExpenseM.UserControls
{
    /// <summary>
    /// Interaction logic for LabeledTextbox.xaml
    /// </summary>
    public partial class LabeledTextbox : UserControl
    {

        private String _title = "Title";
        private String userInputbox = "";


        public LabeledTextbox()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        //public string Title
        //{
        //    get => _title;
        //    set
        //    {
        //        this._title = value;
        //    }
        //}

        //public string TextboxTitle
        //{
        //    get => userInputbox;
        //    set
        //    {
        //        this.userInputbox = value;
        //    }
        //}
        public string TitleLength { get; set; }

        public int MaxLength { get; set; }
    }
}
