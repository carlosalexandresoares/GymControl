using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace projeto_academia
{
    public partial class cadastro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCadastro_Click(object sender, EventArgs e)
        {
            // Obter os valores dos campos
            string cpf = txtCpf.Text;
            string senha = txtSenha.Text;

            // Verificar se o CPF contém apenas números
            if (!System.Text.RegularExpressions.Regex.IsMatch(cpf, "^[0-9]+$"))
            {
                lblmensagem.Text = "O CPF deve conter apenas números.";
                return;
            }

            // Validação do CPF com 11 dígitos
            if (cpf.Length != 11)
            {
                lblmensagem.Text = "O CPF deve conter 11 dígitos numéricos.";
                return;
            }

            // String de conexão com o banco de dados SQL Server
            string connectionString = @"Data Source=CARLOS;Initial Catalog=academia; Integrated Security=True;";

            // Criar a conexão com o banco de dados
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Abrir a conexão
                    connection.Open();

                    // Verificar se o CPF já existe na tabela
                    string checkIfExistsQuery = "SELECT COUNT(*) FROM cadastro WHERE cpf = @cpf";
                    using (SqlCommand checkCommand = new SqlCommand(checkIfExistsQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@cpf", cpf);

                        int count = (int)checkCommand.ExecuteScalar();

                        if (count > 0)
                        {
                            lblmensagem.Text = "Cadastro com este CPF já existe.";
                            return; // Sai da função para evitar a inserção duplicada
                        }
                    }

                    // Se o CPF não existe, podemos prosseguir com a inserção
                    string insertQuery = "INSERT INTO cadastro (cpf, senha) VALUES (@cpf, @senha)";
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@cpf", cpf);
                        command.Parameters.AddWithValue("@senha", senha);

                        int rowsAffected = command.ExecuteNonQuery();

                      

                        if (rowsAffected > 0)
                        {
                            lblmensagem.Text = "Cadastro realizado com sucesso!";
                        }
                        else
                        {
                            lblmensagem.Text = "Erro ao cadastrar. Por favor, tente novamente.";
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