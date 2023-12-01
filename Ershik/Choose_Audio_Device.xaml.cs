using NAudio.CoreAudioApi;
using NAudio.Wave;
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
    /// Логика взаимодействия для Choose_Audio_Device.xaml
    /// </summary>
    public partial class Choose_Audio_Device : Window
    {
        public Choose_Audio_Device()
        {
            InitializeComponent();
            Devices.Items.Clear();
            foreach (var r in GetInputAudioDevices())
            {
                Devices.Items.Add(r);
            }
        }
        public Dictionary<string, MMDevice> GetInputAudioDevices()
        {
            Dictionary<string, MMDevice> retVal = new Dictionary<string, MMDevice>();
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            int waveInDevices = WaveIn.DeviceCount;
            for (int waveInDevice = 0; waveInDevice < waveInDevices; waveInDevice++)
            {
                WaveInCapabilities deviceInfo = WaveIn.GetCapabilities(waveInDevice);
                foreach (MMDevice device in enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.All))
                {
                    if (device.FriendlyName.StartsWith(deviceInfo.ProductName))
                    {
                        retVal.Add(device.FriendlyName, device);
                        break;
                    }
                }
            }
            Devices.SelectedIndex = 0;
            return retVal;
        }

        private void OK_Button_Click(object sender, RoutedEventArgs e)
        {
            App.Current_audio_device = Devices.SelectedIndex;
            this.DialogResult = true;
            this.Close();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void RestoreCopy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Database_interaction.Restore.Restore_Copy();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void CreateCopy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Database_interaction.Restore.Create_Copy();
                MessageBox.Show("Восстановление завершено, перезапустите приложение");
                App.Current.Shutdown();
            }
            catch
            {
                MessageBox.Show("Ошибка восстановления");
            }

        }
    }
}
