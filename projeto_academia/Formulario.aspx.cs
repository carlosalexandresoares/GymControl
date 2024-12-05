using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace projeto_academia
{
    public partial class Formulario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string nome = txtNome.Text;
            string nascimento = txtDataNascimento.Text;
            string cpf = txtCPF.Text;
            string telefone = txtTelefone.Text;
            string cep = txtCEP.Text;
            string dataInicio = txtDataInicio.Text;
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
            string idade = txtIdade.Text;

            // Verificar se algum campo está em branco
            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(idade) || string.IsNullOrEmpty(nascimento) || string.IsNullOrEmpty(cpf) ||
                string.IsNullOrEmpty(telefone) || string.IsNullOrEmpty(cep) || string.IsNullOrEmpty(dataInicio) ||
                string.IsNullOrEmpty(peso) || string.IsNullOrEmpty(altura) || string.IsNullOrEmpty(massaGorda) ||
                string.IsNullOrEmpty(torax) || string.IsNullOrEmpty(cintura) || string.IsNullOrEmpty(abdome) ||
                string.IsNullOrEmpty(quadril) || string.IsNullOrEmpty(bracoE) || string.IsNullOrEmpty(bracoD) ||
                string.IsNullOrEmpty(antebracoE) || string.IsNullOrEmpty(antebracoD) || string.IsNullOrEmpty(coxaE) ||
                string.IsNullOrEmpty(coxaD) || string.IsNullOrEmpty(panturrilhaE) || string.IsNullOrEmpty(panturrilhaD))
            {
                lblMensagem.Text = "Por favor, preencha todos os campos.";
                return;
            }

            // Verificar se todos os campos numéricos são válidos e não negativos
            if (!IsPositiveNumber(idade) || !IsPositiveNumber(peso) || !IsPositiveNumber(altura) ||
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
            if (cpf.Length != 11 || !IsNumeric(cpf))
            {
                lblMensagem.Text = "O CPF deve conter 11 dígitos numéricos.";
                return;
            }

            DateTime dataNascimento;
            if (!DateTime.TryParse(nascimento, out dataNascimento) || dataNascimento.Year < 1922)
            {
                lblMensagem.Text = "A data de nascimento deve ser a partir de 1922.";
                return;
            }

            DateTime dataInicioAcademia;
            if (!DateTime.TryParse(dataInicio, out dataInicioAcademia) || dataInicioAcademia.Year != DateTime.Now.Year)
            {
                lblMensagem.Text = "A data de início na academia deve ser no ano atual.";
                return;
            }

            int anoAtual = DateTime.Now.Year;
            int idadeFornecida = int.Parse(idade);
            int anoNascimento = dataNascimento.Year;
            int idadeCalculada = anoAtual - anoNascimento;

            if (idadeCalculada != idadeFornecida)
            {
                lblMensagem.Text = "A idade fornecida não corresponde ao ano de nascimento.";
                return;
            }

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

            // Código de inserção no banco de dados...
            string connectionString = @"Data Source=CARLOS;Initial Catalog=academia; Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string checkIfExistsQuery = "SELECT COUNT(*) FROM DadosCliente WHERE cpf_cliente = @cpf";
                    using (SqlCommand checkCommand = new SqlCommand(checkIfExistsQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@cpf", cpf);
                        int count = (int)checkCommand.ExecuteScalar();

                        if (count > 0)
                        {
                            lblMensagem.Text = "Cadastro com este CPF já existe.";
                            return;
                        }
                    }

                    string insertQuery = "INSERT INTO DadosCliente (nome_cliente, nascimento_cliente, cpf_cliente, telefone_cliente, cep_cliente, datainicio_cliente, peso_cliente, altura_cliente, massagorda_cliente, massamagra_cliente, torax_cliente, cintura_cliente, abdome_cliente, quadril_cliente, bracoe_cliente, bracod_cliente, antebracoe_cliente, antebracod_cliente, coxae_cliente, coxad_cliente, panturrilhae_cliente, panturrilhad_cliente, idade_cliente) VALUES (@nome, @nascimento, @cpf, @telefone, @cep, @dataInicio, @peso, @altura, @massaGorda, @massaMagra, @torax, @cintura, @abdome, @quadril, @bracoE, @bracoD, @antebracoE, @antebracoD, @coxaE, @coxaD, @panturrilhaE, @panturrilhaD, @idade)";
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@nome", nome);
                        command.Parameters.AddWithValue("@nascimento", nascimento);
                        command.Parameters.AddWithValue("@cpf", cpf);
                        command.Parameters.AddWithValue("@telefone", telefone);
                        command.Parameters.AddWithValue("@cep", cep);
                        command.Parameters.AddWithValue("@dataInicio", dataInicio);
                        command.Parameters.AddWithValue("@peso", peso);
                        command.Parameters.AddWithValue("@altura", altura);
                        command.Parameters.AddWithValue("@massaGorda", massaGorda);
                        command.Parameters.AddWithValue("@massaMagra", massaMagra);
                        command.Parameters.AddWithValue("@torax", torax);
                        command.Parameters.AddWithValue("@cintura", cintura);
                        command.Parameters.AddWithValue("@abdome", abdome);
                        command.Parameters.AddWithValue("@quadril", quadril);
                        command.Parameters.AddWithValue("@bracoE", bracoE);
                        command.Parameters.AddWithValue("@bracoD", bracoD);
                        command.Parameters.AddWithValue("@antebracoE", antebracoE);
                        command.Parameters.AddWithValue("@antebracoD", antebracoD);
                        command.Parameters.AddWithValue("@coxaE", coxaE);
                        command.Parameters.AddWithValue("@coxaD", coxaD);
                        command.Parameters.AddWithValue("@panturrilhaE", panturrilhaE);
                        command.Parameters.AddWithValue("@panturrilhaD", panturrilhaD);
                        command.Parameters.AddWithValue("@idade", idade);

                        command.ExecuteNonQuery();
                        lblMensagem.Text = "Cadastro realizado com sucesso.";
                    }
                }
                catch (Exception ex)
                {
                    lblMensagem.Text = "Erro ao conectar ao banco de dados: " + ex.Message;
                }
            }

            // Limpar campos após o envio
            txtNome.Text = txtDataNascimento.Text = txtCPF.Text = txtTelefone.Text = txtCEP.Text = txtDataInicio.Text =
            txtPeso.Text = txtAltura.Text = txtMassaGorda.Text = txtMassaMagra.Text = txtTorax.Text =
            txtCintura.Text = txtAbdome.Text = txtQuadril.Text = txtBracoEsquerdo.Text = txtBracoDireito.Text =
            txtAntebracoEsquerdo.Text = txtAntebracoDireito.Text = txtCoxaEsquerda.Text = txtCoxaDireita.Text =
            txtPanturrilhaEsquerda.Text = txtPanturrilhaDireita.Text = txtIdade.Text = string.Empty;
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

        protected void txtNome_TextChanged(object sender, EventArgs e)
        {
            // Código que você deseja executar quando o texto muda.
        }
    }
}
