using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Tela_Login
{
    public partial class Consulta : Form
    {
        MySqlConnection conectar = new MySqlConnection(@"Server=localhost;Port=3306;Database=Login;Uid=root;Pwd=;");
        MySqlDataAdapter dados;
        MySqlCommandBuilder cmd;
        DataTable datb;

        Clientes cli = new Clientes(); //Objeto classe usuario
        Cadastro_p1 inicio = new Cadastro_p1();
        public Consulta()
        {
            InitializeComponent();
        }

        private void Consulta_Load(object sender, EventArgs e)
        {
            //SqlConnection conectar = new SqlConnection(@"Data Source = LAB3-7; Initial Catalog = Cadastro; Integrated Security = True");
            dados = new MySqlDataAdapter("select * from usuario", conectar);//classe que armazena dados

            dtgv.DataSource = datb;

            dtgv.Refresh();
        }

        private void dtgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cli.cod_cliente = Convert.ToInt32(dtgv[0, dtgv.CurrentRow.Index].Value.ToString());
            cli.nome = dtgv[1, dtgv.CurrentRow.Index].Value.ToString();
            cli.endereco = dtgv[2, dtgv.CurrentRow.Index].Value.ToString();
            cli.bairro = dtgv[3, dtgv.CurrentRow.Index].Value.ToString();
            cli.cep = dtgv[3, dtgv.CurrentRow.Index].Value.ToString();
            cli.telefone = dtgv[3, dtgv.CurrentRow.Index].Value.ToString();
            cli.cidade = dtgv[3, dtgv.CurrentRow.Index].Value.ToString();
            cli.estado = dtgv[3, dtgv.CurrentRow.Index].Value.ToString();
            cli.obs = dtgv[4, dtgv.CurrentRow.Index].Value.ToString();

            Cadastro_p1 editar_dados = new Cadastro_p1(cli);
            editar_dados.ShowDialog();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            MySqlConnection conectar = new MySqlConnection(@"Data Source = LAB4-13; Initial Catalog = Cadastro; Integrated Security = True");
            dados = new MySqlDataAdapter("select * from usuario where nome LIKE'%" + txtPesq.Text + "%'", conectar);//classe que armazena dados
            datb = new DataTable();
            dados.Fill(datb);
            dtgv.DataSource = datb;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if(dtgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Nada selecionado para edição");
                return;
            }
            else
            {
                cli.cod_cliente= Convert.ToInt32(dtgv[0, dtgv.CurrentRow.Index].Value.ToString());
                cli.nome = dtgv[1, dtgv.CurrentRow.Index].Value.ToString();
                cli.endereco = dtgv[2, dtgv.CurrentRow.Index].Value.ToString();
                cli.bairro = dtgv[3, dtgv.CurrentRow.Index].Value.ToString();
                cli.cep = dtgv[4, dtgv.CurrentRow.Index].Value.ToString();
                cli.telefone = dtgv[5, dtgv.CurrentRow.Index].Value.ToString();
                cli.cidade = dtgv[6, dtgv.CurrentRow.Index].Value.ToString();
                cli.estado = dtgv[7, dtgv.CurrentRow.Index].Value.ToString();
                cli.obs = dtgv[8, dtgv.CurrentRow.Index].Value.ToString();

                this.Hide();
                Cadastro_p1 editar_dados = new Cadastro_p1(cli);
                editar_dados.ShowDialog();
                this.Close();
                

            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (dtgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Nada selecionado para exclusão");
                return;
            }
            else
            {
                DialogResult resp = MessageBox.Show("Tem certeza que deseja excluir o usuario?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (resp == DialogResult.Yes)
                {
                    DataGridViewRow row = dtgv.SelectedRows[0];
                    dtgv.Rows.RemoveAt(row.Index);
                    cmd = new MySqlCommandBuilder(dados);
                    dados.Update(datb);
                }
            }
        }
    }
}
