create database loja_online;

use loja_online;

create table usuario(
	id int not null auto_increment primary key,
    nome varchar(100) not null,
    email varchar(100) not null,
    senha varchar(25) not null,
    foto varchar(100),
    pontos int not null default 0,
    criado_em datetime not null,
    tipo varchar(10) not null default "user"
);

create table categoria(
	id int not null auto_increment primary key,
    nome varchar(100) not null
);

create table jogo(
	id int not null auto_increment primary key,
    nome varchar(100) not null,
    descricao varchar(255) not null,
    valor float not null,
    capa varchar(100) not null,
    banner varchar(100) not null,
    criado_em datetime not null,
	categoria_id int not null,
    foreign key(categoria_id) references categoria(id)
);

create table compra(
	id int not null auto_increment primary key,
    valor_total float not null,
    criado_em datetime not null,
    usuario_id int not null,
    foreign key(usuario_id) references usuario(id)
);

create table pagamento(
	id int not null auto_increment primary key,
    metodo varchar(100) not null,
	pago_em datetime not null,
    compra_id int unique,
    foreign key(compra_id) references compra(id)
);

create table carrinho(
	id int not null auto_increment primary key,
    usuario_id int unique,
    foreign key(usuario_id) references usuario(id)
);

create table usuario_jogo(
	usuario_id int not null,
    jogo_id int not null,
    chave_ativacao varchar(100) not null,
    adquirido_em datetime not null,
    
    primary key(usuario_id, jogo_id),
    
    FOREIGN KEY (usuario_id) REFERENCES usuario(id),
    FOREIGN KEY (jogo_id) REFERENCES jogo(id)
);

create table carrinho_jogo(
	carrinho_id int not null,
    jogo_id int not null,
    qtd int not null,
    
    primary key(carrinho_id, jogo_id),
    
    foreign key(carrinho_id) references carrinho(id),
    foreign key(jogo_id) references jogo(id)
);

create table compra_jogo(
	compra_id int not null,
    jogo_id int not null,
    preco_pago float not null,
    
    primary key(jogo_id, compra_id),
    
    foreign key(compra_id) references compra(id),
    foreign key(jogo_id) references jogo(id)
);

