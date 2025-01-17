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
    public partial class Login : Form
    {
        MySqlConnection conectar = new MySqlConnection(@"Data Source = LAB4-13; Initial Catalog = Login; Integrated Security = True");
        public Login()
        {
            InitializeComponent();
        }
        private void rjButton4_Click(object sender, EventArgs e)
        {
            try
            {
                conectar.Open();

                MySqlCommand verificar = new MySqlCommand("SELECT * FROM usuario WHERE nome = '" + txtUsuario.Text + "' AND senha = '" + txtSenha.Text + "'", conectar);

                bool resultado = verificar.ExecuteReader().HasRows;


                if (resultado == true)
                {
                    Menu_p1 Menu = new Menu_p1();
                    this.Hide();
                    Menu.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuário ou Senha inválidos!");
                    conectar.Close();

                }

            }


            catch
            {
                MessageBox.Show("Não foi possível estabelecer a conexão, verifique o código!");
            }

            if (txtUsuario.Text == "")
            {
                MessageBox.Show("Campo de usuário não está preenchido.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            else if (txtSenha.Text == "")
            {
                MessageBox.Show("Campo da senha não está preenchido.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }


        }


        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            Cadastro_p1 Menu = new Cadastro_p1();
            this.Hide();
            Menu.ShowDialog();
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rjCircularPictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
