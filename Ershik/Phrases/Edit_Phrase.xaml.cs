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
            if (!Phrase.Text.All(x => Char.IsLetter(x) || Char.IsWhiteSpace(x)))
            {
                MessageBox.Show("Фраза дожна состоять только из букв, цифр или пробелов");
                return;
            }
            if (Phrase.Text.Trim() == "")
            {
                MessageBox.Show("Фраза не может быть пустой");
                return;
            }
            if (Phrase.Text.Length > 24 || Phrase.Text.Length < 1)
            {
                MessageBox.Show("Длина фразы не может превышать 24 символов и быть короче 1");
                return;
            }
            if (Desc.Text.Length > 200)
            {
                MessageBox.Show("Длина описания не может превышать 200 символов");
                return;
            }
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
