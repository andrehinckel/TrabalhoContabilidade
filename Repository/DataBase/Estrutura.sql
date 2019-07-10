
CREATE TABLE contabilidades(
	id INT PRIMARY KEY IDENTITY(1,1),
	nome VARCHAR(45)
);
CREATE TABLE clientes(
	id INT PRIMARY KEY IDENTITY(1,1),
	id_contabilidade INT NOT NULL,
	nome VARCHAR(45),
	cpf VARCHAR(14)
);
CREATE TABLE usuarios(
	id INT PRIMARY KEY IDENTITY(1,1),
	login VARCHAR(45),
	senha VARCHAR(45),
	data_nascimento DATETIME2,
	id_contabilidade INT
);

CREATE TABLE categorias(
	id INT PRIMARY KEY IDENTITY(1,1),
	nome VARCHAR(45)
);
DROP TABLE contas_pagar;
CREATE TABLE contas_pagar(
	id INT PRIMARY KEY IDENTITY(1,1),
	id_cliente INT,
	id_categoria INT,
	nome VARCHAR(45),
	data_vencimento DATETIME2,
	data_pagamento DATETIME2,
	valor DECIMAL(12,2)
);
DROP TABLE contas_receber;
CREATE TABLE contas_receber(
	id INT PRIMARY KEY IDENTITY(1,1),
	id_cliente INT,
	id_categoria INT,
	nome VARCHAR(45),
	data_pagamento DATETIME2,
	valor DECIMAL(12,2)
);
DROP TABLE cartoes_credito;
CREATE TABLE cartoes_credito(
	id INT PRIMARY KEY IDENTITY(1,1),
	id_cliente INT,
	numero VARCHAR(45),
	data_vencimento DATETIME2,
	cvv VARCHAR(3)
);
DROP TABLE compras;
CREATE TABLE compras(
	id INT PRIMARY KEY IDENTITY(1,1),
	id_cartao_credito INT,
	valor DECIMAL(12,2),
	data_compra DATETIME2
);


