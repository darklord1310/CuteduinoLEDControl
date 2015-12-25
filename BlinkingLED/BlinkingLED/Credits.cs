using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlinkingLED
{
    public partial class Credits : Form
    {
        public Credits()
        {
            InitializeComponent();
            richTextBox1.Text = "\n" +
                                "Copyright (C)2015 Wong Yan Yin, <jet_wong@hotmail.com>\n\n" +

                                "This program is free software, you can redistribute it and/or modify\n" +
                                "it under the terms of the GNU General Public License as published by\n" +
                                "the Free Software Foundation, either version 3 of the License, or\n" +
                                "(at your option) any later version." +

                                "This program is distributed in the hope that it will be useful, \n" +
                                "but WITHOUT ANY WARRANTY, without even the implied warranty of\n" +
                                "MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the\n" +
                                "GNU General Public License for more details.\n" +
                                "You should have received a copy of the GNU General Public License\n" +
                                "along with This program.If not, see <http://www.gnu.org/licenses/>.\n\n" +

                                "Source code : https://github.com/darklord1310/CuteduinoLEDControl \n\n\n" +

                                "Description\n" +
                                "===============================================================\n" +
                                "This is a software used to control 3 LEDs connected\n" +
                                "to cuteduino which are connected on pin0, pin1 and\n" +
                                "pin2. This software allows easier interface with the\n" +
                                "microcontroller without exposing coding to user.\n" +
                                "===============================================================\n";
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            LED f1 = new LED();
            f1.ShowDialog();
        }
    }
}
