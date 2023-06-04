create table [dbo].[Product_Master](
[id] [int] Identity(1,1) Not NULL,
[product_name] [varchar](250) NULL,
[product_desc] [varchar](250) NULL,
[cost] [decimal](18,0) NULL,
[stock] [int] NULL,
constraint [PK_Product_Master] Primary key clustered([id] Asc)
)

--it is simply used for seeing table----------sp_help 'Product_Master'

create procedure [dbo].[USP_Add_New_Product]
---------ADD the parameters for the the stored procedure here
@ProductName nvarchar (200),
@ProductDescription nvarchar (200),
@ProductCost decimal(18,2),
@Stock int
as
begin
--set nocount on added to prevent extra result sets from
--interfacing with SELECT statements.
set nocount on;
insert into Product_Master(product_name,product_desc,cost,stock) values(@ProductName,@ProductDescription,@ProductCost,@Stock)
end

select * from Product_Master;

--Get Product by ID
create procedure [dbo].[USP_Get_Product_By_Id]
@id int
as begin 
set nocount on;
select id as Id,product_name as ProductName,product_desc as ProductDescription,cost as ProductCost,stock as Stock
from Product_Master where id =@id
END

create procedure [dbo].[USP_Get_ProductList]
--add the parameters for the stored procedure here
as
begin
set nocount on
select id as Id,product_name as ProductName,product_desc as ProductDescription,cost as ProductCost,stock as Stock
from Product_Master
end

--delete Product by ID
create procedure [dbo].[USP_Delete_Product_By_Id]
@id int
as
begin
set nocount on 
delete from Product_Master where id =@id
end

--update the product 
create procedure [dbo].[USP_Update_Product]
@id int,
@ProductName nvarchar (200),
@ProductDescription nvarchar (200),
@ProductCost decimal(18,2),
@Stock int

as begin
set nocount on 
update Product_Master set product_name=@ProductName,product_desc=@ProductDescription,cost=@ProductCost,stock=@Stock where id =@id
end

update Product_Master
set product_name = @ProductName, 
    product_desc = 'bb', 
    cost = 20, 
    stock = 2 
where id = 5


