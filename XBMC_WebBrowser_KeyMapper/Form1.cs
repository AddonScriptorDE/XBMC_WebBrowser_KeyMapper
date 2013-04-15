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
    public partial class Form1 : Form
    {
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(int vKey);
        private ArrayList textBoxes;
        private String userDataFolder;

        public Form1(String[] args)
        {
            InitializeComponent();
            userDataFolder = "";
            if (args.Length > 0)
            {
                userDataFolder = args[0].Replace("\"", "");
            }

            textBoxes = new ArrayList();
            textBoxes.Add(textBox1);
            textBoxes.Add(textBox2);
            textBoxes.Add(textBox3);
            textBoxes.Add(textBox4);
            textBoxes.Add(textBox5);
            textBoxes.Add(textBox6);
            textBoxes.Add(textBox7);
            textBoxes.Add(textBox8);
            textBoxes.Add(textBox9);
            textBoxes.Add(textBox10);
            textBoxes.Add(textBox11);
            textBoxes.Add(textBox12);
            textBoxes.Add(textBox13);
            textBoxes.Add(textBox14);
            textBoxes.Add(textBox15);
            textBoxes.Add(textBox16);

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
                        else if (spl[0] == "Click")
                            textBox9.Text = spl[1].Trim();
                        else if (spl[0] == "DoubleClick")
                            textBox10.Text = spl[1].Trim();
                        else if (spl[0] == "ZoomIn")
                            textBox11.Text = spl[1].Trim();
                        else if (spl[0] == "ZoomOut")
                            textBox12.Text = spl[1].Trim();
                        else if (spl[0] == "EnterURL")
                            textBox13.Text = spl[1].Trim();
                        else if (spl[0] == "Magnifier")
                            textBox14.Text = spl[1].Trim();
                        else if (spl[0] == "CloseWindow")
                            textBox15.Text = spl[1].Trim();
                        else if (spl[0] == "ShowKeyboard")
                            textBox16.Text = spl[1].Trim();

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
                keys = keys.Trim();
                if (keys == "Down" && !buttonSave.Focused && !buttonCancel.Focused)
                {
                    SendKeys.Send("{TAB}");
                }
                else if (keys == "Up" && !buttonSave.Focused && !buttonCancel.Focused)
                {
                    SendKeys.Send("+{TAB}");
                }
                else if (keys != "")
                {
                    foreach (TextBox txt in textBoxes)
                    {
                        if (txt.Focused)
                        {
                            String temp = keys;
                            if (temp.StartsWith("ShiftKey "))
                                temp = temp.Substring(9);
                            if (temp.StartsWith("Menu "))
                                temp = temp.Substring(5);
                            txt.Text = temp;
                            SendKeys.Send("{TAB}");
                            break;
                        }
                    }
                }
            }
            catch
            {
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
            str.WriteLine("Click=" + textBox9.Text);
            str.WriteLine("DoubleClick=" + textBox10.Text);
            str.WriteLine("ZoomIn=" + textBox11.Text);
            str.WriteLine("ZoomOut=" + textBox12.Text);
            str.WriteLine("EnterURL=" + textBox13.Text);
            str.WriteLine("Magnifier=" + textBox14.Text);
            str.WriteLine("CloseWindow=" + textBox15.Text);
            str.WriteLine("ShowKeyboard=" + textBox16.Text);
            str.Close();
            Application.Exit();
        }
    }
}
