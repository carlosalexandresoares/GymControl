<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cadastro.aspx.cs" Inherits="projeto_academia.cadastro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Cadastro</title>
    <style type="text/css">
        @import url('https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600;700;800;900&display=swap');

        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
            font-family: 'Poppins', sans-serif;
        }

        body {
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
            overflow: hidden; /* Impede rolagem */
            position: relative;
            background-color: #3B3A39; /* Fundo escuro */
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

        .container {
            max-width: 400px;
            padding: 20px;
            border-radius: 15px;
            background-color: rgba(255, 255, 255, 0.1); /* Fundo semi-transparente */
            backdrop-filter: blur(10px); /* Efeito de desfoque de fundo */
            box-shadow: 0 4px 30px rgba(0, 0, 0, 0.6);
            z-index: 1; /* Para ficar acima das partículas */
        }

        h1 {
            color: #E47317; /* Cor do título */
            text-align: center;
            margin-bottom: 20px;
            font-size: 32px; /* Tamanho do título */
            text-shadow: 1px 1px 3px rgba(0, 0, 0, 0.6);
        }

        .form label {
            display: block;
            font-weight: 600;
            margin-bottom: 5px;
            color: #E47317; /* Cor do texto das labels */
        }

        .form input[type="text"],
        .form input[type="password"] {
            width: 100%;
            padding: 12px;
            margin-bottom: 15px;
            border: 2px solid transparent;
            border-radius: 5px;
            background: rgba(255, 255, 255, 0.2); /* Fundo dos inputs */
            transition: border-color 0.3s, background-color 0.3s;
        }

        .form input[type="text"]:focus,
        .form input[type="password"]:focus {
            border-color: #E47317; /* Cor do foco dos inputs */
            background-color: rgba(255, 255, 255, 0.3); /* Fundo ao focar */
            outline: none;
        }

        .form .linkConta {
            font-size: 14px;
            text-decoration: none;
            color: #E47317; /* Cor do link */
            margin-bottom: 10px;
            display: block;
            text-align: center;
            transition: color 0.3s;
        }

        .form .linkConta:hover {
            color: #d86512; /* Cor do link ao passar o mouse */
        }

        .form .btnCadastro {
            background-color: #E47317; /* Cor do botão de cadastro */
            color: #fff;
            padding: 12px 20px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            transition: background-color 0.3s, transform 0.2s;
            width: 100%; /* Botão ocupa toda a largura */
            font-weight: 600;
            font-size: 18px; /* Tamanho do texto do botão */
        }

        .form .btnCadastro:hover {
            background-color: #d86512; /* Cor do botão ao passar o mouse */
            transform: translateY(-2px); /* Efeito de movimento ao passar o mouse */
        }

        .form .error-message {
            color: red; /* Cor para mensagens de erro */
            text-align: center;
            margin-top: 10px;
        }

        canvas {
            position: absolute;
            top: 0;
            left: 0;
            z-index: 0; /* Fica atrás do conteúdo */
        }
    </style>
   
</head>
<body>
    <canvas id="particlesCanvas"></canvas>
    <div class="logo">GymControl</div>
    <div class="container">
        <h1>Cadastro</h1>
        <form id="form1" runat="server" class="form">
            <asp:Label ID="lblCpf" runat="server" Text="CPF:"></asp:Label>
            <asp:TextBox ID="txtCpf" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="lblSenha" runat="server" Text="Senha:"></asp:Label>
            <asp:TextBox ID="txtSenha" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <asp:LinkButton ID="linkConta" runat="server" CssClass="linkConta" PostBackUrl="login.aspx">Já tenho conta</asp:LinkButton>
            <asp:Label ID="lblmensagem" runat="server"></asp:Label>
            <br />
            <asp:Button ID="btnCadastro" runat="server" Text="Cadastrar" CssClass="btnCadastro" OnClick="btnCadastro_Click" />
            <br />
        </form>
    </div>
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
