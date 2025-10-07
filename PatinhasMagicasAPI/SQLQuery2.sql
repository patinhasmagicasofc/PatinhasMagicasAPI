USE [PatinhasMagicasDB]
GO

-- Tipos de Usu�rios
INSERT INTO TiposUsuarios (DescricaoTipoUsuario)
VALUES 
('Cliente'),
('Funcion�rio'),
('Administrador'),
('Veterin�rio'),
('Fornecedor');

-- Usu�rios
INSERT INTO Usuarios (Nome, CPF, Email, Ddd, Telefone, TipoUsuarioId, DataCadastro, Senha)
VALUES
('Jo�o Silva','111.111.111-11','joao@email.com',11,'999911111',1, GETDATE(), 'Jo�o Silva'),
('Maria Souza','222.222.222-22','maria@email.com',21,'988822222',1,GETDATE(), 'Jo�o Silva'),
('Carlos Lima','333.333.333-33','carlos@email.com',31,'977733333',2, GETDATE(), 'Jo�o Silva'),
('Ana Paula','444.444.444-44','ana@email.com',41,'966644444',2, GETDATE(), 'Jo�o Silva'),
('Lucas Santos','555.555.555-55','lucas@email.com',51,'955555555',3, GETDATE(), 'Jo�o Silva');

-- Endere�os
INSERT INTO Enderecos (Logradouro, Numero, Bairro, Cidade, Estado, CEP, UsuarioId)
VALUES
('Rua A',100,'Centro','S�o Paulo','SP','01001-000',1),
('Rua B',200,'Jardim','Rio de Janeiro','RJ','20020-000',2),
('Rua C',300,'Bela Vista','Belo Horizonte','MG','30100-000',3),
('Rua D',400,'Centro','Curitiba','PR','80010-000',4),
('Rua E',500,'Centro','Porto Alegre','RS','90010-000',5);

-- Categorias de Produtos
INSERT INTO Categorias (Nome)
VALUES ('Ra��o'),('Brinquedos'),('Medicamentos'),('Acess�rios'),('Higiene');

-- Produtos
INSERT INTO Produtos (Nome, Preco, Foto, Codigo, Validade, CategoriaId)
VALUES
('Ra��o Premium',120.00,'img1.jpg','RAC001','2026-12-31',1),
('Bola de Borracha',25.00,'img2.jpg','BRQ002','2027-01-15',2),
('Antipulgas',50.00,'img3.jpg','ANT003','2025-11-30',3),
('Coleira Pet',30.00,'img4.jpg','COL004','2028-03-10',4),
('Shampoo Pet',40.00,'img5.jpg','SHM005','2026-05-20',5);

-- Status de Pagamentos
INSERT INTO StatusPagamentos (Nome)
VALUES ('Pendente'),('Pago'),('Cancelado'),('Estornado'),('Aguardando');

-- Tipos de Pagamento
INSERT INTO TipoPagamentos (Metodo)
VALUES ('Cart�o de Cr�dito'),('Boleto'),('Pix'),('Dinheiro'),('Transfer�ncia');

-- Status de Pedidos
INSERT INTO StatusPedidos (Nome)
VALUES ('Pendente'),('Processando'),('Conclu�do'),('Cancelado'),('Entregue');

-- Status de Agendamentos
INSERT INTO StatusAgendamentos (Nome)
VALUES ('Agendado'),('Em andamento'),('Conclu�do'),('Cancelado'),('Aguardando');

-- Tipos de Servi�o
INSERT INTO TiposServico (Nome)
VALUES ('Banho'),('Tosa'),('Consulta Veterin�ria'),('Vacina��o'),('Hospedagem');

-- Servi�os
INSERT INTO Servicos (Nome, Descricao, Status, TipoServicoId)
VALUES
('Banho Completo','Banho e secagem','Ativo',1),
('Tosa Higi�nica','Apenas corte de pelos','Ativo',2),
('Consulta Geral','Checkup de sa�de','Ativo',3),
('Vacina Antirr�bica','Vacina anual','Ativo',4),
('Hospedagem Pet','Hospedagem por per�odo','Ativo',5);

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