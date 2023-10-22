using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
    /// Логика взаимодействия для Command_Editor.xaml
    /// </summary>
    public partial class Command_Editor : Window
    {
        List<string> Commands_List;
        string current_script;
        public Command_Editor(string script)
        {
            InitializeComponent();
            current_script = script;
            Commands_List = Database_interaction.Get.Get_Command(current_script);
            Commands_List.Add("<новая команда>");
            Refill();
            Commands.SelectedIndex = Commands_List.Count-1;
        }
        private void Add_Command_Click(object sender, RoutedEventArgs e)
        {
            Commands_List.Insert(Commands_List.Count-1, Current_Command.Text);
            Refill();
            Commands.SelectedIndex = Commands_List.Count-1;
        }
        private void Delete_Command_Click(object sender, RoutedEventArgs e)
        {
            if (Commands_List.Count == 1 || Commands.SelectedIndex == Commands_List.Count - 1)
                return;
            int index = Commands.SelectedIndex-1;
            Commands_List.RemoveAt(Commands.SelectedIndex); 
            Refill();
            if (index < 0)
                index = 0;
            Commands.SelectedIndex = index;
        }
        private void Refill()
        {
            Commands.Items.Clear();
            foreach (var r in Commands_List)
                Commands.Items.Add(new TextBlock() { Text = r.Length>39?r.Substring(0, 39) +"...":r, FontSize = 20 });
        }
        private void Commands_List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Commands.SelectedIndex == -1)
                return;
            if (Commands.SelectedIndex != Commands_List.Count - 1)
                Current_Command.Text = ((TextBlock)Commands.SelectedItem).Text;
            else Current_Command.Text = "";
        }

        private void Down_Click(object sender, RoutedEventArgs e)
        {
            if (Commands.SelectedIndex >= Commands_List.Count - 2)
                return;
            string temp = Commands_List[Commands.SelectedIndex];
            Commands_List[Commands.SelectedIndex] = Commands_List[Commands.SelectedIndex+1];
            Commands_List[Commands.SelectedIndex+1] = temp;

            Refill();
            Commands.SelectedIndex = 0;
        }

        private void Up_Click(object sender, RoutedEventArgs e)
        {
            if (Commands.SelectedIndex == 0)
                return;
            string temp = Commands_List[Commands.SelectedIndex];

            Commands_List[Commands.SelectedIndex] = Commands_List[Commands.SelectedIndex-1];
            Commands_List[Commands.SelectedIndex-1] = temp;

            Refill();
            Commands.SelectedIndex = 1;
        }

        private void Save_Changes_Click(object sender, RoutedEventArgs e)
        {
            Database_interaction.Delete.Delete_Commands(current_script);
            for(int i = 0; i < Commands_List.Count-1; i++)
            {
                Database_interaction.Add.Insert_Command(current_script, Commands_List[i]);
            }
            this.DialogResult = true;
            this.Close();
        }

        private void Discard_Changes_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
