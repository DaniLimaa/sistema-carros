﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sitema2
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CadClientes form = new CadClientes();
            form.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            CadCarros form = new CadCarros();
            form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Gerenciamento_Clientes form = new Gerenciamento_Clientes();
            form.ShowDialog();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Gerenciamento_Carros form = new Gerenciamento_Carros();
            form.ShowDialog();
        }
    }
}
