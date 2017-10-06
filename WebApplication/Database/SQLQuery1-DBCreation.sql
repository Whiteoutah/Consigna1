create database Consigna1
go

use Consigna1
go

create table Direccion(
idDireccion	int identity(1,1) primary key,
calle		varchar(50),
numero		int
);
go

create table Persona(
idPersona		int identity(1,1) primary key,
nombre			varchar(50) not null,
apellido		varchar(50) not null,
numeroDocumento int unique,
fechaNacimiento	date,
direccion		int,
foreign key (direccion) references Direccion (idDireccion),
constraint	ckc_fechaNacimiento_notFuture CHECK (fechaNacimiento < GetDate())
);
go
