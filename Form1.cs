using DragNDrop.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DragNDrop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        void panel2_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Aquamarine,3);
            e.Graphics.DrawRectangle(pen, 1, 1, panel2.Width-3, panel2.Height-3) ;
        }

        async void panel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                arethere = true; //for screamer
                e.Effect = DragDropEffects.Copy;
                label2.Text = "О да, да, сюда файлик...";
                pictureBox1.Visible = true; pictureBox1.BackgroundImage = Resources.аниме_png_лицо_1;
                BackColor = SystemColors.Control;
                //рамка
                Pen pen = new Pen(Color.DarkGray,3);
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                Graphics g = panel1.CreateGraphics();
                Rectangle rect = new Rectangle(1, 1, panel1.Width-3, panel1.Height-3);
                g.DrawRectangle(pen,rect);
            }
        }

        void panel1_DragDrop(object sender, DragEventArgs e)
        {
            arethere = true; //for screamer
            List<string> arr = new List<string> { };
            label1.Text = "";
            pictureBox1.Visible = false; BackColor = SystemColors.Control;
            foreach (string obj in (string [])(e.Data.GetData(DataFormats.FileDrop)))
            {
                if (Directory.Exists(obj))
                { 
                    arr.Add(obj + "  :  "); 
                    arr.AddRange(Directory.GetFiles(obj, "*.*", SearchOption.AllDirectories));
                }
                else
                    arr.Add(obj);
            }
            label1.Text = string.Join("\r\n", arr);
            label2.Text = "Перетащите сюда файл или папку";
            //рамка
            Pen pen = new Pen(Color.WhiteSmoke, 3);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            Graphics g = panel1.CreateGraphics();
            g.DrawRectangle(pen,new Rectangle(1, 1, panel1.Width - 3, panel1.Height - 3));

        }

        void panel1_DragLeave(object sender, EventArgs e)
        {
            arethere = false; //for screamer
            label2.Text = "верни файл, пидор";
            pictureBox1.BackgroundImage = Resources.fnaf_freddy_fazbear;
            BackColor = Color.IndianRed;
            //рамка
            Pen pen = new Pen(Color.Red, 3);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            Graphics g = panel1.CreateGraphics();
            g.DrawRectangle(pen, new Rectangle(1, 1, panel1.Width - 3, panel1.Height - 3));
            screamer();
        }

        void panel1_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Title = "ГДЕ ФАЙЛ, ПРИЗНАВАЙСЯ!!!";
            file.InitialDirectory = @"C:/";
            if (file.ShowDialog() == DialogResult.OK) label1.Text = file.FileName;
        }
        bool arethere = false;
        async void screamer ()
        {
            await Task.Delay(1500);
            if (arethere == false)
            {
                Form form2 = new Form2();
                form2.Show();
                SoundPlayer audio = new SoundPlayer(Resources.fnafsound);
                audio.Play();
                form2.Click += (a, e) => { form2.Close(); audio.Stop(); };
                await Task.Delay(4000); form2.Close();
            }

        }
    }
}
