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
INSERT INTO Usuario (nome, email, senha, CPF, ddd, telefone, ativo, IdTipoUsuario)
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

-- ============================================
-- Tabelas Fixas
-- ============================================

-- Tipos de Usuário
INSERT INTO TiposUsuarios (DescricaoTipoUsuario) VALUES
('Administrador'),
('Funcionário'),
('Cliente');

-- Tipo de Pagamentos
INSERT INTO TiposPagamentos (Nome) VALUES
('Cartão de Crédito'),
('Cartão de Débito'),
('Dinheiro'),
('PIX');

-- Status do Pedido
INSERT INTO StatusPedidos (Nome) VALUES
('Pendente'),
('Confirmado'),
('Cancelado'),
('Concluído');

-- Status de Agendamento
INSERT INTO StatusAgendamentos (Nome) VALUES
('Pendente'),
('Confirmado'),
('Cancelado'),
('Concluído');

-- Status de Pagamento
INSERT INTO StatusPagamentos (Status) VALUES
('Pendente'),
('Em Processamento'),
('Aprovado'),
('Recusado'),
('Cancelado'),
('Estornado'),
('Expirado');

-- Categorias
INSERT INTO Categorias (Nome) VALUES
('Ração'),
('Brinquedos'),
('Acessórios'),
('Medicamentos');

-- Tipos de Serviço
INSERT INTO TiposServico (Nome) VALUES
('Banho'),
('Tosa'),
('Consulta Veterinária'),
('Vacinação');


-- ============================================
-- Dados de Exemplo (para DEV)
-- ============================================

-- Usuários
INSERT INTO Usuarios (Nome, CPF, Email, Ddd, Telefone, TipoUsuarioId) VALUES
('Admin Master', '00000000000', 'admin@teste.com', 11, '999999999', 1),
('Maria Funcionária', '11111111111', 'maria@teste.com', 11, '988888888', 2),
('João Cliente', '22222222222', 'joao@teste.com', 11, '977777777', 3);

-- Endereços
INSERT INTO Enderecos (Logradouro, Numero, Bairro, Cidade, Estado, CEP, Complemento, UsuarioId) VALUES
('Rua Central', 100, 'Centro', 'São Paulo', 'SP', '01000000', 'Apto 101', 3);

-- Produtos
INSERT INTO Produtos (Nome, Preco, Foto, Codigo, Validade, CategoriaId) VALUES
('Ração Premium Cães', 120.50, 'racao.jpg', 'RAC001', DATEADD(MONTH, 6, GETDATE()), 1),
('Bola de Brinquedo', 20.00, 'bola.jpg', 'BRQ001', DATEADD(MONTH, 12, GETDATE()), 2);

-- Pedido
INSERT INTO Pedidos (DataPedido, ClienteId, UsuarioId, StatusPedidoId) VALUES
(GETDATE(), 3, 2, 1);

-- Itens do Pedido
INSERT INTO ItensPedido (Quantidade, PrecoUnitario, ProdutoId, PedidoId) VALUES
(2, 120.50, 1, 1),
(1, 20.00, 2, 1);

-- Pagamento
INSERT INTO Pagamentos (Data, valor, StatusPagamentoId, TipoPagamentoId, PedidoId) VALUES
(GETDATE(), 100, 3, 4, 1);

-- Agendamento
INSERT INTO Agendamentos (DataAgendamento, DataCadastro, PedidoId, IdStatusAgendamento) VALUES
(DATEADD(DAY, 2, GETDATE()), GETDATE(), 1, 1);


USE [PatinhasMagicasDB]
GO

-- Tipos de Usuários
INSERT INTO TiposUsuarios(Nome)
VALUES 
('Administrador'),
('Funcionário'),
('Administrador'),
('Veterinário'),
('Fornecedor');

-- Usuários
INSERT INTO Usuarios (Nome, CPF, Email, Ddd, Telefone, TipoUsuarioId, DataCadastro, Senha)
VALUES
('Adm','111.111.111-10','adm@pm.com',11,'999911111',1, GETDATE(), 'João Silva'),
('Maria Souza','222.222.222-22','maria@email.com',21,'988822222',1,GETDATE(), 'João Silva'),
('Carlos Lima','333.333.333-33','carlos@email.com',31,'977733333',2, GETDATE(), 'João Silva'),
('Ana Paula','444.444.444-44','ana@email.com',41,'966644444',2, GETDATE(), 'João Silva'),
('Lucas Santos','555.555.555-55','lucas@email.com',51,'955555555',3, GETDATE(), 'João Silva');

-- Endereços
INSERT INTO Enderecos (Logradouro, Numero, Bairro, Cidade, Estado, CEP, UsuarioId)
VALUES
('Rua A',100,'Centro','São Paulo','SP','01001-000',1),
('Rua B',200,'Jardim','Rio de Janeiro','RJ','20020-000',2),
('Rua C',300,'Bela Vista','Belo Horizonte','MG','30100-000',3),
('Rua D',400,'Centro','Curitiba','PR','80010-000',4),
('Rua E',500,'Centro','Porto Alegre','RS','90010-000',5);

-- Categorias de Produtos
INSERT INTO Categorias (Nome)
VALUES ('Ração'),('Brinquedos'),('Medicamentos'),('Acessórios'),('Higiene');

-- Produtos
INSERT INTO Produtos (Nome, Preco, Foto, Codigo, Validade, CategoriaId)
VALUES
('Ração Premium',120.00,'img1.jpg','RAC001','2026-12-31',1),
('Bola de Borracha',25.00,'img2.jpg','BRQ002','2027-01-15',2),
('Antipulgas',50.00,'img3.jpg','ANT003','2025-11-30',3),
('Coleira Pet',30.00,'img4.jpg','COL004','2028-03-10',4),
('Shampoo Pet',40.00,'img5.jpg','SHM005','2026-05-20',5);

-- Status de Pagamentos
INSERT INTO StatusPagamentos (Nome)
VALUES ('Pendente'),('Pago'),('Cancelado'),('Estornado'),('Aguardando');

-- Tipos de Pagamento
INSERT INTO TipoPagamentos (Nome)
VALUES ('Cartão de Crédito'),('Boleto'),('Pix'),('Dinheiro'),('Transferência');

-- Status de Pedidos
INSERT INTO StatusPedidos (Nome)
VALUES ('Pendente'),('Processando'),('Concluído'),('Cancelado'),('Entregue');

-- Status de Agendamentos
INSERT INTO StatusAgendamentos (Nome)
VALUES ('Agendado'),('Em andamento'),('Concluído'),('Cancelado'),('Aguardando');

-- Tipos de Serviço
INSERT INTO TiposServico (Nome)
VALUES ('Banho'),('Tosa'),('Consulta Veterinária'),('Vacinação'),('Hospedagem');

-- Serviços
INSERT INTO Servicos (Nome, Descricao, Status, TipoServicoId)
VALUES
('Banho Completo','Banho e secagem','Ativo',1),
('Tosa Higiênica','Apenas corte de pelos','Ativo',2),
('Consulta Geral','Checkup de saúde','Ativo',3),
('Vacina Antirrábica','Vacina anual','Ativo',4),
('Hospedagem Pet','Hospedagem por período','Ativo',5);

-- Pedidos
INSERT INTO Pedidos (DataPedido, ClienteId, UsuarioId, StatusPedidoId)
VALUES
(GETDATE(),1,3,1),(GETDATE(),2,4,1),(GETDATE(),1,3,2),(GETDATE(),2,4,2),(GETDATE(),1,3,3),
(GETDATE(),2,4,3),(GETDATE(),1,3,4),(GETDATE(),2,4,4),(GETDATE(),1,3,5),(GETDATE(),2,4,5),
(GETDATE(),1,3,1),(GETDATE(),2,4,2),(GETDATE(),1,3,3),(GETDATE(),2,4,4),(GETDATE(),1,3,5);

-- Itens de Pedido
INSERT INTO ItensPedido (Quantidade, PrecoUnitario, ProdutoId, PedidoId)
VALUES
(2,120.00,1,1),(1,25.00,2,1),(3,50.00,3,2),(1,30.00,4,2),(2,40.00,5,3),
(1,120.00,1,4),(2,25.00,2,5),(1,50.00,3,6),(1,30.00,4,7),(3,40.00,5,8),
(2,120.00,1,9),(1,25.00,2,10),(3,50.00,3,11),(1,30.00,4,12),(2,40.00,5,13);

-- Pagamentos
INSERT INTO Pagamentos (Data, Valor, StatusPagamentoId, TipoPagamentoId, PedidoId)
VALUES
(GETDATE(),265.00,1,1,1),(GETDATE(),150.00,2,2,2),(GETDATE(),190.00,1,3,3),
(GETDATE(),120.00,2,4,4),(GETDATE(),200.00,1,5,5),(GETDATE(),140.00,2,1,6),
(GETDATE(),180.00,1,2,7),(GETDATE(),130.00,2,3,8),(GETDATE(),160.00,1,4,9),
(GETDATE(),150.00,2,5,10),(GETDATE(),170.00,1,1,11),(GETDATE(),140.00,2,2,12),
(GETDATE(),190.00,1,3,13),(GETDATE(),120.00,2,4,14),(GETDATE(),200.00,1,5,15);

-- Agendamentos
INSERT INTO Agendamentos (DataAgendamento, DataCadastro, PedidoId, IdStatusAgendamento, StatusAgendamentoId)
VALUES
(DATEADD(day,1,GETDATE()),GETDATE(),1,1,1),
(DATEADD(day,2,GETDATE()),GETDATE(),2,2,2),
(DATEADD(day,3,GETDATE()),GETDATE(),3,3,3),
(DATEADD(day,4,GETDATE()),GETDATE(),4,4,4),
(DATEADD(day,5,GETDATE()),GETDATE(),5,5,5);

GO