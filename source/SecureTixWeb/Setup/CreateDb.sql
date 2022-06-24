IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'StealThisDatabase')
BEGIN
    CREATE DATABASE [StealThisDatabase]
END