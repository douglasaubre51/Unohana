create database Isane;

create table UserDetails
(
    UserID int Identity(1,1) primary key,
    FirstName varchar(50) not null,
    LastName varchar(50) not null,
    Password varchar(50) not null
);

select *
from UserDetails;

drop table UserDetails;
drop table PasswordManager;

select *
from sys.tables;

create table Users
(
    UserID int identity(1,1),
    FirstName varchar(50),
    LastName varchar(50),
    Password varchar(50),
)

select *
from Users;

