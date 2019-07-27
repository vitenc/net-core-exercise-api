USE [Northwind]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[sp_Products](
	@Action nvarchar(50), 
	@ProductID int = NULL,
	@ProductName nvarchar(40) = NULL,
	@SupplierID int = NULL,
	@CategoryID int = NULL,
	@QuantityPerUnit nvarchar(20) = NULL,
	@UnitPrice money = NULL,
	@UnitsInStock smallint = NULL,
	@UnitsOnOrder smallint = NULL,
	@ReorderLevel smallint = NULL,
	@Discontinued smallint = NULL
) as
 
 /*	Usage:

 	exec sp_Products 
		@Action = 'SelectAll'

 	exec sp_Products 
		@Action = 'SelectActive'

	exec sp_Products 
		@Action = 'SelectOne',
		@ProductID = 77

	exec sp_Products 
		@Action = 'AddNew',
		@ProductName = 'Popcorn',
		@SupplierID = 15,
		@CategoryID = 5,
		@QuantityPerUnit = '12 boxes',
		@UnitPrice = 12.50,
		@UnitsInStock = 5,
		@UnitsOnOrder = 12,
		@ReorderLevel = 6,
		@Discontinued = 0

	exec sp_Products 
		@Action = 'Update',
		@ProductID = 78,
		@ProductName = 'Popcorn with butter',
		@SupplierID = 15,
		@CategoryID = 5,
		@QuantityPerUnit = '12 boxes',
		@UnitPrice = 19.50,
		@UnitsInStock = 5,
		@UnitsOnOrder = 12,
		@ReorderLevel = 6,
		@Discontinued = 0

	exec sp_Products 
		@Action = 'Delete',
		@ProductID = 78
 */

      declare @intLen int, @x int, @Item nvarchar(100), @Field nvarchar(4000)
if @Action='SelectAll'
     select  
		p.ProductID,
		p.ProductName,
		p.SupplierID,
		s.CompanyName as Supplier,
		p.CategoryID,
		c.CategoryName,
		p.QuantityPerUnit,
		p.UnitPrice,
		p.UnitsInStock,
		p.UnitsOnOrder,
		p.ReorderLevel,
		p.Discontinued
     from	
		dbo.Products p join
		dbo.Categories c on p.CategoryID = c.CategoryID join
		dbo.Suppliers s on p.SupplierID = s.SupplierID
 
if @Action='SelectActive'
     select  
		p.ProductID,
		p.ProductName,
		p.SupplierID,
		s.CompanyName as Supplier,
		p.CategoryID,
		c.CategoryName,
		p.QuantityPerUnit,
		p.UnitPrice,
		p.UnitsInStock,
		p.UnitsOnOrder,
		p.ReorderLevel,
		p.Discontinued
     from	
		dbo.Products p join
		dbo.Categories c on p.CategoryID = c.CategoryID join
		dbo.Suppliers s on p.SupplierID = s.SupplierID
	where 
		p.Discontinued != 1
     
 
if @Action='SelectOne'
     select  
		p.ProductID,
		p.ProductName,
		p.SupplierID,
		s.CompanyName as Supplier,
		p.CategoryID,
		c.CategoryName,
		p.QuantityPerUnit,
		p.UnitPrice,
		p.UnitsInStock,
		p.UnitsOnOrder,
		p.ReorderLevel,
		p.Discontinued
     from	
		dbo.Products p join
		dbo.Categories c on p.CategoryID = c.CategoryID join
		dbo.Suppliers s on p.SupplierID = s.SupplierID
	where 
		p.ProductId = @ProductID

 
if @Action='AddNew'
	begin
     insert dbo.Products (
		ProductName,
		SupplierID,
		CategoryID,
		QuantityPerUnit,
		UnitPrice,
		UnitsInStock,
		UnitsOnOrder,
		ReorderLevel,
		Discontinued)
	 values (	
		@ProductName,
		@SupplierID,
		@CategoryID,
		@QuantityPerUnit,
		@UnitPrice,
		@UnitsInStock,
		@UnitsOnOrder,
		@ReorderLevel,
		@Discontinued)
 
	 select @@IDENTITY
	end

if @Action='Update'
     Update Products
     set ProductName = @ProductName,
		 SupplierID = @SupplierID,
		 CategoryID = @CategoryID,
		 QuantityPerUnit = @QuantityPerUnit,
		 UnitPrice = @UnitPrice,
		 UnitsInStock = @UnitsInStock,
		 UnitsOnOrder = @UnitsOnOrder,
		 ReorderLevel = @ReorderLevel,
		 Discontinued = @Discontinued
     where ProductId=@ProductID
 
if @Action='Delete'
     delete Products
	 where ProductID = @ProductID
   
 

GO


