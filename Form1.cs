using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Win11_Tweaker
{
    public partial class MainForm : Form
    {
        private bool isDragging = false;
        private Point lastCursor;
        private Point lastForm;
        public MainForm()
        {
            InitializeComponent();
            this.MouseDown += new MouseEventHandler(Form_MouseDown);
            this.MouseMove += new MouseEventHandler(Form_MouseMove);
            this.MouseUp += new MouseEventHandler(Form_MouseUp);
        }
        // Обработчик события MouseDown
        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastCursor = Cursor.Position;
                lastForm = this.Location;
            }
        }

        // Обработчик события MouseMove
        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                int xDiff = Cursor.Position.X - lastCursor.X;
                int yDiff = Cursor.Position.Y - lastCursor.Y;
                this.Location = new Point(lastForm.X + xDiff, lastForm.Y + yDiff);
            }
        }

        // Обработчик события MouseUp
        private void Form_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked) // раскрытие проца за 5 копеек
            {
                RegistryKey LocalMachine = Registry.LocalMachine;
                RegistryKey CPULimit = LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile");
                CPULimit.SetValue("SystemResponsiveness", "a", RegistryValueKind.DWord);
            }
            
            if (checkBox2.Checked) // дефендер активити дестроер
            {
                RegistryKey LocalMachine = Registry.LocalMachine;
                RegistryKey DefenderMain = LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows Defender");
                DefenderMain.SetValue("DisableAntiVirus", "1", RegistryValueKind.DWord);
                DefenderMain.SetValue("DisableRoutinelyTakingAction", "1", RegistryValueKind.DWord);
                RegistryKey DefenderReal = LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection");
                DefenderReal.SetValue("DisableBehaviorMonitoring", "1", RegistryValueKind.DWord);
                DefenderReal.SetValue("DisableIOAVProtection", "1", RegistryValueKind.DWord);
                DefenderReal.SetValue("DisableOnAccessProtection", "1", RegistryValueKind.DWord);
                DefenderReal.SetValue("DisableRawWriteNotification", "1", RegistryValueKind.DWord);
                DefenderReal.SetValue("DisableRealtimeMonitoring", "1", RegistryValueKind.DWord);
                DefenderReal.SetValue("DisableScanOnRealtimeEnable", "1", RegistryValueKind.DWord);
                DefenderReal.SetValue("DisableScriptScanning", "1", RegistryValueKind.DWord);
            }

            if (checkBox3.Checked)
            {
                RegistryKey LocalMachine = Registry.LocalMachine;
                RegistryKey Edge = LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Edge");
                Edge.SetValue("HubsSidebarEnabled", "0", RegistryValueKind.DWord);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
