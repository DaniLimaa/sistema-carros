﻿using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sitema2
{
    public partial class CadCarros : Form
    {
        public CadCarros()
        {
            InitializeComponent();
        }
        private bool ValidarCpf(string cpf)
        {
            // Remove qualquer caractere não numérico
            cpf = Regex.Replace(cpf, @"[^\d]", "");

            // Verifica se tem 11 dígitos
            if (cpf.Length != 11)
                return false;

            // Verifica se o CPF é uma sequência de números iguais (ex.: 111.111.111-11)
            if (new string(cpf[0], 11) == cpf)
                return false;

            // Calculando o primeiro dígito verificador
            int soma = 0;
            int peso = 10;
            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(cpf[i].ToString()) * peso--;
            }

            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;
            if (digito1 != int.Parse(cpf[9].ToString()))
                return false;

            // Calculando o segundo dígito verificador
            soma = 0;
            peso = 11;
            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(cpf[i].ToString()) * peso--;
            }

            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;
            return digito2 == int.Parse(cpf[10].ToString());
        }
        private void button2_Click(object sender, EventArgs e)
        {
            textBoxProprietario.Text = "";
            maskedTextBoxCPF.Text = "";
            maskedTextBoxTelefone.Text = "";
            textBoxPlaca.Text = "";
            maskedTextBoxAno.Text = "";
            maskedTextBoxChassi.Text = "";
            textBoxCor.Text = "";
            textBoxModelo.Text = "";
            textBoxMarca.Text = "";
            maskedTextBoxValor.Text = "";
            richTextBoxAcessorio.Text = "";
            textBoxProprietario.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string cpf = maskedTextBoxCPF.Text;

            if (ValidarCpf(cpf))
            {
                labelAlert.Text = "CPF VÁLIDO";
                labelAlert.ForeColor = Color.Green;
            }
            else
            {

                labelAlert.Text = "CPF INVÁLIDO";
                labelAlert.ForeColor = Color.Red;
                maskedTextBoxCPF.Text = "";
                maskedTextBoxCPF.Focus();
                return;
            }


            //Defina sua string de conexão com o banco
            string conexaoString = "Server=localhost; Port=3306; Database=bd_sistema; Uid=root; Pwd=;";

            //Defina a inserção de registro no BD

            string query = "INSERT INTO tb_carros (Proprietario, Telefone, Cpf, Marca, Modelo, Placa, Ano, Chassi, Cor, Acessorio, Valor) VALUES (@Proprietario, @Telefone, @Cpf, @Marca, @Modelo, @Placa, @Ano, @Chassi, @Cor, @Acessorio, @Valor)";

            //Crie uma conexão com o BD
        

            using (MySqlConnection conexao = new MySqlConnection(conexaoString))
            {
                try
                {
                    //Abre a conexão
                    conexao.Open();
                    //Crie o comando SQL
                    using (MySqlCommand comando = new MySqlCommand(query, conexao))
                    {
                        //Adicionar os parâmentros com os valores dos TextBox
                        comando.Parameters.AddWithValue("@Proprietario", textBoxProprietario.Text);
                        comando.Parameters.AddWithValue("@Telefone", maskedTextBoxTelefone.Text);
                        comando.Parameters.AddWithValue("@Cpf", maskedTextBoxCPF.Text);
                        comando.Parameters.AddWithValue("@Marca", textBoxMarca.Text);
                        comando.Parameters.AddWithValue("@Modelo", textBoxModelo.Text);
                        comando.Parameters.AddWithValue("@Placa", textBoxPlaca.Text);
                        comando.Parameters.AddWithValue("@Ano", maskedTextBoxAno.Text);
                        comando.Parameters.AddWithValue("@Chassi", maskedTextBoxChassi.Text);
                        comando.Parameters.AddWithValue("@Cor", textBoxCor.Text);
                        comando.Parameters.AddWithValue("@Acessorio", richTextBoxAcessorio.Text);
                        comando.Parameters.AddWithValue("@Valor", maskedTextBoxValor.Text);
                       
                        // Executa o comando de inserção
                        comando.ExecuteNonQuery();

                        MessageBox.Show("Dados inseridos com sucesso!");
                        textBoxProprietario.Text = "";
                        maskedTextBoxTelefone.Text = "";
                        maskedTextBoxCPF.Text = "";
                        textBoxMarca.Text = "";
                        textBoxModelo.Text = "";
                        textBoxPlaca.Text = "";
                        maskedTextBoxAno.Text = "";
                        maskedTextBoxChassi.Text = "";
                        textBoxCor.Text = "";
                        richTextBoxAcessorio.Text = "";
                        maskedTextBoxValor.Text = "";
                        textBoxProprietario.Focus();
                    }
                }
                catch (Exception ex)
                {
                    // em caso de erro, exiba menssagem do erro
                    MessageBox.Show("Erro: " + ex.Message);
                }
            }
        }
    }
}
