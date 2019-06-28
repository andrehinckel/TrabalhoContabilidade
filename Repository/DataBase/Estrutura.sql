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

CREATE TABLE contas_pagar(
	id INT PRIMARY KEY IDENTITY(1,1),
);

CREATE TABLE contas_receber(
	id INT PRIMARY KEY IDENTITY(1,1)
);