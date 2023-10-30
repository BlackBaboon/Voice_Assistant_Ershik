using System;
using System.Speech.Recognition;
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
using System.IO;
using System.Net;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using NAudio;
using NAudio.Wave;
using System.IO.Compression;
using System.Speech.Synthesis;
using System.Threading;
using System.Diagnostics;
using System.Security;
using NAudio.CoreAudioApi;
using IronPython;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace Ershik
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string voice_to_text = "";
        bool on_write = false;
        int counter = 0;
        WaveIn waveIn;
        WaveFileWriter writer;
        string outputFilename = "demo0.wav";
        public MainWindow()
        {
            Choose_Profile profile = new Choose_Profile();
            profile.ShowDialog();
            if (profile.DialogResult == false)
                Application.Current.Shutdown();

            InitializeComponent();
            App.MainFrame = TitleFrame;
            Phrase_btn_Click(null, null);
            Speech_Recognize();
            App.All_Phrases = Database_interaction.Get.Get_Phrases().Select(x => x[0].ToLower()).ToList();
        }

        private async void Speech_Recognize()
        {
            
            int file_index = 0;
            while (true)
            {
                await Task.Delay(10);
                waveIn = new WaveIn();
                waveIn.DeviceNumber = App.Current_audio_device;
                waveIn.DataAvailable += waveIn_DataAvailable;
                waveIn.RecordingStopped += new EventHandler<NAudio.Wave.StoppedEventArgs>(waveIn_RecordingStopped);
                waveIn.WaveFormat = new WaveFormat(16000, 1);
                writer = new WaveFileWriter(outputFilename, waveIn.WaveFormat);
                waveIn.StartRecording();
                await Task.Delay(5000);
                waveIn.StopRecording();
                await Task.Delay(10);
                await VoiceToText(file_index);
                file_index += 1;
                file_index = file_index % 3;
                outputFilename = $"demo{file_index}.wav";
            }
        }
        void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            writer.WriteData(e.Buffer, 0, e.BytesRecorded);
        }

        void waveIn_RecordingStopped(object sender, EventArgs e)
        {
            waveIn.Dispose();
            waveIn = null;
            writer.Close();
            writer = null;
        }
        private async Task VoiceToText(int file_index)
        {
            string interpreter = $@"{Directory.GetCurrentDirectory()}\SpeechToText\venv\Scripts\python.exe";
            string script_path = $@"{Directory.GetCurrentDirectory()}\SpeechToText\main.py";
            string recorded = "";
            using (Process P = new Process())
            {
                P.StartInfo.FileName = "cmd.exe";
                P.StartInfo.RedirectStandardInput = true;
                P.StartInfo.RedirectStandardOutput = true;
                P.StartInfo.CreateNoWindow = true;
                P.StartInfo.UseShellExecute = false;
                P.StartInfo.RedirectStandardOutput = true;
                P.StartInfo.RedirectStandardError = true;
                P.StartInfo.RedirectStandardInput = true;
                P.Start();
                using (var sw = new StreamWriter(P.StandardInput.BaseStream, Encoding.GetEncoding(866)))
                {
                    string command = $"\"{interpreter}\" \"{script_path}\" {file_index}";
                    sw.WriteLine(command);
                    sw.Flush();
                    sw.Close();
                }

                while (!P.StandardOutput.EndOfStream)
                {
                    if (P.StandardOutput.ReadLine() == file_index.ToString())
                    {
                        recorded = P.StandardOutput.ReadLine().Trim().ToLower()+"\n";
                        break;
                    }
                }
                ceck1.Text = recorded;
                P.WaitForExit();
            }
            
            if (!on_write)
            {
                voice_to_text = recorded;
            }
            else
            {
                voice_to_text += recorded;
                counter++;
                foreach(var r in App.All_Phrases)
                {
                    if (voice_to_text.ToLower().Contains(r.ToLower()))
                    {
                        Database_interaction.Execute.Execute_Phrase(r,false);
                        voice_to_text = "";
                        counter = 3;
                        break;
                    }
                }
                if(counter == 3)
                {
                    on_write=false;
                    voice_to_text = "";
                }
            }
            if(voice_to_text.ToLower().Contains("ёршик") & !on_write)
            {
                counter = 0;
                on_write = true;
            }
            
        }

        private void Phrase_btn_Click(object sender, RoutedEventArgs e)
        {
            App.MainFrame.Navigate(new Phrase_Editor());
        }

        private void Script_btn_Click(object sender, RoutedEventArgs e)
        {
            App.MainFrame.Navigate(new Script_Editor());
        }

        private void Bind_btn_Click(object sender, RoutedEventArgs e)
        {
            App.MainFrame.Navigate(new Bindings());
        }

        private void ChangeProfile_btn_Click(object sender, RoutedEventArgs e)
        {
            Choose_Profile profile = new Choose_Profile();
            profile.ShowDialog();
            if(profile.DialogResult == true)
            {
                Phrase_btn_Click(null, null);
            }
        }

        private void Settings_btn_Click(object sender, RoutedEventArgs e)
        {
            Choose_Audio_Device choose_Audio_Device = new Choose_Audio_Device();
            if (choose_Audio_Device.ShowDialog() == true)
            {
                MessageBox.Show("Текущее аудиоустройство успешно изменено");
            }
        }
    }
}
