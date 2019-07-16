USE master
GO

IF EXISTS(SELECT * FROM sys.databases WHERE NAME = 'StatueStore')
	DROP DATABASE StatueStore
GO

CREATE DATABASE StatueStore
GO

USE StatueStore
GO

CREATE TABLE Envio (
	idEnvio INT PRIMARY KEY IDENTITY NOT NULL,
	frete MONEY NULL,
	meio VARCHAR(20) NULL,
	dataEnvio DATETIME NULL,
	dataEntrega DATETIME NULL,
	dataPrevisao DATE NULL
)
GO

CREATE TABLE TipoPgto (
	idTipoPgto INT PRIMARY KEY IDENTITY NOT NULL,
	nomeTipo VARCHAR(45) UNIQUE NULL,
	obs VARCHAR(100) NULL,
)
GO

CREATE TABLE StatusPedido (
	idStatusPedido INT PRIMARY KEY IDENTITY NOT NULL,
	nomeStatus VARCHAR(20) UNIQUE NULL,
	descricao VARCHAR(100) NULL 
)
GO

CREATE TABLE Cliente (
	idCliente INT PRIMARY KEY IDENTITY NOT NULL,
	email VARCHAR(45) UNIQUE NULL,
	senha VARCHAR(45) NULL,
	nome VARCHAR(45) NULL,
	sobrenome VARCHAR(45) NULL,
	sexo CHAR(1) NULL,
	cpf VARCHAR(11) NULL,
	dataNasc DATE NULL, 
	dataInsc DATE NULL
)
GO

CREATE TABLE CartaoCredito (
	idCartao INT PRIMARY KEY IDENTITY NOT NULL,
	bandeira VARCHAR(20) NULL,
	numCartao VARCHAR(20) NOT NULL,
	cvv VARCHAR(4) NULL,
	validade DATE NULL,
	idCliente INT FOREIGN KEY REFERENCES Cliente(idCliente) NULL,
)
GO

CREATE TABLE Endereco (
	idEndereco INT PRIMARY KEY IDENTITY NOT NULL,
	cep VARCHAR(8) NULL,
	pais VARCHAR(20) NULL,
	estado VARCHAR(20) NULL,
	cidade VARCHAR(45) NULL,
	bairro VARCHAR(45) NULL,
	logradouro VARCHAR(100) NULL,
	tipoLogradouro VARCHAR(45) NULL,
	numero INT NULL,
	complementoEnd VARCHAR(100) NULL,
)
GO

CREATE TABLE Endereco_Cliente (
	idEndereco_Cliente INT PRIMARY KEY IDENTITY NOT NULL,
	idCliente INT FOREIGN KEY REFERENCES Cliente(idCliente) NOT NULL,
	idEndereco INT FOREIGN KEY REFERENCES Endereco(idEndereco) NOT NULL
)
GO

CREATE TABLE Pedido (
	idPedido INT PRIMARY KEY IDENTITY NOT NULL,
	dataPedido DATETIME NULL,
	idEnvio INT FOREIGN KEY REFERENCES Envio(idEnvio) NULL,
	idTipoPgto INT FOREIGN KEY REFERENCES TipoPgto(idTipoPgto) NULL,
	idCliente INT FOREIGN KEY REFERENCES Cliente(idCliente) NULL,
	idEndereco_Cliente INT FOREIGN KEY REFERENCES Endereco_Cliente(idEndereco_Cliente) NULL,
	idCartaoCredito INT FOREIGN KEY REFERENCES CartaoCredito(idCartao) NULL, --Em caso de pagamento em cartão
	pagamentoAtivo TINYINT NULL --Em caso de pagamento em boleto
)
GO

CREATE TABLE NivelAcesso (
	idNivelAcesso INT PRIMARY KEY IDENTITY NOT NULL,
	nomeNivel VARCHAR(20) UNIQUE NULL,
	descricao VARCHAR(200) NULL	
)
GO

CREATE TABLE Funcionario (
	idFuncionario INT PRIMARY KEY IDENTITY NOT NULL,
	cpf VARCHAR(12) UNIQUE NULL,
	nome VARCHAR(45) NULL,
	sobrenome VARCHAR(45) NULL,
	sexo CHAR(1) NULL,
	email VARCHAR(50) NULL,
	senha VARCHAR(20) NULL,
	funcao VARCHAR(45) NULL,
	dataAdmissao DATE NULL,
	dataDemissao DATE NULL,
	valorHora MONEY NULL,
	regimento VARCHAR(15) NULL,
	obs VARCHAR(100) NULL,
	logradouro VARCHAR(100) NULL,
	bairro VARCHAR(20) NULL,
	cidade VARCHAR(45) NULL,
	estado VARCHAR(20) NULL,
	numeroCasa INT NULL,
	cep VARCHAR(8) NULL,
	complementoEnd VARCHAR(100) NULL,
	idNivelAcesso INT FOREIGN KEY REFERENCES NivelAcesso(idNivelAcesso) NULL,
	idFunCad INT FOREIGN KEY REFERENCES Funcionario(idFuncionario) NULL
)
GO

CREATE TABLE Logs (
	idLog INT PRIMARY KEY IDENTITY NOT NULL,
	detalhe VARCHAR(200) NULL,
	dataUltAlt DATETIME NULL,
	areaAlt VARCHAR(45) NULL,
	idFuncionario INT FOREIGN KEY REFERENCES Funcionario(idFuncionario) NULL
)
GO

CREATE TABLE Fornecedor (
	idFornecedor INT PRIMARY KEY IDENTITY NOT NULL,
	razaoSocial VARCHAR(90) NULL,
	nomeFantasia VARCHAR(70) NULL,
	email VARCHAR(40) NULL,
	cnpj VARCHAR(18) NULL,
	ie VARCHAR(12) NULL,
	telefone VARCHAR(20) NULL,
	telefone2 VARCHAR(20) NULL,
	representante VARCHAR(45) NULL,
	observacao VARCHAR(100) NULL,
	dataCad DATE NULL,
	idEndereco INT FOREIGN KEY REFERENCES Endereco(idEndereco) NULL,
	idFunCad INT FOREIGN KEY REFERENCES Funcionario(idFuncionario) NULL
)
GO

CREATE TABLE Grupo (
	idGrupo INT PRIMARY KEY IDENTITY NOT NULL,
	nomeGrupo VARCHAR(45) NULL,
	descricao VARCHAR(200) NULL,
	observacao VARCHAR(100) NULL
)
GO

CREATE TABLE Subgrupo (
	idSubgrupo INT PRIMARY KEY IDENTITY NOT NULL,
	nomeSubgrupo VARCHAR(45) NULL,
	descricao VARCHAR(200) NULL,
	observacao VARCHAR(100) NULL,
	idGrupo INT FOREIGN KEY REFERENCES Grupo(idGrupo) NULL,
)
GO

CREATE TABLE Produto (
	idProduto INT PRIMARY KEY IDENTITY NOT NULL,
	nome VARCHAR(45) NULL,
	imagem varbinary(max),
	precoCusto MONEY NULL,
	precoVenda MONEY NULL,
	descricao VARCHAR(200) NULL,
	descricaoRed VARCHAR(100) NULL,
	modelo VARCHAR(100) NULL,
	marca VARCHAR(100) NULL,
	sexo CHAR(1) NULL,
	tamanho VARCHAR(100) NULL,
	quantidade INT NULL,
	qtdMinina INT NULL,
	dataCad DATE NULL,
	idSubgrupo INT FOREIGN KEY REFERENCES Subgrupo(idSubgrupo) NULL,
	idFunCad INT FOREIGN KEY REFERENCES Funcionario(idFuncionario) NULL
)
GO

CREATE TABLE Fornecedor_Produto (
	idFornecedor_Produto INT PRIMARY KEY IDENTITY NOT NULL,
	detalhe VARCHAR(100) NULL,
	precoUni MONEY NULL,
	dataPedido DATE NULL,
	quantidade INT NULL,
	idFornecedor INT FOREIGN KEY REFERENCES Fornecedor(idFornecedor) NULL,
	idProduto INT FOREIGN KEY REFERENCES Produto(idProduto) NULL,
)
GO

CREATE TABLE Detalhe_Pedido (
	idDetalhe_Pedido INT PRIMARY KEY IDENTITY NOT NULL,
	quantidade INT NULL,
	precoUni MONEY NULL,
	idProduto INT FOREIGN KEY REFERENCES Produto(idProduto) NULL,
	idPedido INT FOREIGN KEY REFERENCES Pedido(idPedido) NULL
)
GO

-- Criando indexes das tabelas

CREATE INDEX iNivelAcesso ON NivelAcesso(idNivelAcesso) 
GO

CREATE INDEX iLogs ON Logs(idLog) 
GO

CREATE INDEX iFuncionario ON Funcionario(idFuncionario)
GO

CREATE INDEX iEndereco ON Endereco(idEndereco) 
GO

CREATE INDEX iFornecedor ON Fornecedor(idFornecedor) 
GO

CREATE INDEX iGrupo ON Grupo(idGrupo) 
GO

CREATE INDEX iSubgrupo ON Subgrupo(idSubgrupo) 
GO

CREATE INDEX iProduto ON Produto(idProduto) 
GO

CREATE INDEX iFornecedor_Produto ON Fornecedor_Produto(idFornecedor_Produto)
GO

CREATE INDEX iCliente ON Cliente(idCliente) 
GO

CREATE INDEX iEndereco_Cliente ON Endereco_Cliente(idEndereco_Cliente)
GO

CREATE INDEX iPedido ON Pedido(idPedido)
GO

CREATE INDEX iDetalhe_Pedido ON Detalhe_Pedido(idDetalhe_Pedido) 
GO

CREATE INDEX iEnvio ON Envio(idEnvio)
GO

CREATE INDEX iStatusPedido ON StatusPedido(idStatusPedido) 
GO

CREATE INDEX iTipoPgto ON TipoPgto(idTipoPgto) 
GO

CREATE INDEX iCartaoCredito ON CartaoCredito(idCartao)
GO

-- Adicionando registros globais

INSERT TipoPgto VALUES
	('Boleto', null),
	('Cartão de Crédito', null),
	('PayPal', null),
	('PagSeguro', null)
GO

INSERT StatusPedido VALUES
	('Aguardando Pagamento', null),
	('Cancelado', null),
	('Pendente', null),
	('Empacotado', null),
	('Enviado', null),
	('Entregue', null),
	('Retornado', null)
GO

INSERT NivelAcesso VALUES
	('Sem Acesso', 'Funcionário sem acesso ao Software da loja'),
	('Básico', 'Pode visualizar as telas de Acompanhamento de Pedido e Estoque (e exigir encomendas de estoque para fornecedores) + Telas de Visualização de Cadastros (Produtos, Fornecedores e Clientes).'),
	('Administrador', 'Tem acesso as mesmas telas dos funcionários de nível básico + telas de cadastro de Produto e Fornecedor) + Alteração dos Cadastros de Produtos, Fornecedores e Clientes.'),
	('Gerente', 'Tem acesso as mesmas telas dos funcionários de nível Administrador + Telas de cadastro de Funcionários + Tela de visualização/alteração de funcionários cadastrados.')
GO

INSERT Grupo VALUES 
	('Indefinido', null, null),
	('Camisetas', null, null),
	('Moletons', null, null),
	('Calças', null, null),
	('Acessórios', null, null)
GO

INSERT Subgrupo VALUES 
	('Indefinido', null, null, 1),
	('Manga Longa', null, null, 2),
	('Manga Curta', null, null, 2),
	('Com touca', null, null, 3),
	('Sem touca', null, null, 3),
	('Jeans', null, null, 4),
	('De moletom', null, null, 4),
	('Bonés', null, null, 5),
	('Pulseiras', null, null, 5)
GO

-- Adicionando 'Gerente Raiz' (Primeiro funcionário que irá cadastrar os outros funcionários)

INSERT Funcionario VALUES ('12345678999', 'Gustavo', 'de Oliveira Soares Silva', 'M', 'gustavopatricio11@gmail.com', '12345', 'Gerente Raiz', '25-10-2018', null, 1500, null, null, null, null, null, null, null, null, null, 4, null)
GO

INSERT INTO PRODUTO(nome, quantidade, qtdMinina) values ('Calça', 50, 35)
/*

GO

SELECT * FROM Funcionario
	WHERE idFuncionario = 1;*/

/*SELECT * FROM Grupo

select * from Logs

select * from Fornecedor

select * from Endereco

SELECT * FROM Produto

SELECT * FROM Fornecedor_Produto

SELECT * FROM NivelAcesso
*/
