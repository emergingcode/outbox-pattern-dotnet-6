CREATE DATABASE OrderManagement;

GO

USE OrderManagement;

GO

CREATE TABLE Orders
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId VARCHAR(200) NOT NULL,
    TotalItems DECIMAL(18,2) NOT NULL,
    Total DECIMAL(18,2) NOT NULL,
    CreatedAt DATE NOT NULL
)

CREATE TABLE OrderDetails
(
    OrderId INT NOT NULL,
    Sku VARCHAR(10) NOT NULL,
    Name VARCHAR(200) NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    Quantity INT NOT NULL,
	LineNumber INT NOT NULL,
    CONSTRAINT OrderDetails_Pk PRIMARY KEY  (OrderId, Sku)
)

ALTER TABLE OrderDetails ADD CONSTRAINT OrderDetails_Order
    FOREIGN KEY (OrderId)
    REFERENCES Orders (Id);