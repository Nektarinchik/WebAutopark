USE Autopark;

CREATE TABLE VehicleTypes(
	VehicleTypeId  INT IDENTITY(1, 1) PRIMARY KEY,
	Name           NVARCHAR(255),
	TaxCoefficient FLOAT NOT NULL
);

CREATE TABLE Components(
	ComponentId INT IDENTITY(1, 1) PRIMARY KEY,
	Name        NVARCHAR(255)
);

CREATE TABLE Vehicles(
	VehicleId     INT IDENTITY(1, 1)
		PRIMARY KEY,
	VehicleTypeId INT NOT NULL
		FOREIGN KEY REFERENCES VehicleTypes(VehicleTypeId) 
		ON DELETE CASCADE,
	Model              NVARCHAR(255), 
	RegistrationNumber NVARCHAR(255), 
	Weight             FLOAT,
	Year               INT,
	Mileage            FLOAT,
	Color              NVARCHAR(255),
	FuelConsumption    FLOAT
);

CREATE TABLE Orders(
	OrderId   INT IDENTITY(1, 1) 
		PRIMARY KEY,
	VehicleId INT
		FOREIGN KEY REFERENCES Vehicles (VehicleId)
		ON DELETE SET NULL,
	Date      SMALLDATETIME
);

CREATE TABLE OrderItems(
	OrderItemId INT IDENTITY(1, 1) 
		PRIMARY KEY,
	OrderId INT NOT NULL
		FOREIGN KEY REFERENCES Orders (OrderId)
		ON DELETE CASCADE,
	ComponentId INT NOT NULL
		FOREIGN KEY REFERENCES Components (ComponentId)
		ON DELETE CASCADE,
	Quantity INT DEFAULT 0
);