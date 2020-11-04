use SuperShoes
go

if exists (select 1
          from sysobjects
          where  id = object_id('sp_ProductExistence')
          and type in ('P','PC'))
   drop procedure sp_ProductExistence
go

/*==============================================================*/
/* Domain: IntegerID                                            */
/*==============================================================*/
create type IntegerID
   from int
go

/*==============================================================*/
/* Domain: IntegerID_Identity                                   */
/*==============================================================*/
create type IntegerID_Identity
   from int not null
go

/*==============================================================*/
/* Domain: NumberInteger                                        */
/*==============================================================*/
create type NumberInteger
   from int
go

/*==============================================================*/
/* Domain: NumberMoney                                          */
/*==============================================================*/
create type NumberMoney
   from decimal(8,4)
go

/*==============================================================*/
/* Domain: Status                                               */
/*==============================================================*/
create type Status
   from tinyint not null
go

/*==============================================================*/
/* Domain: VC_100                                               */
/*==============================================================*/
create type VC_100
   from varchar(100)
go

/*==============================================================*/
/* Domain: VC_20                                                */
/*==============================================================*/
create type VC_20
   from varchar(20)
go

/*==============================================================*/
/* Domain: VC_255                                               */
/*==============================================================*/
create type VC_255
   from varchar(255)
go

/*==============================================================*/
/* Table: ProductExistences                                     */
/*==============================================================*/
create table ProductExistences (
   StoreId              IntegerID            not null,
   ProductId            IntegerID            not null,
   TotalInShelf         NumberInteger        not null,
   TotalInVault         NumberInteger        not null,
   constraint PK_PRODUCTEXISTENCES primary key (StoreId, ProductId)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('ProductExistences') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'ProductExistences' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'Existences by product and store', 
   'user', @CurrentUser, 'table', 'ProductExistences'
go

/*==============================================================*/
/* Table: Products                                              */
/*==============================================================*/
create table Products (
   ProductId            IntegerID_Identity   identity,
   Code                 VC_20                not null,
   Name                 VC_100               not null,
   Description          VC_255               not null,
   Price                NumberMoney          not null,
   StatusId             Status               not null,
   constraint PK_PRODUCTS primary key (ProductId)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Products') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Products' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'Products table', 
   'user', @CurrentUser, 'table', 'Products'
go

/*==============================================================*/
/* Table: Stores                                                */
/*==============================================================*/
create table Stores (
   StoreId              IntegerID_Identity   identity,
   Name                 VC_100               not null,
   Address              VC_255               not null,
   StatusId             Status               not null,
   constraint PK_STORES primary key (StoreId)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Stores') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Stores' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'Stores table', 
   'user', @CurrentUser, 'table', 'Stores'
go

alter table ProductExistences
   add constraint FK_PRODUCTE_REFERENCE_STORES foreign key (StoreId)
      references Stores (StoreId)
go

alter table ProductExistences
   add constraint FK_PRODUCTE_REFERENCE_PRODUCTS foreign key (ProductId)
      references Products (ProductId)
go

CREATE PROCEDURE sp_ProductExistence
	@ProductId AS INT = 0,
	@StoreId AS INT = 0
AS
BEGIN

	SELECT 
		PE.StoreId
		,S.[Name] as StoreName
	    ,PE.ProductId
		,P.[Name] as ProductName
		,TotalInShelf
		,TotalInVault
	  FROM ProductExistences PE 
	 INNER JOIN Products P ON PE.ProductId = P.ProductId
	 INNER JOIN Stores S ON PE.StoreId = S.StoreId
	 WHERE ( @ProductId = 0 OR PE.ProductId = @ProductId)
	  AND ( @StoreId = 0 OR PE.StoreId = @StoreId )
	  AND ( P.StatusId < 255 )	-- Only active products
	  AND ( S.StatusId< 255 )	-- Only active stores
END
GO
