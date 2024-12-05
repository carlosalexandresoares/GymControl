using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace projeto_academia
{
    public partial class Grafico : Page
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

                    // Consulta para buscar as medidas iniciais e atuais
                    string selectQuery = @"
                        SELECT 
                            c.nome_cliente, 
                            c.idade_cliente, 
                            c.datainicio_cliente, 
                            c.altura_cliente,
                            i.peso_cliente AS peso_cliente, 
                            i.massagorda_cliente AS massagorda_cliente, 
                            i.massamagra_cliente AS massamagra_cliente, 
                            i.torax_cliente AS torax_cliente, 
                            i.cintura_cliente AS cintura_cliente, 
                            i.abdome_cliente AS abdome_cliente, 
                            i.quadril_cliente AS quadril_cliente, 
                            i.bracoe_cliente AS bracoe_cliente, 
                            i.bracod_cliente AS bracod_cliente, 
                            i.antebracoe_cliente AS antebracoe_cliente, 
                            i.antebracod_cliente AS antebracod_cliente, 
                            i.coxae_cliente AS coxae_cliente, 
                            i.coxad_cliente AS coxad_cliente, 
                            i.panturrilhae_cliente AS panturrilhae_cliente, 
                            i.panturrilhad_cliente AS panturrilhad_cliente,
                            d.peso_cliente_atual, 
                            d.massagorda_cliente_atual, 
                            d.massamagra_cliente_atual, 
                            d.torax_cliente_atual, 
                            d.cintura_cliente_atual, 
                            d.abdome_cliente_atual, 
                            d.quadril_cliente_atual, 
                            d.bracoe_cliente_atual, 
                            d.bracod_cliente_atual, 
                            d.antebracoe_cliente_atual, 
                            d.antebracod_cliente_atual, 
                            d.coxae_cliente_atual, 
                            d.coxad_cliente_atual, 
                            d.panturrilhae_cliente_atual, 
                            d.panturrilhad_cliente_atual
                        FROM DadosCliente c
                        JOIN DadosCliente i ON c.cpf_cliente = i.cpf_cliente
                        JOIN DadosClienteAtual d ON c.cpf_cliente = d.cpf_cliente_atual
                        WHERE c.cpf_cliente = @cpf";

                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@cpf", cpf);

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            txtNome.Text = reader["nome_cliente"].ToString();
                            txtIdade.Text = reader["idade_cliente"].ToString();
                            txtDataInicio.Text = Convert.ToDateTime(reader["datainicio_cliente"]).ToString("dd/MM/yyyy");
                            txtAltura.Text = reader["altura_cliente"].ToString();

                            // Medidas iniciais
                            string PesoInicial = reader["peso_cliente"].ToString();
                            string MassaGordaInicial = reader["massagorda_cliente"].ToString();
                            string MassaMagraInicial = reader["massamagra_cliente"].ToString();
                            string ToraxInicial = reader["torax_cliente"].ToString();
                            string CinturaInicial = reader["cintura_cliente"].ToString();
                            string AbdomeInicial = reader["abdome_cliente"].ToString();
                            string QuadrilInicial = reader["quadril_cliente"].ToString();
                            string BracoEsquerdoInicial = reader["bracoe_cliente"].ToString();
                            string BracoDireitoInicial = reader["bracod_cliente"].ToString();
                            string AntebracoEsquerdoInicial = reader["antebracoe_cliente"].ToString();
                            string AntebracoDireitoInicial = reader["antebracod_cliente"].ToString();
                            string CoxaEsquerdaInicial = reader["coxae_cliente"].ToString();
                            string CoxaDireitaInicial = reader["coxad_cliente"].ToString();
                            string PanturrilhaEsquerdaInicial = reader["panturrilhae_cliente"].ToString();
                            string PanturrilhaDireitaInicial = reader["panturrilhad_cliente"].ToString();

                            // Medidas atuais
                            string PesoAtual = reader["peso_cliente_atual"].ToString();
                            string MassaGordaAtual = reader["massagorda_cliente_atual"].ToString();
                            string MassaMagraAtual = reader["massamagra_cliente_atual"].ToString();
                            string ToraxAtual = reader["torax_cliente_atual"].ToString();
                            string CinturaAtual = reader["cintura_cliente_atual"].ToString();
                            string AbdomeAtual = reader["abdome_cliente_atual"].ToString();
                            string QuadrilAtual = reader["quadril_cliente_atual"].ToString();
                            string BracoEsquerdoAtual = reader["bracoe_cliente_atual"].ToString();
                            string BracoDireitoAtual = reader["bracod_cliente_atual"].ToString();
                            string AntebracoEsquerdoAtual = reader["antebracoe_cliente_atual"].ToString();
                            string AntebracoDireitoAtual = reader["antebracod_cliente_atual"].ToString();
                            string CoxaEsquerdaAtual = reader["coxae_cliente_atual"].ToString();
                            string CoxaDireitaAtual = reader["coxad_cliente_atual"].ToString();
                            string PanturrilhaEsquerdaAtual = reader["panturrilhae_cliente_atual"].ToString();
                            string PanturrilhaDireitaAtual = reader["panturrilhad_cliente_atual"].ToString();

                            // Chama a função JavaScript para gerar o gráfico
                            ScriptManager.RegisterStartupScript(this, GetType(), "gerarGrafico",
                                $"gerarGrafico(" +
                                $"{PesoAtual}, {MassaGordaAtual}, {MassaMagraAtual}, {ToraxAtual}, {CinturaAtual}, {AbdomeAtual}, " +
                                $"{QuadrilAtual}, {BracoEsquerdoAtual}, {BracoDireitoAtual}, {AntebracoEsquerdoAtual}, {AntebracoDireitoAtual}, " +
                                $"{CoxaEsquerdaAtual}, {CoxaDireitaAtual}, {PanturrilhaEsquerdaAtual}, {PanturrilhaDireitaAtual}, " +
                                $"{PesoInicial}, {MassaGordaInicial}, {MassaMagraInicial}, {ToraxInicial}, {CinturaInicial}, {AbdomeInicial}, " +
                                $"{QuadrilInicial}, {BracoEsquerdoInicial}, {BracoDireitoInicial}, {AntebracoEsquerdoInicial}, {AntebracoDireitoInicial}, " +
                                $"{CoxaEsquerdaInicial}, {CoxaDireitaInicial}, {PanturrilhaEsquerdaInicial}, {PanturrilhaDireitaInicial});", true);
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
    }
}
