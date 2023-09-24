IF NOT EXISTS(SELECT * FROM sys.tables WHERE name = 'Product')
BEGIN
CREATE TABLE Product
(
	Id NVARCHAR(255) NOT NULL,
	Name NVARCHAR(255) NOT NULL,
	Description NVARCHAR(MAX) NOT NULL,
	Price FLOAT NOT NULL,
	CONSTRAINT PK_Product_Id PRIMARY KEY (Id),
	CONSTRAINT UK_Product_Name UNIQUE (Name)
)
END
GO
IF NOT EXISTS(SELECT * FROM sys.tables WHERE name = 'Customer')
BEGIN
CREATE TABLE Customer
(
	Id NVARCHAR(255) NOT NULL,
	Name NVARCHAR(255) NOT NULL,
	Email NVARCHAR(255) NOT NULL,
	Password NVARCHAR(MAX) NOT NULL,
	UserRole INT NOT NULL DEFAULT 1,
	CONSTRAINT PK_Customer_Id PRIMARY KEY (Id),
	CONSTRAINT UK_Customer_Name UNIQUE (Email)
)
END
GO

IF NOT EXISTS(SELECT * FROM sys.tables WHERE name = 'Orders')
BEGIN
CREATE TABLE Orders
(
	Id NVARCHAR(255) NOT NULL,
	OrderNo NVARCHAR(300) NOT NULL,
	Paid BIT NOT NULL DEFAULT 0,
	CustomerId NVARCHAR(255) NOT NULL,
	Total Float NOT NULL,
	CreatedOn DATETIME NOT NULL,
	CONSTRAINT PK_Orders_Id PRIMARY KEY (Id),
	CONSTRAINT UK_Orders_OrderNo UNIQUE (OrderNo),
	CONSTRAINT FK_Orders_CustomerId_Customer_Id FOREIGN KEY (CustomerId) REFERENCES Customer(Id)
)
END
GO

IF NOT EXISTS(SELECT * FROM sys.tables WHERE name = 'OrderProducts')
BEGIN
CREATE TABLE OrderProducts
(
	Id INT IDENTITY(1,1) NOT NULL,
	OrderId NVARCHAR(255) NOT NULL,
	ProductId NVARCHAR(255) NOT NULL,
	Quantity INT NOT NULL,
	CONSTRAINT PK_OrderProducts_Id PRIMARY KEY (Id),
	CONSTRAINT FK_OrderProducts_OrderId_Orders_Id FOREIGN KEY (OrderId) REFERENCES Orders(Id),
	CONSTRAINT FK_OrderProducts_ProductId_Orders_Id FOREIGN KEY (ProductId) REFERENCES Product(Id)
)
END
GO

IF NOT EXISTS (SELECT * FROM Customer)
BEGIN
INSERT INTO [dbo].[Customer]
           ([Id]
           ,[Name]
           ,[Email]
           ,[Password]
           ,[UserRole])
     VALUES
          ('Customer-243','Ekemini','ekeminiudom@gmail.com','725accb5a8e46d19bc3f3a7636d0860a85470fa450be7f0f6a8c7572756765fd',1),
('Customer-638311039745177434','Kenpachi','pachi@gmail.com','f3b52d69cf81d6819c75114d4681a1a84032880ca56258f406703dc17d7d992e',0),
('Customer-638311988069576437','Customer 2','customer02@gmail.com','725accb5a8e46d19bc3f3a7636d0860a85470fa450be7f0f6a8c7572756765fd',1),
('Customer-638311990629454301','Customer 12','customer2@gmail.com','725accb5a8e46d19bc3f3a7636d0860a85470fa450be7f0f6a8c7572756765fd',1)

END
GO

IF NOT EXISTS (SELECT * FROM Product)
BEGIN
INSERT INTO [dbo].[Product]
           ([Id]
           ,[Name]
           ,[Description]
           ,[Price])
     VALUES
          ('Product-638310072977876570','The Quantified Cactus: An Easy Plant Soil Moisture Sensor','This project is a good learning project to get comfortable with soldering and programming an Arduino.',100),
('Product-638310072977884742','A beautiful switch-on book light','Use craft items you have around the house, plus two LEDs and a LilyPad battery holder, to create a useful book light for reading in the dark.',300),
('Product-638310072977884784','Bling your Laptop with an Internet-Connected Light Show','Create a web-connected light-strip API controllable from your website, using the Particle.io.',200),
('Product-638310072977884790','Create a Compact Survival Kit with LED Track Lighting','Use an Altoids tin with Chibitronics sticker LEDs to create a light-up compact that doubles as a survival kit for the young hipster',520),
('Product-638310072977884794','Bubblesort Visualization','Visualization of sailor scouts sorted by bubblesort algorithm by their planets distance from the sun',280),
('Product-638310072977884805','Light-up Corsage','Light-up corsage I made with my summer intern.',100),
('Product-638310072977884809','Pastel hardware kit','Pastel hardware kits complete with custom manufactured pastel alligator clips.',450),
('Product-638310072977884913','Heart-shaped LED','custom molded heart shaped LED with sprinkles.',253),
('Product-638310072977884919','Black Sweatshirt','Black sweatshirt hoody with the Sick of the Internet logo.',152),
('Product-638310072977884924','Sick of the Internet Pins','Still some time to enter the pin/sticker giveaway! ',231),
('Product-638310072977884927','Hipster Dev','Hipster Dev is busy coding away while styled in a camo jacket and orange beanie.',222),
('Product-638310072977884930','Pretty Girls Code Tee','Everyones favorite design is finally here on a tee! The Pretty Girls Code crew-neck tee is available in a soft pink with red writing.',541),
('Product-638310072977884934','Ruby Sis','Styled in a dashiki, Ruby Sis is listening to music while coding in her favorite language, Ruby!',523),
('Product-638310072977884938','Holographic Dark Moon Necklace','Not sure if Ill be making more, get it while I have it in the store.',522),
('Product-638310072977884941','Floppy Crop','Used up the Diskette fabric today to make 2 of these crops.',552),
('Product-638311971723522370','Some New Name For Product','Some Other Description',50.6699981689453),
('Product-638311989436117597','Some Product 5','Some description',123.400001525879)
END
GO


IF NOT EXISTS (SELECT * FROM Orders)
BEGIN
INSERT INTO [dbo].[Orders]
           ([Id]
           ,[OrderNo]
           ,[Paid]
           ,[CustomerId]
           ,[Total]
           ,[CreatedOn])
     VALUES
           ('Order-638310836851785357','Order-W-638310836851789515',1,'Customer-243',8440,'2023-09-23 16:34:45.180'),
('Order-638310838765600221','Order-W-638310838765600257',1,'Customer-243',40544,'2023-09-23 16:37:56.560'),
('Order-638310886554060011','Order-W-638310886554060048',1,'Customer-243',4000,'2023-09-23 17:57:35.407'),
('Order-638310953257182104','Order-W-638310953257186289',1,'Customer-243',4450,'2023-09-23 19:48:45.720'),
('Order-638310953626608502','Order-W-638310953626608552',1,'Customer-243',11800,'2023-09-23 19:49:22.660'),
('Order-638311076940970144','Order-W-638311076940973351',0,'Customer-243',8200,'2023-09-23 23:14:54.097'),
('Order-638311936509639582','Order-X-638311936509643121',1,'Customer-243',4050,'2023-09-24 23:07:30.963')
END
GO


IF NOT EXISTS (SELECT * FROM OrderProducts)
BEGIN
INSERT INTO [dbo].[OrderProducts]
           ([OrderId]
           ,[ProductId]
           ,[Quantity])
     VALUES
           ('Order-638310836851785357','Product-638310072977876570',20),
('Order-638310836851785357','Product-638310072977884794',23),
('Order-638310838765600221','Product-638310072977876570',20),
('Order-638310838765600221','Product-638310072977884805',24),
('Order-638310838765600221','Product-638310072977884938',52),
('Order-638310838765600221','Product-638310072977884809',20),
('Order-638310886554060011','Product-638310072977884742',10),
('Order-638310886554060011','Product-638310072977876570',10),
('Order-638310953257182104','Product-638310072977884794',5),
('Order-638310953257182104','Product-638310072977884805',17),
('Order-638310953257182104','Product-638310072977884809',3),
('Order-638310953626608502','Product-638310072977884794',5),
('Order-638310953626608502','Product-638310072977884790',10),
('Order-638310953626608502','Product-638310072977884790',10),
('Order-638311076940970144','Product-638310072977884784',20),
('Order-638311076940970144','Product-638310072977884794',15),
('Order-638311936509639582','Product-638310072977884913',10),
('Order-638311936509639582','Product-638310072977884919',10)
END
GO