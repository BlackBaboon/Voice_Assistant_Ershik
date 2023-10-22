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
    /// Логика взаимодействия для Edit_Phrase.xaml
    /// </summary>
    public partial class Edit_Phrase : Window
    {
        string phrase, desc;
        public Edit_Phrase(string old_phrase, string old_desc)
        {
            InitializeComponent();
            phrase = old_phrase;
            desc = old_desc;
            Phrase.Text = phrase;
            Desc.Text = old_desc;
        }

        private void Add_Phrase_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Database_interaction.Change.Change_Phrase(phrase, Phrase.Text, Desc.Text);
                this.DialogResult = true;
            }
            catch
            {
                this.DialogResult= false;
                this.Close();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
