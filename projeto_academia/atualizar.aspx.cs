using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace projeto_academia
{
    public partial class atualizar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string cpf = txtBuscar.Text;

            string connectionString = @"Data Source=CARLOS;Initial Catalog=academia; Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string selectQuery = "SELECT * FROM DadosCliente WHERE cpf_cliente = @cpf";
                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@cpf", cpf);

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            txtCPF.Text = reader["cpf_cliente"].ToString();
                            txtPeso.Text = reader["peso_cliente"].ToString();
                            txtMassaGorda.Text = reader["massagorda_cliente"].ToString();
                            txtMassaMagra.Text = reader["massamagra_cliente"].ToString();
                            txtTorax.Text = reader["torax_cliente"].ToString();
                            txtCintura.Text = reader["cintura_cliente"].ToString();
                            txtAbdome.Text = reader["abdome_cliente"].ToString();
                            txtQuadril.Text = reader["quadril_cliente"].ToString();
                            txtBracoEsquerdo.Text = reader["bracoe_cliente"].ToString();
                            txtBracoDireito.Text = reader["bracod_cliente"].ToString();
                            txtAntebracoEsquerdo.Text = reader["antebracoe_cliente"].ToString();
                            txtAntebracoDireito.Text = reader["antebracod_cliente"].ToString();
                            txtCoxaEsquerda.Text = reader["coxae_cliente"].ToString();
                            txtCoxaDireita.Text = reader["coxad_cliente"].ToString();
                            txtPanturrilhaEsquerda.Text = reader["panturrilhae_cliente"].ToString();
                            txtPanturrilhaDireita.Text = reader["panturrilhad_cliente"].ToString();
                        }
                        else
                        {
                            lblMensagem.Text = "CPF não encontrado.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMensagem.Text = "Erro: " + ex.Message;
                }
            }
        }

        protected void btbAtualizar_Click(object sender, EventArgs e)
        {
            string cpf = txtCPF.Text;
            string connectionString = @"Data Source=CARLOS;Initial Catalog=academia; Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Verifica se todos os campos estão preenchidos
                    if (CamposPreenchidos())
                    {
                        string peso = txtPeso.Text;
                        string massaGorda = txtMassaGorda.Text;
                        string massaMagra = txtMassaMagra.Text;
                        string torax = txtTorax.Text;
                        string cintura = txtCintura.Text;
                        string abdome = txtAbdome.Text;
                        string quadril = txtQuadril.Text;
                        string bracoE = txtBracoEsquerdo.Text;
                        string bracoD = txtBracoDireito.Text;
                        string antebracoE = txtAntebracoEsquerdo.Text;
                        string antebracoD = txtAntebracoDireito.Text;
                        string coxaE = txtCoxaEsquerda.Text;
                        string coxaD = txtCoxaDireita.Text;
                        string panturrilhaE = txtPanturrilhaEsquerda.Text;
                        string panturrilhaD = txtPanturrilhaDireita.Text;

                        // Validações dos campos omitidos para brevidade...

                        // Consulta para verificar se o registro existe
                        string checkQuery = "SELECT COUNT(*) FROM DadosClienteAtual WHERE cpf_cliente_atual = @cpf";
                        using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                        {
                            checkCommand.Parameters.AddWithValue("@cpf", cpf);
                            int recordExists = (int)checkCommand.ExecuteScalar();

                            if (recordExists > 0)
                            {
                                // Registro existente: Atualiza
                                string updateQuery = "UPDATE DadosClienteAtual SET " +
                                    "peso_cliente_atual = @peso, " +
                                    "massagorda_cliente_atual = @massagorda, " +
                                    "massamagra_cliente_atual = @massamagra, " +
                                    "torax_cliente_atual = @torax, " +
                                    "cintura_cliente_atual = @cintura, " +
                                    "abdome_cliente_atual = @abdome, " +
                                    "quadril_cliente_atual = @quadril, " +
                                    "bracoe_cliente_atual = @bracoe, " +
                                    "bracod_cliente_atual = @bracod, " +
                                    "antebracoe_cliente_atual = @antebracoe, " +
                                    "antebracod_cliente_atual = @antebracod, " +
                                    "coxae_cliente_atual = @coxae, " +
                                    "coxad_cliente_atual = @coxad, " +
                                    "panturrilhae_cliente_atual = @panturrilhae, " +
                                    "panturrilhad_cliente_atual = @panturrilhad " +
                                    "WHERE cpf_cliente_atual = @cpf";

                                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                                {
                                    updateCommand.Parameters.AddWithValue("@cpf", cpf);
                                    updateCommand.Parameters.AddWithValue("@peso", Convert.ToDouble(peso));
                                    updateCommand.Parameters.AddWithValue("@massagorda", Convert.ToDouble(massaGorda));
                                    updateCommand.Parameters.AddWithValue("@massamagra", Convert.ToDouble(massaMagra));
                                    updateCommand.Parameters.AddWithValue("@torax", Convert.ToDouble(torax));
                                    updateCommand.Parameters.AddWithValue("@cintura", Convert.ToDouble(cintura));
                                    updateCommand.Parameters.AddWithValue("@abdome", Convert.ToDouble(abdome));
                                    updateCommand.Parameters.AddWithValue("@quadril", Convert.ToDouble(quadril));
                                    updateCommand.Parameters.AddWithValue("@bracoe", Convert.ToDouble(bracoE));
                                    updateCommand.Parameters.AddWithValue("@bracod", Convert.ToDouble(bracoD));
                                    updateCommand.Parameters.AddWithValue("@antebracoe", Convert.ToDouble(antebracoE));
                                    updateCommand.Parameters.AddWithValue("@antebracod", Convert.ToDouble(antebracoD));
                                    updateCommand.Parameters.AddWithValue("@coxae", Convert.ToDouble(coxaE));
                                    updateCommand.Parameters.AddWithValue("@coxad", Convert.ToDouble(coxaD));
                                    updateCommand.Parameters.AddWithValue("@panturrilhae", Convert.ToDouble(panturrilhaE));
                                    updateCommand.Parameters.AddWithValue("@panturrilhad", Convert.ToDouble(panturrilhaD));

                                    updateCommand.ExecuteNonQuery();
                                    lblMensagem.Text = "Registro atualizado com sucesso.";
                                }
                            }
                            else
                            {
                                // Registro inexistente: Insere novo registro
                                string insertQuery = "INSERT INTO DadosClienteAtual (cpf_cliente_atual, peso_cliente_atual, massagorda_cliente_atual, massamagra_cliente_atual, torax_cliente_atual, cintura_cliente_atual, abdome_cliente_atual, quadril_cliente_atual, bracoe_cliente_atual, bracod_cliente_atual, antebracoe_cliente_atual, antebracod_cliente_atual, coxae_cliente_atual, coxad_cliente_atual, panturrilhae_cliente_atual, panturrilhad_cliente_atual) " +
                                    "VALUES (@cpf, @peso, @massagorda, @massamagra, @torax, @cintura, @abdome, @quadril, @bracoe, @bracod, @antebracoe, @antebracod, @coxae, @coxad, @panturrilhae, @panturrilhad)";

                                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                                {
                                    insertCommand.Parameters.AddWithValue("@cpf", cpf);
                                    insertCommand.Parameters.AddWithValue("@peso", Convert.ToDouble(peso));
                                    insertCommand.Parameters.AddWithValue("@massagorda", Convert.ToDouble(massaGorda));
                                    insertCommand.Parameters.AddWithValue("@massamagra", Convert.ToDouble(massaMagra));
                                    insertCommand.Parameters.AddWithValue("@torax", Convert.ToDouble(torax));
                                    insertCommand.Parameters.AddWithValue("@cintura", Convert.ToDouble(cintura));
                                    insertCommand.Parameters.AddWithValue("@abdome", Convert.ToDouble(abdome));
                                    insertCommand.Parameters.AddWithValue("@quadril", Convert.ToDouble(quadril));
                                    insertCommand.Parameters.AddWithValue("@bracoe", Convert.ToDouble(bracoE));
                                    insertCommand.Parameters.AddWithValue("@bracod", Convert.ToDouble(bracoD));
                                    insertCommand.Parameters.AddWithValue("@antebracoe", Convert.ToDouble(antebracoE));
                                    insertCommand.Parameters.AddWithValue("@antebracod", Convert.ToDouble(antebracoD));
                                    insertCommand.Parameters.AddWithValue("@coxae", Convert.ToDouble(coxaE));
                                    insertCommand.Parameters.AddWithValue("@coxad", Convert.ToDouble(coxaD));
                                    insertCommand.Parameters.AddWithValue("@panturrilhae", Convert.ToDouble(panturrilhaE));
                                    insertCommand.Parameters.AddWithValue("@panturrilhad", Convert.ToDouble(panturrilhaD));

                                    insertCommand.ExecuteNonQuery();
                                    lblMensagem.Text = "Registro inserido com sucesso.";
                                }
                            }

                            limparCampos();
                        }
                    }
                    else
                    {
                        lblMensagem.Text = "Todos os campos devem ser preenchidos.";
                    }
                }
                catch (Exception ex)
                {
                    lblMensagem.Text = "Erro: " + ex.Message;
                }
            }
        }


        private bool IsPositiveNumber(string input)
        {
            double value;
            return double.TryParse(input, out value) && value > 0;
        }

        private bool IsWithinLimit(string input, double maxLimit)
        {
            double value;
            return double.TryParse(input, out value) && value <= maxLimit;
        }

        private bool IsNumeric(string input)
        {
            return long.TryParse(input, out _);
        }

        protected void btbUltimaAtualizacao_Click(object sender, EventArgs e)
        {
            string cpf = txtBuscar.Text;

            string connectionString = @"Data Source=CARLOS;Initial Catalog=academia; Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string selectQuery = "SELECT * FROM DadosClienteAtual WHERE cpf_cliente_atual = @cpf";
                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@cpf", cpf);

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            txtCPF.Text = reader["cpf_cliente_atual"].ToString();
                            txtPeso.Text = reader["peso_cliente_atual"].ToString();
                            txtMassaGorda.Text = reader["massagorda_cliente_atual"].ToString();
                            txtMassaMagra.Text = reader["massamagra_cliente_atual"].ToString();
                            txtTorax.Text = reader["torax_cliente_atual"].ToString();
                            txtCintura.Text = reader["cintura_cliente_atual"].ToString();
                            txtAbdome.Text = reader["abdome_cliente_atual"].ToString();
                            txtQuadril.Text = reader["quadril_cliente_atual"].ToString();
                            txtBracoEsquerdo.Text = reader["bracoe_cliente_atual"].ToString();
                            txtBracoDireito.Text = reader["bracod_cliente_atual"].ToString();
                            txtAntebracoEsquerdo.Text = reader["antebracoe_cliente_atual"].ToString();
                            txtAntebracoDireito.Text = reader["antebracod_cliente_atual"].ToString();
                            txtCoxaEsquerda.Text = reader["coxae_cliente_atual"].ToString();
                            txtCoxaDireita.Text = reader["coxad_cliente_atual"].ToString();
                            txtPanturrilhaEsquerda.Text = reader["panturrilhae_cliente_atual"].ToString();
                            txtPanturrilhaDireita.Text = reader["panturrilhad_cliente_atual"].ToString();
                        }
                        else
                        {
                            lblMensagem.Text = "CPF não encontrado.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMensagem.Text = "Erro: " + ex.Message;
                }
            }
        }

        private bool CamposPreenchidos()
        {
            return
                   !string.IsNullOrEmpty(txtCPF.Text) &&
                   !string.IsNullOrEmpty(txtPeso.Text) &&
                   !string.IsNullOrEmpty(txtMassaGorda.Text) &&
                   !string.IsNullOrEmpty(txtMassaMagra.Text) &&
                   !string.IsNullOrEmpty(txtTorax.Text) &&
                   !string.IsNullOrEmpty(txtCintura.Text) &&
                   !string.IsNullOrEmpty(txtAbdome.Text) &&
                   !string.IsNullOrEmpty(txtQuadril.Text) &&
                   !string.IsNullOrEmpty(txtBracoEsquerdo.Text) &&
                   !string.IsNullOrEmpty(txtBracoDireito.Text) &&
                   !string.IsNullOrEmpty(txtAntebracoEsquerdo.Text) &&
                   !string.IsNullOrEmpty(txtAntebracoDireito.Text) &&
                   !string.IsNullOrEmpty(txtCoxaEsquerda.Text) &&
                   !string.IsNullOrEmpty(txtCoxaDireita.Text) &&
                   !string.IsNullOrEmpty(txtPanturrilhaEsquerda.Text) &&
                   !string.IsNullOrEmpty(txtPanturrilhaDireita.Text);
        }

       
        protected void btbExcluir_Click(object sender, EventArgs e)
        {
            string cpf = txtCPF.Text;

            string connectionString = @"Data Source=CARLOS;Initial Catalog=academia; Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string deleteQuery = "DELETE FROM DadosClienteAtual WHERE cpf_cliente_atual = @cpf";

                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@cpf", cpf);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            lblMensagem.Text = "Registro excluído com sucesso.";
                            limparCampos();
                        }
                        else
                        {
                            lblMensagem.Text = "Nenhum registro foi excluído.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMensagem.Text = "Erro: " + ex.Message;
                }
            }
        }

        private void limparCampos()
        {
            txtCPF.Text = "";
            txtPeso.Text = "";
            txtMassaGorda.Text = "";
            txtMassaMagra.Text = "";
            txtTorax.Text = "";
            txtCintura.Text = "";
            txtAbdome.Text = "";
            txtQuadril.Text = "";
            txtBracoEsquerdo.Text = "";
            txtBracoDireito.Text = "";
            txtAntebracoEsquerdo.Text = "";
            txtAntebracoDireito.Text = "";
            txtCoxaEsquerda.Text = "";
            txtCoxaDireita.Text = "";
            txtPanturrilhaEsquerda.Text = "";
            txtPanturrilhaDireita.Text = "";
        }
        protected void txtNome_TextChanged(object sender, EventArgs e)
        {

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
   
}





