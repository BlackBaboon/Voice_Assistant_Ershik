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
using System.Windows.Shapes;

namespace Ershik
{
    /// <summary>
    /// Логика взаимодействия для Add_Script.xaml
    /// </summary>
    public partial class Add_Script : Window
    {
        public Add_Script()
        {
            InitializeComponent();
        }
        private void Add_Script_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Database_interaction.Add.Insert_Script(Script.Text, Desc.Text);
                this.DialogResult = true;
            }
            catch
            {
                this.DialogResult = false;
                this.Close();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
