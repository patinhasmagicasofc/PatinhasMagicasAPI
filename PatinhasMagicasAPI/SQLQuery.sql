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

-- ============================================
-- TABELAS FIXAS
-- ============================================

-- Tipos de Usuário
INSERT INTO TiposUsuario(Nome) VALUES
('Administrador'),
('Funcionário'),
('Cliente');

-- TamanhoAnimal
INSERT INTO TamanhoAnimal (Nome) VALUES
('Pequeno'),
('Médio'),
('Grande');

-- Especies
INSERT INTO Especies (Nome) VALUES
('Cachorro'),
('Gato'),
('Coelho'),
('Pássaro'),
('Hamster'),
('Porquinho-da-Índia'),
('Tartaruga'),
('Peixe'),
('Calopsita'),
('Furão');

-- Categorias
INSERT INTO Categorias (Nome) VALUES
('Ração'),
('Brinquedos'),
('Acessórios'),
('Medicamentos');

-- TipoServicos
INSERT INTO TipoServico(Nome) VALUES
('Banho'),
('Tosa'),
('Consulta Veterinária'),
('Vacinação'),
('Higiene e Cuidados'),
('Adestramento'),
('Transporte Pet');

-- TipoPagamento
INSERT INTO TipoPagamento(Nome) VALUES
('Cartão de Crédito'),
('Cartão de Débito'),
('PIX'),
('Dinheiro'),
('Boleto Bancário'),
('Transferência Bancária'),
('Carteira Digital');

-- StatusPagamento
INSERT INTO StatusPagamento (Nome) VALUES
('Pendente'),
('Aprovado'),
('Recusado'),
('Em Processamento'),
('Estornado'),
('Cancelado'),
('Concluído');

-- StatusPedido
INSERT INTO StatusPedido (Nome) VALUES
('Pendente'),
('Aguardando Pagamento'),
('Pagamento Aprovado'),
('Em Preparação'),
('Enviado'),
('Concluído'),
('Cancelado'),
('Devolvido');

-- StatusAgendamento
INSERT INTO StatusAgendamento (Nome) VALUES
('Pendente'),
('Confirmado'),
('Em Andamento'),
('Concluído'),
('Cancelado pelo Cliente'),
('Cancelado pela Loja'),
('Remarcado'),
('Não Compareceu');


-- ============================================
-- Produtos
-- ============================================
INSERT INTO Produtos 
(Nome, EspecieId, Preco, Marca, UrlImagem, Codigo, Descricao, DescricaoDetalhada, Validade, CategoriaId)
VALUES
('Ração Premium para Cães Adultos 10kg', 1, 179.90, 'Golden', 'https://example.com/produtos/racao-golden-cachorro.jpg', 'PROD001',
 'Ração completa para cães adultos de médio e grande porte.',
 'Ração balanceada com proteínas de alta qualidade e ômega 3 e 6 para pelagem saudável.', '2026-05-15', 1),
('Ração Seca para Gatos Filhotes 1,5kg', 2, 69.90, 'Whiskas', 'https://example.com/produtos/racao-whiskas-filhote.jpg', 'PROD002',
 'Ração completa para gatos filhotes até 12 meses.',
 'Enriquecida com cálcio e vitaminas para o crescimento saudável do seu gato.', '2026-02-10', 1),
('Ração Especial para Coelhos Adultos 2kg', 3, 42.90, 'NutriPet', 'https://example.com/produtos/racao-coelho.jpg', 'PROD003',
 'Ração rica em fibras e nutrientes essenciais.',
 'Ajuda na digestão e mantém o pelo macio e saudável.', '2026-07-20', 1),
('Brinquedo Interativo para Calopsitas com Espelho', 9, 24.50, 'AviToy', 'https://example.com/produtos/brinquedo-calopsita.jpg', 'PROD004',
 'Brinquedo com espelho e sininho para estimular as aves.',
 'Ajuda a reduzir o estresse e promove o entretenimento da calopsita.', NULL, 2),
('Ração Color Flakes para Peixes Ornamentais 250g', 8, 22.90, 'Alcon', 'https://example.com/produtos/racao-peixe.jpg', 'PROD005',
 'Ração em flocos para peixes tropicais e ornamentais.',
 'Realça a coloração natural e fortalece o sistema imunológico.', '2027-01-01', 1),
('Comedouro Duplo Inox para Cães', 1, 49.90, 'PetInox', 'https://example.com/produtos/comedouro-inox.jpg', 'PROD006',
 'Comedouro duplo em aço inox resistente.',
 'Ideal para água e ração, com base antiderrapante.', NULL, 3),
('Coleira Ajustável para Gatos com Guizo', 2, 27.90, 'CatStyle', 'https://example.com/produtos/coleira-gato.jpg', 'PROD007',
 'Coleira com guizo ajustável e segura.',
 'Disponível em várias cores e tamanhos.', NULL, 3),
('Brinquedo Mordedor para Cães Filhotes', 1, 19.90, 'DogToy', 'https://example.com/produtos/mordedor-cachorro.jpg', 'PROD008',
 'Brinquedo de borracha macia ideal para filhotes.',
 'Ajuda na dentição e evita que o cão roa móveis.', NULL, 2),
('Suplemento Vitamínico para Hamsters 30ml', 5, 31.90, 'RodentCare', 'https://example.com/produtos/suplemento-hamster.jpg', 'PROD009',
 'Complexo vitamínico líquido para hamsters.',
 'Fortalece o sistema imunológico e melhora a vitalidade.', '2026-09-30', 4),
('Pomada Cicatrizante para Cães e Gatos 50g', 1, 29.50, 'VetPlus', 'https://example.com/produtos/pomada-cicatrizante.jpg', 'PROD010',
 'Pomada para uso tópico em ferimentos leves.',
 'Acelera o processo de cicatrização e possui ação antibacteriana.', '2026-12-12', 4);

-- ============================================
-- Usuarios
-- ============================================
INSERT INTO Usuarios 
(Nome, CPF, Email, Senha, Ddd, Telefone, DataCadastro, Ativo, TipoUsuarioId)
VALUES
('Mariana Souza', '123.456.789-00', 'mariana.souza@gmail.com', 'Senha@123', 11, '988776655', '2024-12-05', 1, 3),
('Ricardo Lima', '987.654.321-00', 'ricardo.lima@yahoo.com', 'Senha@123', 21, '987654321', '2025-01-10', 1, 3),
('Fernanda Oliveira', '321.654.987-11', 'fernanda.oli@hotmail.com', 'Senha@123', 31, '991234567', '2025-02-20', 1, 3),
('Lucas Andrade', '654.987.321-22', 'lucas.andrade@gmail.com', 'Senha@123', 47, '999112233', '2025-03-15', 1, 3),
('Patrícia Gomes', '741.852.963-33', 'patricia.gomes@gmail.com', 'Senha@123', 19, '981223344', '2025-04-08', 1, 3),
('Camila Torres', '852.963.741-44', 'camila.torres@patinhas.com', 'Adm@1234', 11, '999887766', '2024-11-12', 1, 2),
('Gustavo Ribeiro', '963.258.741-55', 'gustavo.ribeiro@patinhas.com', 'Adm@1234', 11, '977665544', '2024-10-20', 1, 2),
('Administrador do Sistema', '111.222.333-44', 'admin@patinhas.com', 'Root@2024', 11, '900000000', '2024-01-01', 1, 1);

-- ============================================
-- Animais
-- ============================================
INSERT INTO Animais 
(Nome, Raca, Idade, EspecieId, TamanhoAnimalId, UsuarioId)
VALUES
('Rex', 'Labrador', 3, 1, 3, 1),
('Mimi', 'Siamês', 2, 2, 1, 2),
('Bilu', 'Coelho Holandês', 1, 3, 1, 3),
('Pipoca', 'Calopsita', 2, 9, 1, 4),
('Nemo', 'Peixe Beta', 1, 8, 1, 5),
('Thor', 'Golden Retriever', 4, 1, 3, 1),
('Luna', 'Persa', 3, 2, 2, 2),
('Fifi', 'Porquinho-da-Índia', 1, 6, 1, 3),
('Spike', 'Pitbull', 5, 1, 3, 6),
('Tortuga', 'Tartaruga', 10, 7, 2, 7);

-- ============================================
-- Servicos
-- ============================================
INSERT INTO Servicos
(Nome, TipoServicoId, Descricao)
VALUES
('Banho Simples', 1, 'Banho básico para pets de pequeno a grande porte.'),
('Tosa Higiênica', 2, 'Tosa higiênica e aparo de pelos em regiões específicas.'),
('Consulta Veterinária Rotina', 3, 'Consulta de rotina para avaliação da saúde do animal.'),
('Vacinação Anual', 4, 'Vacinas obrigatórias e preventivas.'),
('Higiene Bucal', 5, 'Escovação e cuidados dentários.'),
('Adestramento Básico', 6, 'Treinamento de comandos básicos e comportamento.'),
('Transporte Seguro', 7, 'Transporte do animal com segurança e conforto.');

-- ============================================
-- Pedidos
-- ============================================
INSERT INTO Pedidos
(DataPedido, UsuarioId, StatusPedidoId)
VALUES
('2025-10-15 10:00', 1, 1),
('2025-10-16 11:30', 2, 2),
('2025-10-17 14:00', 3, 3),
('2025-10-18 09:15', 4, 4),
('2025-10-19 16:00', 5, 5),
('2025-10-20 13:30', 1, 1),
('2025-10-21 15:00', 2, 2),
('2025-10-22 10:45', 3, 3),
('2025-10-23 12:30', 4, 4),
('2025-10-24 14:15', 5, 5);

-- ============================================
-- Agendamentos
-- ============================================
INSERT INTO Agendamentos
(DataAgendamento, DataCadastro, PedidoId, AnimalId, StatusAgendamentoId)
VALUES
('2025-10-25 10:00', '2025-10-15', 1, 1, 2),
('2025-10-26 14:30', '2025-10-16', 2, 2, 1),
('2025-10-27 09:00', '2025-10-17', 3, 3, 2),
('2025-10-28 15:00', '2025-10-18', 4, 4, 1),
('2025-10-29 11:30', '2025-10-19', 5, 5, 2),
('2025-11-01 13:00', '2025-10-20', 6, 6, 3),
('2025-11-02 10:30', '2025-10-21', 7, 7, 2),
('2025-11-03 16:00', '2025-10-22', 8, 8, 1),
('2025-11-04 09:30', '2025-10-23', 9, 9, 2),
('2025-11-05 14:00', '2025-10-24', 10, 10, 3);

-- ============================================
-- AgendamentoServico
-- ============================================
INSERT INTO AgendamentoServico
(Preco, AgendamentoId, ServicoId)
VALUES
(60.00, 1, 1),
(90.00, 2, 2),
(45.00, 3, 3),
(30.00, 4, 4),
(20.00, 5, 5),
(20.00, 6, 7),
(15.00, 7, 7),
(60.00, 8, 6),
(40.00, 9, 7),
(90.00, 10, 2);

-- ============================================
-- Enderecos
-- ============================================
INSERT INTO Enderecos
(Logradouro, Numero, Bairro, Cidade, Estado, CEP, Complemento, UsuarioId)
VALUES
('Rua das Flores', 123, 'Jardim Primavera', 'São Paulo', 'SP', '01234-567', 'Apto 101', 1),
('Avenida Central', 456, 'Centro', 'Rio de Janeiro', 'RJ', '20010-890', 'Sala 202', 2),
('Rua do Sol', 78, 'Vila Nova', 'Belo Horizonte', 'MG', '30123-456', NULL, 3),
('Rua das Acácias', 150, 'Boa Vista', 'Curitiba', 'PR', '80015-789', 'Casa B', 4),
('Alameda Santos', 321, 'Jardim Paulista', 'São Paulo', 'SP', '01412-345', 'Cobertura', 5),
('Rua das Palmeiras', 87, 'Centro', 'Fortaleza', 'CE', '60020-123', NULL, 6),
('Avenida Brasil', 500, 'Centro', 'Porto Alegre', 'RS', '90040-567', 'Sala 3', 7),
('Rua do Comércio', 250, 'São José', 'Florianópolis', 'SC', '88015-789', 'Loja 5', 8);

-- ============================================
-- ItensPedido
-- ============================================
INSERT INTO ItensPedido
(Quantidade, PrecoUnitario, ProdutoId, PedidoId)
VALUES
(2, 120.00, 1, 1),
(1, 85.50, 2, 1),
(3, 45.00, 3, 2),
(1, 60.00, 4, 2),
(2, 30.00, 5, 3),
(1, 95.00, 6, 3),
(1, 150.00, 7, 4),
(2, 40.00, 8, 4),
(1, 70.00, 9, 5),
(3, 25.00, 10, 5),
(1, 55.00, 10, 6),
(2, 65.00, 9, 6),
(1, 80.00, 8, 7),
(1, 90.00, 7, 7);

-- ============================================
-- Pagamentos
-- ============================================
INSERT INTO Pagamentos
(DataPagamento, Valor, Observacao, StatusPagamentoId, TipoPagamentoId, PedidoId)
VALUES
('2025-10-15 10:30', 205.50, 'Pagamento realizado via cartão', 2, 1, 1),
('2025-10-16 12:00', 105.00, 'Pagamento via PIX', 2, 3, 2),
('2025-10-17 14:30', 125.00, 'Pagamento parcial', 1, 2, 3),
('2025-10-18 09:45', 190.00, 'Pagamento aprovado', 2, 1, 4),
('2025-10-19 16:30', 145.00, 'Pagamento via dinheiro', 2, 4, 5),
('2025-10-20 14:00', 120.00, 'Pagamento pendente', 1, 3, 6),
('2025-10-21 15:15', 170.00, 'Pagamento aprovado via cartão', 2, 1, 7),
('2025-10-22 11:00', 100.00, 'Pagamento via PIX', 2, 3, 8),
('2025-10-23 12:45', 165.00, 'Pagamento aprovado', 2, 2, 9),
('2025-10-24 14:30', 135.00, 'Pagamento via dinheiro', 2, 4, 10);

-- ============================================
-- ServicoTamanho
-- ============================================
INSERT INTO ServicoTamanho
(Preco, ServicoId, TamanhoAnimalId)
VALUES
(50.00, 1, 1),
(70.00, 1, 2),
(90.00, 1, 3),
(80.00, 2, 1),
(100.00, 2, 2),
(120.00, 2, 3),
(30.00, 3, 1),
(50.00, 3, 2),
(70.00, 3, 3),
(60.00, 4, 1),
(60.00, 4, 2),
(60.00, 4, 3),
(25.00, 5, 1),
(25.00, 5, 2),
(25.00, 5, 3),
(20.00, 6, 1),
(30.00, 6, 2),
(40.00, 6, 3),
(15.00, 7, 1),
(25.00, 7, 2),
(35.00, 7, 3);

