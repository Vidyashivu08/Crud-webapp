-- Create Database
IF DB_ID('crud_app') IS NULL
BEGIN
    CREATE DATABASE crud_app;
END
GO

USE crud_app;
GO

-- Create Table
IF OBJECT_ID('personal_details', 'U') IS NULL
BEGIN
    CREATE TABLE personal_details (
        id INT IDENTITY(1,1) PRIMARY KEY,
        name NVARCHAR(100) NOT NULL,
        address NVARCHAR(MAX) NOT NULL,
        state NVARCHAR(50) NOT NULL,
        district NVARCHAR(50) NOT NULL,
        dob DATE NOT NULL,
        language NVARCHAR(50) NOT NULL,
        created_at DATETIME DEFAULT GETDATE()
    );
END
GO

-- Create Indexes
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_name' AND object_id = OBJECT_ID('personal_details'))
BEGIN
    CREATE INDEX idx_name ON personal_details(name);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_state' AND object_id = OBJECT_ID('personal_details'))
BEGIN
    CREATE INDEX idx_state ON personal_details(state);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_district' AND object_id = OBJECT_ID('personal_details'))
BEGIN
    CREATE INDEX idx_district ON personal_details(district);
END
GO

-- Sample Data (Optional)
INSERT INTO personal_details (name, address, state, district, dob, language)
VALUES 
    ('John Doe', '123 Main St', 'Karnataka', 'Bangalore', '1990-01-15', 'English'),
    ('Jane Smith', '456 Elm St', 'Maharashtra', 'Mumbai', '1985-05-20', 'Hindi');
GO
