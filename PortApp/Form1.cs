﻿using System;
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
        string dataOUT;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            cBoxCOMPORT.Items.AddRange(ports);

            cBoxBaudRate.Text = "9600";
            cBoxDataBits.Text = "8";
            cBoxStopBits.Text = "One";
            cBoxParityBits.Text = "None";

            cBoxBaudRate.Items.Add("2400"); //Dodawanie różnych wartości
            cBoxBaudRate.Items.Add("4800");
            cBoxBaudRate.Items.Add("9600");

            cBoxDataBits.Items.Add("6");
            cBoxDataBits.Items.Add("7");
            cBoxDataBits.Items.Add("8");

            cBoxStopBits.Items.Add("One");
            cBoxStopBits.Items.Add("Two");

            cBoxParityBits.Items.Add("None");
            cBoxParityBits.Items.Add("Odd");
            cBoxParityBits.Items.Add("Even");

            cbDtrEnable.Checked = false;        //inicjalizowanie odznaczonego CheckBoxa
            serialPort1.DtrEnable = false;
            cbRtsEnable.Checked = false;
            serialPort1.RtsEnable = false;

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = cBoxCOMPORT.Text;
                serialPort1.BaudRate = Convert.ToInt32(cBoxBaudRate.Text);
                serialPort1.DataBits = Convert.ToInt32(cBoxDataBits.Text);
                serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cBoxStopBits.Text);
                serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), cBoxParityBits.Text);

                serialPort1.Open();
                progressBar1.Value = 100;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if(serialPort1.IsOpen)
            {
                serialPort1.Close();
                progressBar1.Value = 0;
            }
        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            if(serialPort1.IsOpen && serialPort1.BaudRate > 0)
            {
                dataOUT = tBoxDataOut.Text;
                serialPort1.WriteLine(dataOUT);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            cBoxCOMPORT.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            cBoxCOMPORT.Items.AddRange(ports);
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void cbDtrEnable_CheckedChanged(object sender, EventArgs e)
        {
            // ta metoda jest wywoływana przy zmianie statusu checkboxa
            if(cbDtrEnable.Checked == true)
            {
                serialPort1.DtrEnable = true;
            }
            else
            {
                serialPort1.DtrEnable = false;
            }
        }

        private void cbRtsEnable_CheckedChanged(object sender, EventArgs e)
        {
            if(cbRtsEnable.Checked == true)
            {
                serialPort1.RtsEnable = true;
            }
            else
            {
                serialPort1.RtsEnable = false;
            }
        }

        private void btnClearDataOut_Click(object sender, EventArgs e)
        {
            if(tBoxDataOut.Text != "")
            {
                tBoxDataOut.Text = "";
            }
        }

        private void tBoxDataOut_TextChanged(object sender, EventArgs e)
        {
            int dataOUTLength = tBoxDataOut.TextLength;
            lblDataOutLength.Text = dataOUTLength.ToString();
        }
    }
}
