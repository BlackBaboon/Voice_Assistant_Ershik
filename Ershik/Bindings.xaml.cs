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

namespace Ershik
{
    /// <summary>
    /// Логика взаимодействия для Bindings.xaml
    /// </summary>
    public partial class Bindings : Page
    {
        List<string> Scripts_List, Phrases_List;
        List<string> Current_Binds = new List<string>();

        public Bindings()
        {
            InitializeComponent();
            Scripts_List = Database_interaction.Get.Get_Scripts().Select(x => x[0]).ToList();
            Phrases_List = Database_interaction.Get.Get_Phrases().Select(x => x[0]).ToList();
            Scripts.ItemsSource = Scripts_List;
            Phrases.ItemsSource = Phrases_List;
            Scripts.SelectedIndex = 0;
            Phrases.SelectedIndex = 0;
            Current_Binds = new List<string>();
        }

        private void Phrases_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Phrases.SelectedIndex == -1)
                return;
            Current_Binds.Clear();
            foreach (var r in Database_interaction.Get.Get_Binds(Phrases.SelectedItem.ToString()))
                Current_Binds.Add(r); 
            Fill();
        }
        private void Fill()
        {
            Binds.Items.Clear();
            foreach (var r in Current_Binds)
                Binds.Items.Add(r);
            Binds.SelectedIndex = 0;
            Binds_Better.Text = String.Join("\n", Current_Binds);
        }

        private void Save_Binds_Click(object sender, RoutedEventArgs e)
        {
            Database_interaction.Delete.UnBind_Phrase(Phrases.SelectedItem.ToString());
            foreach (string r in Binds.Items)
            {
                Database_interaction.Add.Bind_Phrase_Script(Phrases.SelectedItem.ToString(), r);
            }
        }

        private void Clear_Binds_Click(object sender, RoutedEventArgs e)
        {
            Binds.Items.Clear();
            Binds_Better.Text = "";
        }
        private void Up_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Binds.SelectedIndex == 0)
                return;
            int ind = Binds.SelectedIndex-1;

            string temp = Current_Binds[Binds.SelectedIndex];
            Current_Binds[Binds.SelectedIndex] = Current_Binds[Binds.SelectedIndex-1];
            Current_Binds[Binds.SelectedIndex - 1] = temp;

            MessageBox.Show(String.Join("\n", Current_Binds));

            Fill();
            Binds.SelectedIndex = ind;
        }

        private void Down_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Binds.SelectedIndex == Current_Binds.Count-1)
                return;

            int ind = Binds.SelectedIndex+1;

            string temp = Current_Binds[Binds.SelectedIndex];
            Current_Binds[Binds.SelectedIndex] = Current_Binds[Binds.SelectedIndex+1];
            Current_Binds[Binds.SelectedIndex + 1] = temp;

            MessageBox.Show(String.Join("\n", Current_Binds));

            Fill();
            Binds.SelectedIndex = ind;
        }

        private void Bind_Button_Click(object sender, RoutedEventArgs e)
        {
            Current_Binds.Add(Scripts.SelectedItem.ToString());
            Fill();
            Binds.SelectedIndex = Current_Binds.Count - 1;
        }
    }
}
