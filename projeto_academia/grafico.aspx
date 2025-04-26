<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="grafico.aspx.cs" Inherits="projeto_academia.Grafico" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dados do Cliente</title>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/2.4.0/jspdf.umd.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/1.4.1/html2canvas.min.js"></script>
    <style>

           body {
              background-color: #3B3A39; /* Fundo escuro */
            font-family: Arial, sans-serif;
            overflow: auto; /* Para impedir scroll se houver overflow */
            animation: fadeIn 0.5s ease; /* Animação de entrada da página */
            position: relative; /* Para posicionar o canvas */
            height: 100vh; /* Para garantir que o canvas preencha a tela */
            margin: 0; /* Remove margens padrão do body */
            padding: 0; /* Remove preenchimento padrão do body */
        }
        .logo {
            position: absolute;
            top: 20px;
            left: 20px;
            font-size: 28px;
            font-weight: bold;
            color: #E47317; /* Cor do logo */
            text-shadow: 3px 3px 6px rgba(0, 0, 0, 0.8);
            letter-spacing: 2px; /* Espaçamento das letras */
        }
      
        @keyframes fadeIn {
            from { opacity: 0; }
            to { opacity: 1; }
        }

        #particlesCanvas {
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            z-index: 1; /* Coloca o canvas atrás do formulário */
        }
     form {
            max-width: 1000px;
            margin: 0 auto;
            background-color: rgba(255, 255, 255, 0.9); /* Fundo branco semi-transparente */
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            position: relative; /* Para que o z-index funcione */
            z-index: 2; /* Coloca o formulário acima do canvas */
        }

    label, input{
        display: block;
        width: 100%;
        margin-bottom: 15px;
    }

    label {
        font-weight: bold;
        color: #333;
    }

    input[type="text"], input[type="date"] {
        padding: 10px;
        border: 1px solid #ddd;
        border-radius: 5px;
        font-size: 16px;
        width: 95%;
    }

    #btnBuscar {
        padding: 10px;
        width:100px;
        background-color: blue;
        color: white;
        border: none;
        border-radius: 5px;
        cursor: pointer;
        font-size: 16px;
        text-align: center;
    }

    #btnBuscar:hover {
        background-color: #0056b3;
    }

      #btnDownload {
        padding: 10px;
        width:100px;
        background-color: blue;
        color: white;
        border: none;
        border-radius: 5px;
        cursor: pointer;
        font-size: 16px;
        text-align: center;
    }

    #btnDownload:hover {
        background-color: #0056b3;
    }
     #btnVoltar {
        padding: 10px;
        width:100px;
        background-color: blue;
        color: white;
        border: none;
        border-radius: 5px;
        cursor: pointer;
        font-size: 16px;
        text-decoration:none;
    }

    #btnVoltar:hover {
        background-color: #0056b3;
    }

    #myChart {
        margin-top: 20px;
        margin-bottom: 40px;
    }
    
      #particlesCanvas {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    z-index: -1; /* Garante que as partículas fiquem atrás do conteúdo */
}
</style>
   <script>
function gerarGrafico(
    PesoAtual, MassaGordaAtual, MassaMagraAtual, ToraxAtual, CinturaAtual, AbdomeAtual, QuadrilAtual, 
    BracoEsquerdoAtual, BracoDireitoAtual, AntebracoEsquerdoAtual, AntebracoDireitoAtual, CoxaEsquerdaAtual, CoxaDireitaAtual, 
    PanturrilhaEsquerdaAtual, PanturrilhaDireitaAtual,
    PesoInicial, MassaGordaInicial, MassaMagraInicial, ToraxInicial, CinturaInicial, AbdomeInicial, QuadrilInicial, 
    BracoEsquerdoInicial, BracoDireitoInicial, AntebracoEsquerdoInicial, AntebracoDireitoInicial, CoxaEsquerdaInicial, CoxaDireitaInicial, 
    PanturrilhaEsquerdaInicial, PanturrilhaDireitaInicial
) {
   const ctx = document.getElementById("myChart").getContext("2d");


    const labels = [
        "Peso", "Massa Gorda", "Massa Magra", "Tórax", "Cintura", "Abdome", "Quadril",
        "Braço E", "Braço D", "Antebraço E", "Antebraço D", "Coxa E", "Coxa D",
        "Panturrilha E", "Panturrilha D"
    ];

    const dadosIniciais = [
        PesoInicial, MassaGordaInicial, MassaMagraInicial, ToraxInicial, CinturaInicial, AbdomeInicial, QuadrilInicial,
        BracoEsquerdoInicial, BracoDireitoInicial, AntebracoEsquerdoInicial, AntebracoDireitoInicial,
        CoxaEsquerdaInicial, CoxaDireitaInicial, PanturrilhaEsquerdaInicial, PanturrilhaDireitaInicial
    ];

    const dadosAtuais = [
        PesoAtual, MassaGordaAtual, MassaMagraAtual, ToraxAtual, CinturaAtual, AbdomeAtual, QuadrilAtual,
        BracoEsquerdoAtual, BracoDireitoAtual, AntebracoEsquerdoAtual, AntebracoDireitoAtual,
        CoxaEsquerdaAtual, CoxaDireitaAtual, PanturrilhaEsquerdaAtual, PanturrilhaDireitaAtual
    ];

    new Chart(ctx, {
        type: "bar",
        data: {
            labels: labels,
            datasets: [
                {
                    label: "Medidas Iniciais",
                    backgroundColor: "rgba(0, 123, 255, 0.5)",
                    borderColor: "rgba(0, 123, 255, 1)",
                    borderWidth: 1,
                    data: dadosIniciais
                },
                {
                    label: "Medidas Atuais",
                    backgroundColor: "rgba(40, 167, 69, 0.5)",
                    borderColor: "rgba(40, 167, 69, 1)",
                    borderWidth: 1,
                    data: dadosAtuais
                }
            ]
        },
        options: {
            responsive: true,
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
       };




    
    async function downloadPDF() {
        const { jsPDF } = window.jspdf;
        const pdf = new jsPDF();
        const canvas = await html2canvas(document.getElementById('myChart'));
        const chartImage = canvas.toDataURL('image/png');

        pdf.setFontSize(18);
        pdf.text("Dados do Cliente", 10, 10);

        // Informações do cliente
        pdf.setFontSize(12);
        pdf.text("Nome: " + document.getElementById('txtNome').value, 10, 20);
        pdf.text("Idade: " + document.getElementById('txtIdade').value, 10, 30);
        pdf.text("CPF: " + document.getElementById('txtBuscar').value, 10, 40);

        // Adiciona o gráfico
        pdf.addImage(chartImage, 'PNG', 10, 50, 180, 90);
        pdf.save("grafico_cliente.pdf");
    }
</script>

</head>
<body>
    <canvas id="particlesCanvas"></canvas>
     <div class="logo">GymControl</div>
    <div class="section-2">
    <form id="form1" runat="server">
        
            <asp:Label ID="lblMensagem" runat="server" Text=""></asp:Label>
            <asp:Label ID="Label1" runat="server" Text="CPF:"></asp:Label>
            <asp:TextBox ID="txtBuscar" runat="server" placeholder="Digite o CPF"></asp:TextBox>
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar CPF" OnClick="btnBuscar_Click" />
            <asp:Label ID="Label2" runat="server" Text="NOME:"></asp:Label>
            <asp:TextBox ID="txtNome" ReadOnly="true" runat="server" placeholder="Nome"></asp:TextBox>
            <asp:Label ID="Label3" runat="server" Text="IDADE:"></asp:Label>
            <asp:TextBox ID="txtIdade" ReadOnly="true" runat="server" placeholder="Idade"></asp:TextBox>
         <asp:Label ID="Label5" runat="server" Text="ALTURA:"></asp:Label>
            <asp:TextBox ID="txtAltura" ReadOnly="true" runat="server" placeholder="Altura"></asp:TextBox>
            <asp:Label ID="Label4" runat="server" Text="Data de Início:"></asp:Label>
            <asp:TextBox ID="txtDataInicio" ReadOnly="true" runat="server" placeholder="Data de Início"></asp:TextBox>
           
            <canvas id="myChart" width="200" height="100"></canvas>
            <asp:LinkButton ID="btnVoltar" runat="server" CssClass="linkVoltar" PostBackUrl="Dados.aspx">Voltar</asp:LinkButton>
        <button type="button" id="btnDownload"  onclick="downloadPDF()">Baixar Gráfico</button>
    </form>
        </div>

    <script>
    // Código da animação de partículas
    const canvas = document.getElementById('particlesCanvas');
    const ctx = canvas.getContext('2d');

    // Define o tamanho do canvas
    canvas.width = window.innerWidth;
    canvas.height = window.innerHeight;

    let particles = [];
    const particleCount = 150; // Número de partículas

    // Classe Partícula
    class Particle {
        constructor(x, y) {
            this.x = x;
            this.y = y;
            this.size = Math.random() * 5 + 2; // Tamanho aleatório
            this.speedX = Math.random() * 3 - 1.5; // Velocidade X aleatória
            this.speedY = Math.random() * 3 - 1.5; // Velocidade Y aleatória
        }

        update() {
            this.x += this.speedX;
            this.y += this.speedY;

            // Verifica as bordas
            if (this.size < 0) this.size = 0;
            if (this.size > 6) this.size = 6;

            if (this.x < 0 || this.x > canvas.width) this.speedX *= -1;
            if (this.y < 0 || this.y > canvas.height) this.speedY *= -1;
        }

        draw() {
            ctx.fillStyle = '#E47317'; // Cor da partícula
            ctx.beginPath();
            ctx.arc(this.x, this.y, this.size, 0, Math.PI * 2);
            ctx.fill();
        }
    }

    // Cria partículas
    for (let i = 0; i < particleCount; i++) {
        const x = Math.random() * canvas.width;
        const y = Math.random() * canvas.height;
        particles.push(new Particle(x, y));
    }

    // Anima as partículas
    function animateParticles() {
        ctx.clearRect(0, 0, canvas.width, canvas.height); // Limpa o canvas

        particles.forEach(particle => {
            particle.update();
            particle.draw();
        });

        requestAnimationFrame(animateParticles); // Chama a função novamente
    }

    // Inicia a animação
    animateParticles();

    // Ajusta o canvas ao redimensionar a janela
    window.addEventListener('resize', () => {
        canvas.width = window.innerWidth;
        canvas.height = window.innerHeight;
    });
</script>
</body>
</html>
