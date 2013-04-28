using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace XBMC_WebBrowser_KeyMapper
{
    public partial class FormMain : Form
    {
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(int vKey);
        private String userDataFolder;

        public FormMain(String[] args)
        {
            InitializeComponent();
            userDataFolder = "";
            if (args.Length > 0)
            {
                userDataFolder = args[0].Replace("\"", "");
            }

            String file = "";
            //When using Windows
            if (File.Exists(userDataFolder + "\\keymap"))
            {
                file = userDataFolder + "\\keymap";

            }
            //When using Wine
            else if (File.Exists("C:\\xbmc_webbrowser\\keymap"))
            {
                file = "C:\\xbmc_webbrowser\\keymap";
            }
            if (File.Exists(file))
            {
                StreamReader str = new StreamReader(file);
                String line;
                while ((line = str.ReadLine()) != null)
                {
                    if (line.Contains("="))
                    {
                        String[] spl = line.Split('=');
                        if (spl[0] == "Up")
                            textBox1.Text = spl[1].Trim();
                        else if (spl[0] == "Down")
                            textBox2.Text = spl[1].Trim();
                        else if (spl[0] == "Left")
                            textBox3.Text = spl[1].Trim();
                        else if (spl[0] == "Right")
                            textBox4.Text = spl[1].Trim();
                        else if (spl[0] == "UpLeft")
                            textBox5.Text = spl[1].Trim();
                        else if (spl[0] == "UpRight")
                            textBox6.Text = spl[1].Trim();
                        else if (spl[0] == "DownLeft")
                            textBox7.Text = spl[1].Trim();
                        else if (spl[0] == "DownRight")
                            textBox8.Text = spl[1].Trim();
                        else if (spl[0] == "ToggleMouse")
                            textBox9.Text = spl[1].Trim();
                        else if (spl[0] == "Click")
                            textBox10.Text = spl[1].Trim();
                        else if (spl[0] == "ZoomIn")
                            textBox11.Text = spl[1].Trim();
                        else if (spl[0] == "ZoomOut")
                            textBox12.Text = spl[1].Trim();
                        else if (spl[0] == "ShowContextMenu")
                            textBox13.Text = spl[1].Trim();
                        else if (spl[0] == "CloseWindow")
                            textBox14.Text = spl[1].Trim();
                        else if (spl[0] == "DoubleClick")
                            textBox15.Text = spl[1].Trim();
                        else if (spl[0] == "EnterURL")
                            textBox16.Text = spl[1].Trim();
                        else if (spl[0] == "ShowKeyboard")
                            textBox17.Text = spl[1].Trim();
                        else if (spl[0] == "Magnifier")
                            textBox18.Text = spl[1].Trim();
                        else if (spl[0] == "ShowFavourites")
                            textBox19.Text = spl[1].Trim();
                        else if (spl[0] == "ShowShortcuts")
                            textBox20.Text = spl[1].Trim();
                        else if (spl[0] == "PressTAB")
                            textBox21.Text = spl[1].Trim();
                        else if (spl[0] == "PressESC")
                            textBox22.Text = spl[1].Trim();
                        else if (spl[0] == "PressF5")
                            textBox23.Text = spl[1].Trim();
                    }
                }
                str.Close();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                String keys = "";
                foreach (int i in Enum.GetValues(typeof(Keys)))
                {
                    if (GetAsyncKeyState(i) == -32767)
                    {
                        keys += Enum.GetName(typeof(Keys), i) + " ";
                    }
                }
                String[] usedKeys = { "Left", "Right", "Back", "Escape", "Tab", "F5" };
                keys = keys.Trim();
                if (keys == "Down")
                {
                    this.SelectNextControl(ActiveControl, true, true, true, true);
                }
                else if (keys == "Up")
                {
                    this.SelectNextControl(ActiveControl, false, true, true, true);
                }
                else if (keys == "Enter" && !buttonSave.Focused && !buttonCancel.Focused)
                {
                    ((TextBox)ActiveControl).Text = "";
                }
                else if (keys != "" && !usedKeys.Contains(keys))
                {
                    foreach (Control control in this.groupBox1.Controls)
                    {
                        if (control is TextBox && ((TextBox)control).Focused)
                        {
                            String temp = keys;
                            if (temp.StartsWith("ShiftKey "))
                                temp = temp.Substring(9);
                            if (temp.StartsWith("Menu "))
                                temp = temp.Substring(5);
                            ((TextBox)control).Text = temp;
                            this.SelectNextControl(ActiveControl, true, true, true, true);
                            break;
                        }
                    }
                    foreach (Control control in this.groupBox2.Controls)
                    {
                        if (control is TextBox && ((TextBox)control).Focused)
                        {
                            String temp = keys;
                            if (temp.StartsWith("ShiftKey "))
                                temp = temp.Substring(9);
                            if (temp.StartsWith("Menu "))
                                temp = temp.Substring(5);
                            ((TextBox)control).Text = temp;
                            this.SelectNextControl(ActiveControl, true, true, true, true);
                            break;
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void button_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                e.IsInputKey = true;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            StreamWriter str;
            String dir = "";
            //When using Windows
            if (Directory.Exists(userDataFolder))
            {
                dir = userDataFolder;
                
            }
            //When using Wine
            else
            {
                dir = "C:\\xbmc_webbrowser";
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
            }
            str = new StreamWriter(dir + "\\keymap", false);
            str.WriteLine("Up=" + textBox1.Text);
            str.WriteLine("Down=" + textBox2.Text);
            str.WriteLine("Left=" + textBox3.Text);
            str.WriteLine("Right=" + textBox4.Text);
            str.WriteLine("UpLeft=" + textBox5.Text);
            str.WriteLine("UpRight=" + textBox6.Text);
            str.WriteLine("DownLeft=" + textBox7.Text);
            str.WriteLine("DownRight=" + textBox8.Text);
            str.WriteLine("ToggleMouse=" + textBox9.Text);
            str.WriteLine("Click=" + textBox10.Text);
            str.WriteLine("ZoomIn=" + textBox11.Text);
            str.WriteLine("ZoomOut=" + textBox12.Text);
            str.WriteLine("ShowContextMenu=" + textBox13.Text);
            str.WriteLine("CloseWindow=" + textBox14.Text);
            str.WriteLine("DoubleClick=" + textBox15.Text);
            str.WriteLine("EnterURL=" + textBox16.Text);
            str.WriteLine("ShowKeyboard=" + textBox17.Text);
            str.WriteLine("Magnifier=" + textBox18.Text);
            str.WriteLine("ShowFavourites=" + textBox19.Text);
            str.WriteLine("ShowShortcuts=" + textBox20.Text);
            str.WriteLine("PressTAB=" + textBox21.Text);
            str.WriteLine("PressESC=" + textBox22.Text);
            str.WriteLine("PressF5=" + textBox23.Text);
            str.Close();
            Application.Exit();
        }
    }
}
