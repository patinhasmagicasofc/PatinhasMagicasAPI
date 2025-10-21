-- Tipo de usuário (Cliente, Funcionário, Admin, etc.)
CREATE TABLE TipoUsuario (
    IdTipoUsuario INT PRIMARY KEY IDENTITY,
    nome VARCHAR(20) NOT NULL
);

-- Usuário
CREATE TABLE Usuario (
    IdUsuario INT PRIMARY KEY IDENTITY,
    nome VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL UNIQUE,
    senha VARCHAR(255) NOT NULL,
    CPF VARCHAR(14) NOT NULL UNIQUE,
    ddd VARCHAR(3),
    telefone VARCHAR(10),
    ativo BIT DEFAULT(1),
    dataCadastro DATE DEFAULT(GETDATE()),
    IdTipoUsuario INT NOT NULL FOREIGN KEY REFERENCES TipoUsuario(IdTipoUsuario)
);

-- Endereço
CREATE TABLE Endereco (
    IdEndereco INT PRIMARY KEY IDENTITY,
    logradouro VARCHAR(255) NOT NULL,
    numero VARCHAR(10) NOT NULL,
    bairro VARCHAR(50) NOT NULL,
    cidade VARCHAR(50) NOT NULL,
    estado VARCHAR(2) NOT NULL,
    CEP VARCHAR(9) NOT NULL,
    complemento VARCHAR(50),
    IdUsuario INT NOT NULL FOREIGN KEY REFERENCES Usuario(IdUsuario)
);

-- ========================================================
-- 2️⃣ CATEGORIA E PRODUTO
-- ========================================================

CREATE TABLE Categoria (
    IdCategoria INT PRIMARY KEY IDENTITY,
    nome VARCHAR(255) NOT NULL
);

CREATE TABLE Produto (
    IdProduto INT PRIMARY KEY IDENTITY,
    nome VARCHAR(255) NOT NULL,
    marca VARCHAR(255) NOT NULL,
    preco DECIMAL(10,2) NOT NULL,
    urlImagem VARCHAR(255),
    descricao VARCHAR(255),
    descricaoDetalhada TEXT,
    codigo INT NOT NULL UNIQUE,
    validade DATE NOT NULL,
    IdCategoria INT NOT NULL FOREIGN KEY REFERENCES Categoria(IdCategoria)
);

-- ========================================================
-- 3️⃣ PEDIDOS E ITENS DE PEDIDO
-- ========================================================

CREATE TABLE StatusPedido (
    IdStatusPedido INT PRIMARY KEY IDENTITY,
    nome VARCHAR(255) NOT NULL
);

CREATE TABLE Pedido (
    IdPedido INT PRIMARY KEY IDENTITY,
    dataPedido DATETIME NOT NULL DEFAULT(GETDATE()),
    IdUsuario INT NOT NULL FOREIGN KEY REFERENCES Usuario(IdUsuario),
    IdStatusPedido INT NOT NULL FOREIGN KEY REFERENCES StatusPedido(IdStatusPedido)
);

CREATE TABLE ItensPedido (
    IdItemPedido INT PRIMARY KEY IDENTITY,
    quantidade INT NOT NULL,
    precoUnitario DECIMAL(10,2) NOT NULL,
    IdProduto INT NOT NULL FOREIGN KEY REFERENCES Produto(IdProduto),
    IdPedido INT NOT NULL FOREIGN KEY REFERENCES Pedido(IdPedido)
);

-- ========================================================
-- 4️⃣ PAGAMENTO
-- ========================================================

CREATE TABLE TipoPagamento (
    IdTipoPagamento INT PRIMARY KEY IDENTITY,
    nome VARCHAR(255) NOT NULL  -- ex: 'Pix', 'Crédito', 'Débito'
);

CREATE TABLE StatusPagamento (
    IdStatusPagamento INT PRIMARY KEY IDENTITY,
    nome VARCHAR(255) NOT NULL  -- ex: 'Pendente', 'Pago', 'Falhou'
);

CREATE TABLE Pagamento (
    IdPagamento INT PRIMARY KEY IDENTITY,
    valor DECIMAL(10,2) NOT NULL,
    dataPagamento DATETIME NOT NULL DEFAULT(GETDATE()),
    observacao VARCHAR(255) NULL,
    IdTipoPagamento INT NOT NULL FOREIGN KEY REFERENCES TipoPagamento(IdTipoPagamento),
    IdStatusPagamento INT NOT NULL FOREIGN KEY REFERENCES StatusPagamento(IdStatusPagamento),
    IdPedido INT NOT NULL FOREIGN KEY REFERENCES Pedido(IdPedido)
);

-- ========================================================
-- 5️⃣ SERVIÇOS E TIPOS DE SERVIÇOS
-- ========================================================

CREATE TABLE TipoServico (
    IdTipoServico INT PRIMARY KEY IDENTITY,
    nome VARCHAR(50) NOT NULL
);

CREATE TABLE Servico (
    IdServico INT PRIMARY KEY IDENTITY,
    nome VARCHAR(50) NOT NULL,
    descricao VARCHAR(255),
    descricaoDetalhada TEXT,
    tempoEstimadoMinutos INT NULL,
    ativo BIT NOT NULL DEFAULT(1),
    IdTipoServico INT NOT NULL FOREIGN KEY REFERENCES TipoServico(IdTipoServico)
);

-- ========================================================
-- 6️⃣ TAMANHO E ANIMAIS
-- ========================================================

CREATE TABLE TamanhoAnimal (
    IdTamanhoAnimal INT PRIMARY KEY IDENTITY,
    tamanho VARCHAR(20) NOT NULL
);

CREATE TABLE Animal (
    IdAnimal INT PRIMARY KEY IDENTITY,
    nome VARCHAR(100) NOT NULL,
    especie VARCHAR(50) NOT NULL,
    raca VARCHAR(100),
    idade INT,
    IdTamanhoAnimal INT NOT NULL FOREIGN KEY REFERENCES TamanhoAnimal(IdTamanhoAnimal),
    IdUsuario INT NOT NULL FOREIGN KEY REFERENCES Usuario(IdUsuario)
);

-- ========================================================
-- 7️⃣ RELAÇÃO SERVIÇO × TAMANHO DO ANIMAL
-- ========================================================

CREATE TABLE ServicoTamanho (
    IdServicoTamanho INT PRIMARY KEY IDENTITY,
    preco DECIMAL(10,2) NOT NULL,
    IdServico INT NOT NULL FOREIGN KEY REFERENCES Servico(IdServico),
    IdTamanhoAnimal INT NOT NULL FOREIGN KEY REFERENCES TamanhoAnimal(IdTamanhoAnimal)
);

-- ========================================================
-- 8️⃣ AGENDAMENTO
-- ========================================================

CREATE TABLE StatusAgendamento (
    IdStatusAgendamento INT PRIMARY KEY IDENTITY,
    nome VARCHAR(50) NOT NULL
);

CREATE TABLE Agendamento (
    IdAgendamento INT PRIMARY KEY IDENTITY,
    dataAgendamento DATETIME2(0) NOT NULL,
    dataCadastro DATETIME2(0) NOT NULL DEFAULT(GETDATE()),
    IdAnimal INT NOT NULL FOREIGN KEY REFERENCES Animal(IdAnimal),
    IdPedido INT NOT NULL FOREIGN KEY REFERENCES Pedido(IdPedido),
    IdStatusAgendamento INT NOT NULL FOREIGN KEY REFERENCES StatusAgendamento(IdStatusAgendamento)
);

CREATE TABLE AgendamentoServico (
    IdAgendamentoServico INT PRIMARY KEY IDENTITY,
    preco DECIMAL(10,2) NOT NULL,
    IdAgendamento INT NOT NULL FOREIGN KEY REFERENCES Agendamento(IdAgendamento),
    IdServico INT NOT NULL FOREIGN KEY REFERENCES Servico(IdServico)
);

-- ========================================================
-- 1️⃣ TamanhoAnimal
-- ========================================================
INSERT INTO TamanhoAnimals (Nome) VALUES
('Pequeno'),
('Médio'),
('Grande');

-- ========================================================
-- 2️⃣ Usuário
-- ========================================================
INSERT INTO Usuario (nome, email, senha, CPF, ddd, telefone, ativo)
VALUES
('João Silva', 'joao@email.com', 'senha123', '123.456.789-00', '11', '999999999', 1);

-- ========================================================
-- 3️⃣ Animal
-- ========================================================
-- Supondo que João Silva tenha IdUsuario = 1
INSERT INTO Animals (nome, especie, raca, idade, TamanhoAnimalId, UsuarioId)
VALUES
('Luna', 'Cachorro', 'Vira-lata', 3, 2, 1),  -- Médio
('Mimi', 'Gato', 'Siames', 2, 1, 1);         -- Pequeno

-- ========================================================
-- 4️⃣ TipoServico
-- ========================================================
INSERT INTO TiposServico(nome) VALUES
('Higiene'),
('Saúde');

-- ========================================================
-- 5️⃣ Servico
-- ========================================================
INSERT INTO Servicos (nome, descricao, tempoEstimadoMinutos, ativo, TipoServicoId)
VALUES
('Banho', 'Banho simples', 45, 1, 1),
('Tosa Completa', 'Tosa completa e banho', 60, 1, 1),
('Consulta Veterinária', 'Consulta geral', 30, 1, 2);

-- ========================================================
-- 6️⃣ ServicoTamanho
-- ========================================================
-- Banho: Pequeno, Médio, Grande
INSERT INTO ServicoTamanhos(preco, ServicoId, TamanhoAnimalId) VALUES
(30.00, 1, 1),  -- Banho Pequeno
(50.00, 1, 2),  -- Banho Médio
(70.00, 1, 3),  -- Banho Grande

-- Tosa Completa: Pequeno, Médio, Grande
(60.00, 2, 1),
(80.00, 2, 2),
(100.00, 2, 3),

-- Consulta Veterinária: mesmo preço para todos tamanhos
(120.00, 3, 1),
(120.00, 3, 2),
(120.00, 3, 3);