using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace projeto_academia
{
    public partial class Dados : System.Web.UI.Page




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
                            txtNome.Text = reader["nome_cliente"].ToString();
                            txtIdade.Text = reader["idade_cliente"].ToString();
                            txtTelefone.Text = reader["telefone_cliente"].ToString();
                            txtDataNascimento.Text = Convert.ToDateTime(reader["nascimento_cliente"]).ToString("dd/MM/yyyy");
                            txtCPF.Text = reader["cpf_cliente"].ToString();
                            txtCEP.Text = reader["cep_cliente"].ToString();
                            txtDataInicio.Text = Convert.ToDateTime(reader["datainicio_cliente"]).ToString("dd/MM/yyyy");
                            txtPeso.Text = reader["peso_cliente"].ToString();
                            txtAltura.Text = reader["altura_cliente"].ToString();
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

        protected void btbEditar_Click(object sender, EventArgs e)
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
                        string nome = txtNome.Text;
                        string nascimento = txtDataNascimento.Text;

                        string telefone = txtTelefone.Text;
                        string cep = txtCEP.Text;

                        string peso = txtPeso.Text;
                        string altura = txtAltura.Text;
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




                        // Verificar se todos os campos numéricos são válidos e não negativos
                        if (!IsPositiveNumber(peso) || !IsPositiveNumber(altura) ||
                            !IsPositiveNumber(massaGorda) || !IsPositiveNumber(torax) || !IsPositiveNumber(cintura) ||
                            !IsPositiveNumber(abdome) || !IsPositiveNumber(quadril) || !IsPositiveNumber(bracoE) ||
                            !IsPositiveNumber(bracoD) || !IsPositiveNumber(antebracoE) || !IsPositiveNumber(antebracoD) ||
                            !IsPositiveNumber(coxaE) || !IsPositiveNumber(coxaD) || !IsPositiveNumber(panturrilhaE) ||
                            !IsPositiveNumber(panturrilhaD))
                        {
                            lblMensagem.Text = "Campos numéricos devem ser números positivos.";
                            return;
                        }

                        // Validação do CPF com 11 dígitos
                       // if (cpf.Length != 11 || !IsNumeric(cpf))
                       // {
                        //    lblMensagem.Text = "O CPF deve conter 11 dígitos numéricos.";
                      //      return;
                      //  }



                        // Validação do telefone com 10 ou 11 dígitos
                        if ((telefone.Length != 10 && telefone.Length != 11) || !IsNumeric(telefone))
                        {
                            lblMensagem.Text = "O telefone deve conter 10 ou 11 dígitos.";
                            return;
                        }

                        // Validação do peso máximo (250 kg)
                        double pesoDouble;
                        if (!double.TryParse(peso, out pesoDouble) || pesoDouble > 250)
                        {
                            lblMensagem.Text = "O peso não pode ultrapassar 250 kg.";
                            return;
                        }

                        // Normaliza o separador decimal para um ponto, caso o usuário insira uma vírgula
                        altura = altura.Replace(",", ".");

                        // Validação da altura (mínima de 1.0 m e máxima de 2.5 m)
                        double alturaDouble;
                        if (!double.TryParse(altura, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out alturaDouble) || alturaDouble < 1.0 || alturaDouble > 2.5)
                        {
                            lblMensagem.Text = "A altura deve ser entre 1.0 m e 2.5 m.";
                            return;
                        }

                        // Validação da massa gorda e massa magra
                        double massaGordaDouble, massaMagraDouble;
                        if (double.TryParse(massaGorda, out massaGordaDouble) && double.TryParse(massaMagra, out massaMagraDouble) &&
                            (massaGordaDouble + massaMagraDouble > pesoDouble))
                        {
                            lblMensagem.Text = "A soma da massa gorda e massa magra não pode exceder o peso.";
                            return;
                        }

                        // Validação de medidas com valores máximos realistas
                        if (!IsWithinLimit(torax, 150) || !IsWithinLimit(cintura, 120) ||
                            !IsWithinLimit(abdome, 120) || !IsWithinLimit(quadril, 150) ||
                            !IsWithinLimit(bracoE, 60) || !IsWithinLimit(bracoD, 60) ||
                            !IsWithinLimit(antebracoE, 50) || !IsWithinLimit(antebracoD, 50) ||
                            !IsWithinLimit(coxaE, 90) || !IsWithinLimit(coxaD, 90) ||
                            !IsWithinLimit(panturrilhaE, 60) || !IsWithinLimit(panturrilhaD, 60))
                        {
                            lblMensagem.Text = "Verifique as medidas. Valores excedem o limite permitido.";
                            return;
                        }

                        // Verifica se a idade está dentro do intervalo correto
                        if (int.TryParse(txtIdade.Text, out int idade) && idade >= 6 && idade <= 99)
                        {
                            // Verifica se os campos numéricos contêm apenas números
                            if (IsNumeric(txtIdade.Text) && IsNumeric(txtTelefone.Text) && IsNumeric(cpf) &&
                                IsNumeric(txtCEP.Text) && IsNumeric(txtPeso.Text) && IsNumeric(txtAltura.Text) &&
                                IsNumeric(txtMassaGorda.Text) && IsNumeric(txtMassaMagra.Text) &&
                                IsNumeric(txtTorax.Text) && IsNumeric(txtCintura.Text) &&
                                IsNumeric(txtAbdome.Text) && IsNumeric(txtQuadril.Text) &&
                                IsNumeric(txtBracoEsquerdo.Text) && IsNumeric(txtBracoDireito.Text) &&
                                IsNumeric(txtAntebracoEsquerdo.Text) && IsNumeric(txtAntebracoDireito.Text) &&
                                IsNumeric(txtCoxaEsquerda.Text) && IsNumeric(txtCoxaDireita.Text) &&
                                IsNumeric(txtPanturrilhaEsquerda.Text) && IsNumeric(txtPanturrilhaDireita.Text))
                            {
                                // Verifica se a data de nascimento é compatível com a idade
                                if (DateTime.TryParse(txtDataNascimento.Text, out DateTime dataNascimento))
                                {
                                    DateTime dataInicio = Convert.ToDateTime(txtDataInicio.Text);
                                    if (dataInicio.Year - dataNascimento.Year == idade)
                                    {
                                        // Verifica se a data de início é no ano atual
                                        if (dataInicio.Year == DateTime.Now.Year)
                                        {
                                            string updateQuery = "UPDATE DadosCliente SET " +
                                                   "nome_cliente = @nome, " +
                                                    "idade_cliente = @idade, " +
                                                    "telefone_cliente = @telefone, " +
                                                    "nascimento_cliente = @nascimento, " +
                                                    "cep_cliente = @cep, " +
                                                    "datainicio_cliente = @datainicio, " +
                                                    "peso_cliente = @peso, " +
                                                    "altura_cliente = @altura, " +
                                                    "massagorda_cliente = @massagorda, " +
                                                    "massamagra_cliente = @massamagra, " +
                                                    "torax_cliente = @torax, " +
                                                    "cintura_cliente = @cintura, " +
                                                    "abdome_cliente = @abdome, " +
                                                    "quadril_cliente = @quadril, " +
                                                    "bracoe_cliente = @bracoe, " +
                                                    "bracod_cliente = @bracod, " +
                                                    "antebracoe_cliente = @antebracoe, " +
                                                    "antebracod_cliente = @antebracod, " +
                                                    "coxae_cliente = @coxae, " +
                                                    "coxad_cliente = @coxad, " +
                                                    "panturrilhae_cliente = @panturrilhae, " +
                                                    "panturrilhad_cliente = @panturrilhad " +
                                                    "WHERE cpf_cliente = @cpf";

                                            using (SqlCommand command = new SqlCommand(updateQuery, connection))
                                            {
                                                command.Parameters.AddWithValue("@nome", txtNome.Text);
                                                command.Parameters.AddWithValue("@idade", Convert.ToInt32(txtIdade.Text));
                                                command.Parameters.AddWithValue("@telefone", txtTelefone.Text);
                                                command.Parameters.AddWithValue("@nascimento", Convert.ToDateTime(txtDataNascimento.Text));
                                                command.Parameters.AddWithValue("@cep", txtCEP.Text);
                                                command.Parameters.AddWithValue("@datainicio", Convert.ToDateTime(txtDataInicio.Text));
                                                command.Parameters.AddWithValue("@peso", Convert.ToDouble(txtPeso.Text));
                                                command.Parameters.AddWithValue("@altura", Convert.ToDouble(txtAltura.Text));
                                                command.Parameters.AddWithValue("@massagorda", Convert.ToDouble(txtMassaGorda.Text));
                                                command.Parameters.AddWithValue("@massamagra", Convert.ToDouble(txtMassaMagra.Text));
                                                command.Parameters.AddWithValue("@torax", Convert.ToDouble(txtTorax.Text));
                                                command.Parameters.AddWithValue("@cintura", Convert.ToDouble(txtCintura.Text));
                                                command.Parameters.AddWithValue("@abdome", Convert.ToDouble(txtAbdome.Text));
                                                command.Parameters.AddWithValue("@quadril", Convert.ToDouble(txtQuadril.Text));
                                                command.Parameters.AddWithValue("@bracoe", Convert.ToDouble(txtBracoEsquerdo.Text));
                                                command.Parameters.AddWithValue("@bracod", Convert.ToDouble(txtBracoDireito.Text));
                                                command.Parameters.AddWithValue("@antebracoe", Convert.ToDouble(txtAntebracoEsquerdo.Text));
                                                command.Parameters.AddWithValue("@antebracod", Convert.ToDouble(txtAntebracoDireito.Text));
                                                command.Parameters.AddWithValue("@coxae", Convert.ToDouble(txtCoxaEsquerda.Text));
                                                command.Parameters.AddWithValue("@coxad", Convert.ToDouble(txtCoxaDireita.Text));
                                                command.Parameters.AddWithValue("@panturrilhae", Convert.ToDouble(txtPanturrilhaEsquerda.Text));
                                                command.Parameters.AddWithValue("@panturrilhad", Convert.ToDouble(txtPanturrilhaDireita.Text));
                                                command.Parameters.AddWithValue("@cpf", cpf);

                                                int rowsAffected = command.ExecuteNonQuery();

                                                if (rowsAffected > 0)
                                                {
                                                    lblMensagem.Text = "Registro atualizado com sucesso.";
                                                    limparCampos();
                                                }
                                                else
                                                {
                                                    lblMensagem.Text = "Nenhum registro foi atualizado.";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            lblMensagem.Text = "A data de início deve ser no ano atual.";
                                        }
                                    }
                                    else
                                    {
                                        lblMensagem.Text = "A data de nascimento não é compatível com a idade.";
                                    }
                                }
                                else
                                {
                                    lblMensagem.Text = "Formato de data inválido.";
                                }
                            }
                            else
                            {
                                lblMensagem.Text = "Os campos de valores numéricos devem conter apenas números.";
                            }
                        }
                        else
                        {
                            lblMensagem.Text = "A idade deve estar entre 6 e 99 anos.";
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

       


        private bool CamposPreenchidos()
        {
            // Verifica se todos os campos estão preenchidos
            return !string.IsNullOrEmpty(txtNome.Text) &&
                   !string.IsNullOrEmpty(txtIdade.Text) &&
                   !string.IsNullOrEmpty(txtTelefone.Text) &&
                   !string.IsNullOrEmpty(txtDataNascimento.Text) &&
                   !string.IsNullOrEmpty(txtCPF.Text) &&
                   !string.IsNullOrEmpty(txtCEP.Text) &&
                   !string.IsNullOrEmpty(txtDataInicio.Text) &&
                   !string.IsNullOrEmpty(txtPeso.Text) &&
                   !string.IsNullOrEmpty(txtAltura.Text) &&
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
        private bool IsNumeric(string input)
        {
            return double.TryParse(input, out _);
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

                    string deleteQuery = "DELETE FROM DadosCliente WHERE cpf_cliente = @cpf";

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
            txtNome.Text = "";
            txtIdade.Text = "";
            txtTelefone.Text = "";
            txtDataNascimento.Text = "";
            txtCPF.Text = "";
            txtCEP.Text = "";
            txtDataInicio.Text = "";
            txtPeso.Text = "";
            txtAltura.Text = "";
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