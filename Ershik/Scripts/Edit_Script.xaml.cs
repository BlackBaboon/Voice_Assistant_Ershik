using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Логика взаимодействия для Edit_Script.xaml
    /// </summary>
    public partial class Edit_Script : Window
    {
        string script, desc;
        public Edit_Script(string old_script, string old_desc)
        {
            InitializeComponent();
            script = old_script;
            desc = old_desc;
            script.Text = script;
            Desc.Text = old_desc;
        }

        private void Add_Script_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Database_interaction.Change.Change_Script(script, script.Text, Desc.Text);
                this.DialogResult = true;
            }
            catch
            {
                this.DialogResult = false;
                this.Close();
            }
        }

        private void Add_Phrase_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
