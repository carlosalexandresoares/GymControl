using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace projeto_academia
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string cpf = txtCpf.Text;
            string senha = txtSenha.Text;

            // String de conexão com o banco de dados SQL Server
            string connectionString = @"Data Source=CARLOS;Initial Catalog=academia; Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Verificar se o CPF e a senha correspondem a um registro na tabela
                    string query = "SELECT COUNT(*) FROM cadastro WHERE cpf = @cpf AND senha = @senha";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@cpf", cpf);
                        command.Parameters.AddWithValue("@senha", senha);

                        int count = (int)command.ExecuteScalar();

                        if (count > 0)
                        {
                            lblmensagem.Text = "Login bem-sucedido!";
                            Response.Redirect("Formulario.aspx"); // Redirecionar para a página do formulário
                        }
                        else
                        {
                            lblmensagem.Text = "CPF ou senha incorretos.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblmensagem.Text = "Erro: " + ex.Message;
                }
            }
        }
    }
}