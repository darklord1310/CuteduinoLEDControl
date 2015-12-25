using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using DigiSparkDotNet;          //this header must be added

namespace BlinkingLED
{
    public delegate void otherdelegate();

    public partial class Form2 : Form
    {
        ArduinoUsbDevice digispark = new ArduinoUsbDevice();
        byte[] value;
        LED[] led = new LED[3];

        public Form2()
        {
            InitializeComponent();
            led[0] = new LED(0, 0, 0);
            led[1] = new LED(1, 0, 0);
            led[2] = new LED(2, 0, 0);
        }


        public class LED
        {
            private int ledIndicator;
            private int ledState;
            private int ledTogglingFreq;

            public LED(int led, int state, int togglingFreq)
            {
                ledIndicator = led;
                ledState = state;
                ledTogglingFreq = togglingFreq;
            }

            public void setLED(int led)
            {
                ledIndicator = led;
            }

            public void setFrequency(int freq)
            {
                ledTogglingFreq = freq;
            }

            public void setState(int state)
            {
                ledState = state;
            }

            public int getFrequency()
            {
                return ledTogglingFreq;
            }

            public int getState()
            {
                return ledState;
            }

            public int getLED()
            {
                return ledIndicator;
            }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        public void toggleRedLED()
        {
            pictureBox1.Image = Image.FromFile("C:/Users/Jet/Desktop/GUIAssignment/BlinkingLED/BlinkingLED/led.png");
            Thread.Sleep(led[0].getFrequency());
            pictureBox1.Image = Image.FromFile("C:/Users/Jet/Desktop/GUIAssignment/BlinkingLED/BlinkingLED/red.png");
            Thread.Sleep(led[0].getFrequency());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //green
            if(radioButton1.Checked)
            {
                pictureBox1.Image = Image.FromFile("C:/Users/Jet/Desktop/GUIAssignment/BlinkingLED/BlinkingLED/green.png");
                digispark.WriteByte(7);
            }
            else if(radioButton2.Checked)
            {
                pictureBox1.Image = Image.FromFile("C:/Users/Jet/Desktop/GUIAssignment/BlinkingLED/BlinkingLED/led.png");
                digispark.WriteByte(8);
            }
            else if (radioButton3.Checked)
            {
                digispark.WriteByte(6);
            }

            //red
            if(radioButton6.Checked)
            {
                pictureBox2.Image = Image.FromFile("C:/Users/Jet/Desktop/GUIAssignment/BlinkingLED/BlinkingLED/red.png");
                digispark.WriteByte(1);
            }
            else if (radioButton5.Checked)
            {
                pictureBox2.Image = Image.FromFile("C:/Users/Jet/Desktop/GUIAssignment/BlinkingLED/BlinkingLED/led.png");
                digispark.WriteByte(2);
            }
            else if (radioButton4.Checked)
            {
                digispark.WriteByte(0);
            }

            //yellow
            if (radioButton9.Checked)
            {
                pictureBox3.Image = Image.FromFile("C:/Users/Jet/Desktop/GUIAssignment/BlinkingLED/BlinkingLED/yellow.gif");
                digispark.WriteByte(4);
            }
            else if (radioButton8.Checked)
            {
                pictureBox3.Image = Image.FromFile("C:/Users/Jet/Desktop/GUIAssignment/BlinkingLED/BlinkingLED/led.png");
                digispark.WriteByte(5);
            }
            else if (radioButton7.Checked)
            {
                digispark.WriteByte(3);
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton3.Checked)
            {
                textBox3.Enabled = true;
                button2.Enabled = true;
            }
            else
            {
                textBox3.Enabled = false;
                button2.Enabled = false;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //set green freq
            digispark.WriteByte(11);        //to tell cuteduino to set green freq
            try
            {
                if (Convert.ToInt32(textBox3.Text) > 255)
                    throw new MyException("LargerThan255");
                else
                {
                    digispark.WriteByte(Convert.ToByte(textBox3.Text));
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (MyException ex)
            {

            }
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton4.Checked)
            {
                textBox4.Enabled = true;
                button3.Enabled = true;
            }
            else
            {
                textBox4.Enabled = false;
                button3.Enabled = false;
            }
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton7.Checked)
            {
                textBox5.Enabled = true;
                button4.Enabled = true;
            }
            else
            {
                textBox5.Enabled = false;
                button4.Enabled = false;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString().Equals("Blink All"))
            {
                button6.Enabled = true;
                textBox1.Enabled = true;
            }
            else
            {
                textBox1.Enabled = false;
                button6.Enabled = false;
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedIndex.ToString().Equals(""))
                    throw new MyException("EmptyPreset");
                else
                {
                    if (comboBox1.SelectedItem.ToString().Equals("Light Up All"))
                    {
                        pictureBox1.Image = Image.FromFile("C:/Users/Jet/Desktop/GUIAssignment/BlinkingLED/BlinkingLED/green.png");
                        pictureBox2.Image = Image.FromFile("C:/Users/Jet/Desktop/GUIAssignment/BlinkingLED/BlinkingLED/red.png");
                        pictureBox3.Image = Image.FromFile("C:/Users/Jet/Desktop/GUIAssignment/BlinkingLED/BlinkingLED/yellow.gif");
                        digispark.WriteByte(12);
                    }
                        
                    else if (comboBox1.SelectedItem.ToString().Equals("Light Off All"))
                    {
                        pictureBox1.Image = Image.FromFile("C:/Users/Jet/Desktop/GUIAssignment/BlinkingLED/BlinkingLED/led.png");
                        pictureBox2.Image = Image.FromFile("C:/Users/Jet/Desktop/GUIAssignment/BlinkingLED/BlinkingLED/led.png");
                        pictureBox3.Image = Image.FromFile("C:/Users/Jet/Desktop/GUIAssignment/BlinkingLED/BlinkingLED/led.png");
                        digispark.WriteByte(13);
                    }
                        
                    else if (comboBox1.SelectedItem.ToString().Equals("Running Light"))
                        digispark.WriteByte(14);
                    else if(comboBox1.SelectedItem.ToString().Equals("Blink All"))
                        digispark.WriteByte(16);
                }
            }
            catch(MyException ex)
            {

            }

        }

        public class MyException : Exception
        {
            public MyException(string s)
            {
                if (s == "EmptyPreset")
                    MessageBox.Show("Please select any one of the preset!");
                else if(s == "LargerThan255")
                    MessageBox.Show("Please enter value smaller than 255");
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_MouseHover(object sender, EventArgs e)
        {
            ToolTip TP = new ToolTip();
            TP.ShowAlways = true;
            TP.SetToolTip(textBox1, "The blink frequency entered will be multiply with 4 and it must be smaller than 256");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            digispark.WriteByte(15);        //to tell cuteduino to set preset freq
            try
            {
                if(Convert.ToInt32(textBox1.Text) > 255)
                    throw new MyException("LargerThan255");
                else
                {
                  digispark.WriteByte(Convert.ToByte(textBox1.Text));
                }               
            }
            catch(FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(MyException ex)
            {

            }
            
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void textBox3_MouseHover(object sender, EventArgs e)
        {
            ToolTip TP = new ToolTip();
            TP.ShowAlways = true;
            TP.SetToolTip(textBox3, "The blink frequency entered will be multiply with 4 and it must be smaller than 256");
        }

        private void textBox4_MouseHover(object sender, EventArgs e)
        {
            ToolTip TP = new ToolTip();
            TP.ShowAlways = true;
            TP.SetToolTip(textBox4, "The blink frequency entered will be multiply with 4 and it must be smaller than 256");
        }

        private void textBox5_MouseHover(object sender, EventArgs e)
        {
            ToolTip TP = new ToolTip();
            TP.ShowAlways = true;
            TP.SetToolTip(textBox5, "The blink frequency entered will be multiply with 4 and it must be smaller than 256");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //set red freq
            digispark.WriteByte(9);        //to tell cuteduino to set red freq
            try
            {
                if (Convert.ToInt32(textBox4.Text) > 255)
                    throw new MyException("LargerThan255");
                else
                {
                    digispark.WriteByte(Convert.ToByte(textBox4.Text));
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (MyException ex)
            {

            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //set yellow freq
            digispark.WriteByte(10);        //to tell cuteduino to set yellow freq
            try
            {
                if (Convert.ToInt32(textBox5.Text) > 255)
                    throw new MyException("LargerThan255");
                else
                {
                    digispark.WriteByte(Convert.ToByte(textBox5.Text));
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (MyException ex)
            {

            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
