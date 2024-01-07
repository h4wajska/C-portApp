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
        string dataOUT;
        string sendWith;
        string dataIN;


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

            string[] BaudRateItems = { "2400", "4800", "9600" };
            cBoxBaudRate.Items.AddRange(BaudRateItems);

            string[] DataBitsItems = { "6", "7", "8" };
            cBoxDataBits.Items.AddRange(DataBitsItems);

            string[] StopBitsItems = { "One", "Two" };
            cBoxStopBits.Items.AddRange(StopBitsItems);

            string[] ParityBitsItems = { "None", "Odd", "Even" };
            cBoxParityBits.Items.AddRange(ParityBitsItems);


            /*
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
            */

            cbDtrEnable.Checked = false;        //inicjalizowanie odznaczonego CheckBoxa
            serialPort1.DtrEnable = false;
            cbRtsEnable.Checked = false;
            serialPort1.RtsEnable = false;
            btnSendData.Enabled = false;
            cbWriteLine.Checked = false;
            cbWrite.Checked = true;
            sendWith = "Write";

            btnOpen.Enabled = true;            
            btnClose.Enabled = false;

            chBoxAddToOldData.Checked = true;
            chBoxAlwaysUpdate.Checked = false;

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
                btnOpen.Enabled = false;            //zapobieganie przed klikaniem open gdy port jest aktywny
                btnClose.Enabled = true;

                lblStatusCom.Text = "ON";
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnOpen.Enabled = true;
                btnClose.Enabled = false;
                lblStatusCom.Text = "OFF";
            }
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if(serialPort1.IsOpen)
            {
                serialPort1.Close();
                lblStatusCom.Text = "OFF";
                btnOpen.Enabled = true;
                btnClose.Enabled = false;
                progressBar1.Value = 0;
            }

        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            if(serialPort1.IsOpen && serialPort1.BaudRate > 0)
            {
                dataOUT = tBoxDataOut.Text;
                if (sendWith == "WriteLine")
                {
                    serialPort1.WriteLine(dataOUT);
                }
                else if(sendWith == "Write")
                {
                    serialPort1.Write(dataOUT);
                }
                
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
            if(cbUsingEnter.Checked == true)
            {
                tBoxDataOut.Text = tBoxDataOut.Text.Replace(Environment.NewLine, "");   //zmienia funkcję entera z robienia nowej linii, na wysyłanie
            }
        }

        private void cbUsingButton_CheckedChanged(object sender, EventArgs e)
        {
            if(cbUsingButton.Checked == true)
            {
                cbUsingEnter.Checked = false;
                btnSendData.Enabled = true;
            }
            else
            {
                btnSendData.Enabled = false;
            }
        }

        private void tBoxDataOut_KeyDown(object sender, KeyEventArgs e)
        {
            if(cbUsingEnter.Checked == true)
            {
                if(e.KeyCode == Keys.Enter)
                {
                    if (serialPort1.IsOpen && serialPort1.BaudRate > 0)
                    {
                        dataOUT = tBoxDataOut.Text;
                        dataOUT = tBoxDataOut.Text;
                        if (sendWith == "WriteLine")
                        {
                            serialPort1.WriteLine(dataOUT);
                        }
                        else if (sendWith == "Write")
                        {
                            serialPort1.WriteLine(dataOUT);
                        }
                    }
                }
            }
        }

        private void cbUsingEnter_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void cbWriteLine_CheckedChanged(object sender, EventArgs e)
        {
            if(cbWriteLine.Checked == true)
            {
                sendWith = "WriteLine";
                cbWrite.Checked = false;
                cbWriteLine.Checked = true;
            }
            
            
        }

        private void cbWrite_CheckedChanged(object sender, EventArgs e)
        {
            if(cbWrite.Checked == true)
            {
                sendWith = "Write";
                cbWriteLine.Checked = false;
                cbWrite.Checked = true;
            }
        }

        private void cbUsingEnter_CheckedChanged(object sender, EventArgs e)
        {
            if( cbUsingEnter.Checked == true)
            {
                cbUsingButton.Checked = false;
            }
        }

        private void lblStatusCom_Click(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        //----------------------------------------------------------------------------------
        // TE DWIE METODY SĄ ZE SOBĄ POWIĄZANE - odczyt danych
        //----------------------------------------------------------------------------------
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            dataIN = serialPort1.ReadExisting();        //odczyt z serial portu chyba?
            this.Invoke(new EventHandler(ShowData));  //pokazuje dane w textboxie; nie da się pokazać danych bezpośrednio bez użycia tej metody
        }

        private void ShowData(object sender, EventArgs e)
        {
            if(chBoxAlwaysUpdate.Checked == true)
            {
                tBoxDataIN.Text = dataIN.ToString();    //dane zostaną zaktualizowane tylko gdy będzie zaznaczony checkbox
            }
            else if(chBoxAddToOldData.Checked == true)
            {
                tBoxDataIN.Text += dataIN.ToString(); 
            }
        }
        //----------------------------------------------------------------------------------

        private void chBoxAlwaysUpdate_CheckedChanged(object sender, EventArgs e)
        {
            if (chBoxAlwaysUpdate.Checked)
            {
                chBoxAlwaysUpdate.Checked = true;
                chBoxAddToOldData.Checked = false;
            }
        }

        private void chBoxAddToOldData_CheckedChanged(object sender, EventArgs e)
        {
            if (chBoxAddToOldData.Checked)
            {
                chBoxAddToOldData.Checked = true;
                chBoxAlwaysUpdate.Checked = false;
            }
        }
    }
}
