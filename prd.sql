create database stock
use stock

go
create table tb_produc(
prd_id smallint primary key identity,
prd_name varchar(50),
prd_category varchar(50),
prd_price numeric(18,2)
)
go


insert into tb_produc(prd_name,prd_category,prd_price)values('tuna','enlatado',1200)
insert into tb_produc(prd_name,prd_category,prd_price)values('tomate','vejetales',900)
insert into tb_produc(prd_name,prd_category,prd_price)values('ribai','carnico',15000)

go
/**************************************************************************************/

create procedure sp_insprd
@prd_name varchar(50),
@prd_category varchar(50),
@prd_price numeric(18,2)
as
  begin
    insert into tb_produc(prd_name,prd_category,prd_price)values(@prd_name,@prd_category,@prd_price)
  end
go

/**************************************************************************************/

go
CREATE PROCEDURE sp_upprd
@prd_id smallint,
@prd_name varchar(50),
@prd_category varchar(50),
@prd_price numeric(18,2)
AS  
BEGIN
	UPDATE tb_produc
	SET    prd_name = @prd_name,
           prd_category  = @prd_category,
           prd_price =  @prd_price
	WHERE  prd_id=@prd_id
END
go

/**************************************************************************************/

go
CREATE PROCEDURE sp_delprd
@prd_id smallint
AS  
BEGIN
      delete from tb_produc	WHERE  prd_id=@prd_id
END
go

/**************************************************************************************/

go
CREATE PROCEDURE sp_viewAll

AS  
BEGIN
      select * from tb_produc	
END
go

/**************************************************************************************/

go
CREATE PROCEDURE sp_viewId
@prd_id smallint
AS  
BEGIN
      select * from tb_produc WHERE  prd_id=@prd_id
END
go

/**************************************************************************************/
go
exec sp_viewId 2
exec sp_viewAll
go
exec sp_insprd 'paracetamol','farmaco',2000

go
exec sp_upprd  4,'globo','juguetes',200

go
exec sp_delprd 4
go
