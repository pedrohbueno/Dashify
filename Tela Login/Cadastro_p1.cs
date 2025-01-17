using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Tela_Login
{
    public partial class Cadastro_p1 : Form
    {
        MySqlConnection conectar = new MySqlConnection(@"Server=localhost;Port=3306;Database=DashifyDataBase;Uid=root;Pwd=;");
        MySqlDataAdapter dados;//Mostra os códigos SQL
        MySqlCommandBuilder cmd;//DataTable é quem vai receber os dados do adapter
        DataTable datb;

        int COD_CLIENTE;
        String NOME, ENDERECO, BAIRRO, CEP, TELEFONE, CIDADE, ESTADO, OBS;


        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            Consulta f3 = new Consulta();
            this.Hide();
            f3.ShowDialog();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public Cadastro_p1()
        {
            InitializeComponent();
        }
            public Cadastro_p1(Clientes cli)
        {
            InitializeComponent();
            this.Text = "Cadastro Cleinte";
            COD_CLIENTE = cli.cod_cliente;
            txtNome.Text = cli.nome;
            txtEndereco.Text = cli.endereco;
            txtBairro.Text = cli.bairro;
            mktCep.Text = cli.cep;
            mktTelefone.Text = cli.telefone;
            txtCidade.Text = cli.cidade;
            cbEstado.Text = cli.estado;
            txtObs.Text = cli.obs;
        }
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            String NOME, ENDERECO, BAIRRO, CEP, TELEFONE, CIDADE, ESTADO, OBS;

            NOME = txtNome.Text;
            ENDERECO = txtEndereco.Text;
            BAIRRO = txtBairro.Text;
            CEP = mktCep.Text;
            TELEFONE = mktTelefone.Text;
            CIDADE = txtCidade.Text;
            ESTADO = cbEstado.Text;
            OBS = txtObs.Text;

            try
            {
                //abrindo a conexão
                conectar.Open();

                //objeto de conexao
                MySqlCommand cadastrar = new MySqlCommand();

                if (COD_CLIENTE == 0)
                {
                    cadastrar.CommandText = "INSERT INTO clientes (nome, endereco, bairro, cep, telefone, cidade, estado, obs) VALUES ('" + NOME + "', '" + ENDERECO + "', '" + BAIRRO + "', '" + CEP + "', '" + TELEFONE + "', '" + CIDADE + "', '" + ESTADO + "', '" + OBS + "')";

                }
                else
                {
                    cadastrar.CommandText = "update clientes set nome= '" + txtNome.Text +
                    "', endereco='" + txtEndereco.Text + "', BAIRRO='" + txtBairro.Text + "', CEP='" + mktCep.Text + "', telefone='" + mktTelefone.Text
                    + "', cidade='" + txtCidade.Text + "', estado='" + cbEstado.Text + "', obs='" + txtObs.Text + "' Where COD_CLIENTE=     " + COD_CLIENTE;

                    COD_CLIENTE = 0;

                }
                //Local onde será guardado os dados
                cadastrar.Connection = conectar;

                //Executar Query
                cadastrar.ExecuteNonQuery();

                //Fechar Conexao
                conectar.Close();

                MessageBox.Show("Cadastro efetuado com sucesso");

                txtNome.Clear();
                txtEndereco.Clear();
                txtBairro.Clear();
                mktCep.Clear();
                mktTelefone.Clear();
                txtCidade.Clear();
                cbEstado.Text = "";
                txtObs.Clear();

                txtNome.Focus();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("NÃO FOI POSSIVEL CONECTAR COM O BANCO DE DADOS!!!" + Convert.ToString(ex));
            }
        }
    }
}
