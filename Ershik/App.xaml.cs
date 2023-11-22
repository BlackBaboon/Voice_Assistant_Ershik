using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Ershik
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static SqlConnection Connection = new SqlConnection("server=DAUN;database=VoiceAssistant_Ershik;trusted_connection=true");

        public static List<string> All_Phrases;

        public static Frame MainFrame;

        public static int Current_audio_device;

        public static string Current_profile_name = "Test_profile";
    }
}
