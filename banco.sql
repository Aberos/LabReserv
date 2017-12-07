create database labreserve
go

use labreserve
go


 
create table pessoas
(
    id        int          not null  primary key identity,
    estatus   int,
    email     varchar(250) not null  unique,
    nome      varchar(50)  not null,
    sobrenome varchar(50)  not null,
    cel       varchar(20)  null,
    senha     varchar(50)  not null,
    check (estatus in (1,2,3,4))
)
go
 
create table admins
(
    id_pessoa   int not null primary key references pessoas,
    permissao    int
)
go
 
create table professores
(
    id_pessoa   int not null primary key references pessoas,
    permissao    int
)
go
 
create table salas
(
    id      int not null     primary key identity,
    nome    varchar(50)  not null
)
go
 
create table cursos
(
    id      int not null     primary key identity,
    nome    varchar(50)  not null
)
go
 
create table turmas
(
    id      int not null     primary key identity,
    nome    varchar(50)  not null,
    id_curso   int not null  references cursos
)
go
 
 
create table turma_professor
(
    id_professor    int not null references professores,
    id_turma    int not null references turmas,
    status      int not null,
    primary key(id_professor, id_turma)
)
go
 
create table reservas
(
    id      int not null     primary key identity,
    dia     varchar(25)  not null,
    turno    int not null,
	bloco	  int not null,
    id_sala   int not null references salas,
    id_admin   int not null  references admins,
    id_professor   int not null  references professores,
    id_turma   int not null  references turmas,
    estatus   int,
    check (estatus in (1,2,3,4))
)
go
 
create view v_turmas
as
select t.id             idTurma,
       t.nome           nomeTurma,
       c.id             idCurso,
       c.nome           nomeCurso
 
from turmas t, cursos c
where t.id_curso = c.id
Go
 
 
Create Procedure CadAdmin
(
    @estatus     int,
    @email       varchar(250),
    @nome        varchar(50),
    @sobreNome   varchar(50),
    @cel         varchar(20),
    @senha       varchar(50)
)
as
begin
    insert into pessoas values (@estatus, @email, @nome, @sobreNome, @cel, @senha)
    insert into admins values(@@IDENTITY, 1)
end
Go
 
create view v_admin
as
select p.id             idAdmin,
       p.nome           Nome,
       p.sobrenome      Sobrenome,
       p.email          Email,
       p.senha          Senha,
       p.cel            Celular,
       p.estatus        Status,
       a.permissao      Permissao
 
from pessoas p, admins a
where p.id = a.id_pessoa
Go
 
 
create procedure LogarAdmins
(
    @email varchar(250),
    @senha varchar(50)
)
as
begin
    select * from v_admin where Email = @email and Senha = @senha and Status = 1
end
go
 
Create Procedure CadProfessor
(
    @estatus     int,
    @email       varchar(250),
    @nome        varchar(50),
    @sobreNome   varchar(50),
    @cel         varchar(20),
    @senha       varchar(50)
)
as
begin
    insert into pessoas values (@estatus, @email, @nome, @sobreNome, @cel, @senha)
    insert into professores values(@@IDENTITY, 1)
end
Go
 
create view v_professores
as
select p.id             idProfessor,
       p.nome           Nome,
       p.sobrenome      Sobrenome,
       p.email          Email,
       p.senha          Senha,
       p.cel            Celular,
       p.estatus        Status,
       e.permissao      Permissao
 
from pessoas p, professores e
where p.id = e.id_pessoa
Go
 
 
create procedure LogarProfessor
(
    @email varchar(250),
    @senha varchar(50)
)
as
begin
    select * from v_professores where Email = @email and Senha = @senha and Status = 1
end
go
 
 
Create Procedure AddProfessorTurma
(
    @idProfessor     int,
    @idTurma         int
)   
as
begin
    insert into turma_professor values (@idProfessor, @idTurma, 1)
end
Go
 
Create Procedure AddReserva
(
    @dia                varchar(25),
    @turno              int,
	@bloco              int,
    @idSala             int,
    @idAdmin            int,
    @idProfessor        int,
    @idTurma            int
)   
as
begin
    insert into reservas values (@dia, @turno, @bloco, @idSala, @idAdmin, @idProfessor, @idTurma, 1)
end
Go
 
 
create view v_reservas
as
select  r.id            idReserva,
        r.dia           Dia,
        r.turno         Turno,
		r.bloco			Bloco,
        r.estatus       Estatus,
        s.id            idSala,
        s.nome          nomeSala,
        a.idAdmin       idAdmin,
        a.Nome          nomeAdmin,
        p.idProfessor   idProfessor,
        p.Nome          nomeProfessor,
        t.idTurma       idTurma,
        t.nomeTurma     nomeTurma,
        t.idCurso       idCurso,
        t.nomeCurso     nomeCurso
         
from reservas r, salas s,
    v_admin a, v_professores p, 
    v_turmas t
where   r.id_sala = s.id AND
        r.id_admin = a.idAdmin AND
        r.id_professor = p.idProfessor AND
        r.id_turma = t.idTurma
Go
 
Create Procedure UpdateReserva
(
    @idReserva          int,
    @dia                varchar(25),
    @turno              int,
	@bloco				int,
    @estatus            int,
    @idSala             int,
    @idAdmin            int,
    @idProfessor        int,
    @idTurma            int
)
as
begin  
    UPDATE reservas SET estatus = @estatus, dia = @dia, turno = @turno, bloco = @bloco, 
                       id_sala = @idSala, id_admin = @idAdmin, id_professor = @idProfessor, id_turma = @idTurma
                       WHERE id = @idReserva
end
go

create view v_turma_prof
as
select t.id             idTurma,
       t.nome           nomeTurma,
       c.id             idCurso,
       c.nome           nomeCurso,
	   a.id_professor   idProfessor,
	   a.status			Status		
 
from turma_professor a, turmas t, cursos c
where a.id_turma = t.id  and t.id_curso = c.id
Go

create table solicitacoes
(
	id int not null  primary key identity,
	idProfessor int not null references professores,
	idSala int not null references salas,
	idTurma int not null references turmas,
	dia int not null,
	turno int not null,
	bloco int not null,
	status int not null
)
go

create view v_solicitacao
as
select 
     s.id           idSolicitacao,
	 p.idProfessor  idProfessor,
	 p.Nome         nomeProfessor,
	 p.Sobrenome    sobrenomeProfessor,
	 a.id           idSala,
	 a.nome         nomeSala,
	 t.idTurma      idTurma,
	 t.nomeTurma    nomeTurma,
	 t.idCurso      idCurso,
	 t.nomeTurma    nomeCurso,
	 s.dia          Dia,
	 s.turno        Turno,
	 s.bloco        Bloco,
	 s.status       Status

from solicitacoes s, v_professores p,
	 salas a, v_turmas t
where s.idProfessor = p.idProfessor and
	  s.idSala = a.id and
	  s.idTurma = t.idTurma
go