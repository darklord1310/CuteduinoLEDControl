using System;
using System.Text;
using System.Windows.Forms;
using DigiSparkDotNet;          //this header must be added
using System.Drawing;

namespace BlinkingLED
{
    public partial class LED : Form
    {
        ArduinoUsbDevice digispark = new ArduinoUsbDevice();
        byte[] value;

        public LED()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var data = (byte)1;         //send 2 to turn on LED
            digispark.WriteByte(data);
            if (digispark.ReadByte(out value))
            {
                Console.WriteLine(Encoding.Default.GetString(value));
            }
            //textBox1.Text = getString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var data = (byte)2;         //send 3 to turn off LED
            digispark.WriteByte(data);
            //textBox1.Text = getString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private string getString()
        {
            string s = new string(' ', 5000);
            int i = 0;
            while(true)
            {
                if (digispark.ReadByte(out value))
                {
                    if (Encoding.Default.GetString(value).Equals('\n'))
                        s = s.Insert(i, Encoding.Default.GetString(value));
                    else
                        break;
                    i++;
                }
            }

            return s;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void LED_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Credits f3 = new Credits();
            f3.ShowDialog();
        }
    }
}
