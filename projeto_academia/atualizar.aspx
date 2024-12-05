<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="atualizar.aspx.cs" Inherits="projeto_academia.atualizar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Atualização dos Dados</title>
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

        h2 {
            color: #333;
            font-size: 24px;
            margin-bottom: 20px;
        }

        label {
            color: #333;
            font-weight: bold;
            margin-bottom: 5px;
        }

        input[type="text"] {
            width: calc(100% - 10px);
            padding: 10px;
            margin-bottom: 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
            font-size: 16px;
            transition: border-color 0.3s; /* Transição suave na borda */
        }

        input[type="text"]:focus {
            border-color: #ff5722; /* Cor da borda ao focar */
            outline: none; /* Remove a borda padrão */
        }

        .styled-button {
            padding: 10px 20px;
            font-size: 16px;
            background-color: #ff5722;
            color: #fff;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            transition: background-color 0.3s; /* Transição suave na cor do botão */
        }

          .styled-button1 {
            padding: 10px 20px;
            font-size: 16px;
           background-color: #007bff;
            color: #fff;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }

        .styled-button2 {
            padding: 10px 20px;
            font-size: 16px;
             background-color: #007bff;
            color: #fff;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }

        .styled-button:hover {
            background-color: #e64a19;
        }

        .linkConta, .linkGrafico {
            display: inline-block;
            padding: 10px 20px;
            font-size: 16px;
            text-align: center;
            text-decoration: none;
            background-color: #007bff;
            color: #fff;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }



        .styled-button:hover {
            background-color: #e64a19;
        }

        .linkConta {
            display: inline-block;
            padding: 10px 20px;
            font-size: 16px;
            text-align: center;
            text-decoration: none;
            background-color: #007bff;
            color: #fff;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            transition: background-color 0.3s; /* Transição suave na cor do link */
        }

        .linkConta:hover {
            background-color: #0056b3; /* Cor do fundo ao passar o mouse */
        }

        /* Estilo para dividir as colunas */
        .container {
            display: flex;
            justify-content: space-between;
            margin-bottom: 30px; /* Adiciona espaço entre as seções */
        }

        .section {
            width: 100px; /* Ajusta a largura para caber duas seções lado a lado */
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 10px;
            background-color: #f9f9f9;
            height:50px;
            margin-top: 40px;
            margin-left:35px;
            animation: bounce 0.5s ease forwards; /* Animação de entrada da seção */
        }

        @keyframes bounce {
            0% { transform: translateY(-10px); opacity: 0; }
            50% { transform: translateY(5px); opacity: 0.5; }
            100% { transform: translateY(0); opacity: 1; }
        }

        .section-2 {
            width: 48%; /* Ajusta a largura para caber duas seções lado a lado */
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 10px;
            background-color: #f9f9f9;
            margin-top: 40px;
            margin-left:35px;
            margin-right:35px;
            animation: bounce 0.5s ease forwards; /* Animação de entrada da seção */
        }

        .subsection {
    margin-bottom: 20px;
    padding: 15px;
    border: 1px solid #ccc;
    border-radius: 10px;
    background-color: #f2f2f2;
}
.subsection h3 {
    color: #333;
    font-size: 20px;
    margin-bottom: 15px;
}

    </style>
</head>
<body>
     <canvas id="particlesCanvas"></canvas> <!-- Canvas para partículas -->
      <div class="logo">GymControl</div>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblBuscar" runat="server" Text="Buscar CPF:"></asp:Label>

            <asp:TextBox ID="txtBuscar" runat="server" Width="187px" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
            &nbsp;

            <asp:Label ID="lblMensagem" runat="server"></asp:Label>
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" CssClass="styled-button1" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btbAtualizar" runat="server" Text="Atualizar" OnClick="btbAtualizar_Click" CssClass="styled-button2"/>
            &nbsp;&nbsp;&nbsp;
           
             <asp:Button ID="btbUltimaAtualizacao" runat="server" Text="Ultima atualização" OnClick="btbUltimaAtualizacao_Click" CssClass="styled-button2"/>
            <asp:LinkButton ID="linkGrafico" runat="server" CssClass="linkGrafico" PostBackUrl="grafico.aspx">Grafico</asp:LinkButton>
             <asp:Button ID="btbExcluir" runat="server" Text="Excluir" OnClick="btbExcluir_Click" CssClass="styled-button"/>

        </div>

        <br />

        <!-- Container com flex para dividir os dados pessoais e as medidas -->
        <div class="container">
            <!-- Seção de Dados Pessoais -->
            <div class="section">
                <label for="txtCPF">CPF:</label><br />
                <asp:TextBox ID="txtCPF" ReadOnly="true" runat="server"></asp:TextBox><br />
            
            </div>

            <!-- Seção de Medidas Corporais -->
          <div class="section-2">
    <h2>Medidas do Corpo</h2>

    <!-- Div para Medidas Superiores -->
    <div class="subsection">
        <h3>Medidas Superiores</h3>
        <label for="txtPeso">Peso (kg):</label><br />
        <asp:TextBox ID="txtPeso" runat="server"></asp:TextBox><br />
        <label for="txtTorax">Tórax (cm):</label><br />
        <asp:TextBox ID="txtTorax" runat="server"></asp:TextBox><br />
        <label for="txtBracoEsquerdo">Braço Esquerdo (cm):</label><br />
        <asp:TextBox ID="txtBracoEsquerdo" runat="server"></asp:TextBox><br />
        <label for="txtBracoDireito">Braço Direito (cm):</label><br />
        <asp:TextBox ID="txtBracoDireito" runat="server"></asp:TextBox><br />
        <label for="txtCintura">Cintura (cm):</label><br />
        <asp:TextBox ID="txtCintura" runat="server"></asp:TextBox><br />
        <label for="txtAbdome">Abdome (cm):</label><br />
        <asp:TextBox ID="txtAbdome" runat="server"></asp:TextBox><br />
        <label for="txtAntebracoEsquerdo">Antebraço Esquerdo (cm):</label><br />
        <asp:TextBox ID="txtAntebracoEsquerdo" runat="server"></asp:TextBox><br />
        <label for="txtAntebracoDireito">Antebraço Direito (cm):</label><br />
        <asp:TextBox ID="txtAntebracoDireito" runat="server"></asp:TextBox><br />
    </div>

    <!-- Div para Medidas Inferiores -->
    <div class="subsection">
        <h3>Medidas Inferiores</h3>
        <label for="txtCoxaEsquerda">Coxa Esquerda (cm):</label><br />
        <asp:TextBox ID="txtCoxaEsquerda" runat="server"></asp:TextBox><br />
        <label for="txtCoxaDireita">Coxa Direita (cm):</label><br />
        <asp:TextBox ID="txtCoxaDireita" runat="server"></asp:TextBox><br />
        <label for="txtPanturrilhaEsquerda">Panturrilha Esquerda (cm):</label><br />
        <asp:TextBox ID="txtPanturrilhaEsquerda" runat="server"></asp:TextBox><br />
        <label for="txtPanturrilhaDireita">Panturrilha Direita (cm):</label><br />
        <asp:TextBox ID="txtPanturrilhaDireita" runat="server"></asp:TextBox><br />
        <label for="txtQuadril">Quadril (cm):</label><br />
        <asp:TextBox ID="txtQuadril" runat="server"></asp:TextBox><br />
     
        <label for="txtMassaGorda">Massa Gorda (kg):</label><br />
        <asp:TextBox ID="txtMassaGorda" runat="server"></asp:TextBox><br />
        <label for="txtMassaMagra">Massa Magra (kg):</label><br />
        <asp:TextBox ID="txtMassaMagra" runat="server"></asp:TextBox><br />
    </div>
</div>
        </div>
    </form>

     <script>
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
