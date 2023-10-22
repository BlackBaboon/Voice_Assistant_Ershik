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

namespace Ershik
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WaveIn waveIn;
        WaveFileWriter writer;
        string outputFilename = "demo0.wav";
        public MainWindow()
        {
            InitializeComponent();
            App.MainFrame = TitleFrame;
            Phrase_btn_Click(null, null);
            Speech_Recognize();
        }

        private async void Speech_Recognize()
        {
            int file_index = 0;
            while (true)
            {
                await Task.Delay(100);
                waveIn = new WaveIn();
                waveIn.DeviceNumber = App.Current_audio_device;
                waveIn.DataAvailable += waveIn_DataAvailable;
                waveIn.RecordingStopped += new EventHandler<NAudio.Wave.StoppedEventArgs>(waveIn_RecordingStopped);
                waveIn.WaveFormat = new WaveFormat(16000, 1);
                writer = new WaveFileWriter(outputFilename, waveIn.WaveFormat);
                waveIn.StartRecording();
                await Task.Delay(5000);
                waveIn.StopRecording();
                await Task.Delay(100);
                await VoiceToText();
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
        private async Task VoiceToText()
        {
            WebRequest request = WebRequest.Create("https://www.google.com/speech-api/v2/recognize?output=json&lang=ru-RU&key=AIzaSyBOti4mM-6x9WDnZIjIeyEU21OpBXqWBgw");
            request.Method = "POST";
            byte[] byteArray = File.ReadAllBytes(outputFilename);
            request.ContentType = "audio/l16; rate=16000";
            request.ContentLength = byteArray.Length;
            request.GetRequestStream().Write(byteArray, 0, byteArray.Length);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());

            string strtrs = reader.ReadToEnd();
            var rg = new Regex(@"transcript" + '"' + ":" + '"' + "([A-Z, А-Я, a-z,а-я, ,0-9]*)");
            var result = rg.Match(strtrs).Groups[1].Value;

            reader.Close();
            response.Close();
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
