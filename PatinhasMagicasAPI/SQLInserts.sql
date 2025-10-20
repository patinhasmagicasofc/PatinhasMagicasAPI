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
