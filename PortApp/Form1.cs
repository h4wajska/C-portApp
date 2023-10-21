using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace PortApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            cBoxCOMPORT.Items.AddRange(ports);

            cBoxBaudRate.Items.Add("2400");
            cBoxBaudRate.Items.Add("4800");
            cBoxBaudRate.Items.Add("9600");

            cBoxDataBits.Items.Add("6");
            cBoxDataBits.Items.Add("7");
            cBoxDataBits.Items.Add("8");

            cBoxStopDataBits.Items.Add("One");
            cBoxStopDataBits.Items.Add("Two");

            cBoxParityBits.Items.Add("None");
            cBoxParityBits.Items.Add("Odd");
            cBoxParityBits.Items.Add("Even");

        }
    }
}
