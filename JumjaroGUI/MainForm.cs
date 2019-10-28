using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jumjaro;

namespace JumjaroGUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void translateButton_Click(object sender, EventArgs e)
        {
            var jumjaro = new Jumjaro.Jumjaro();
            outputTextBox.Text = BrailleASCII.FromUnicode(jumjaro.ToJumja(inputTextBox.Text));
        }
    }
}
