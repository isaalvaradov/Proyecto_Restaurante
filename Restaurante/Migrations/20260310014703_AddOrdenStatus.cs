using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Restaurante.Migrations
{
    /// <inheritdoc />
    public partial class AddOrdenStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"
IF OBJECT_ID('dbo.Ordenes', 'U') IS NOT NULL
BEGIN
    IF COL_LENGTH('dbo.Ordenes','Status') IS NULL
    BEGIN
        ALTER TABLE dbo.Ordenes ADD Status nvarchar(50) NOT NULL CONSTRAINT DF_Ordenes_Status DEFAULT('Pending');
    END
END
ELSE
BEGIN
    CREATE TABLE dbo.Ordenes(
        OrdenId int IDENTITY(1,1) NOT NULL PRIMARY KEY,
        FechaCompra datetime2 NOT NULL,
        CompradorCorreo nvarchar(max) NOT NULL,
        Total decimal(18,2) NOT NULL,
        Status nvarchar(50) NOT NULL DEFAULT('Pending')
    );
END

IF OBJECT_ID('dbo.OrdenItem','U') IS NULL
BEGIN
  CREATE TABLE dbo.OrdenItem(
    OrdenItemId int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    PlatilloId int NOT NULL,
    Nombre nvarchar(max) NOT NULL,
    Precio decimal(18,2) NOT NULL,
    Cantidad int NOT NULL,
    OrdenId int NULL,
    CONSTRAINT FK_OrdenItem_Ordenes_OrdenId FOREIGN KEY (OrdenId) REFERENCES dbo.Ordenes(OrdenId)
  );
END
ELSE
BEGIN
  IF COL_LENGTH('dbo.OrdenItem','OrdenId') IS NULL
  BEGIN
    ALTER TABLE dbo.OrdenItem ADD OrdenId int NULL;
    IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE parent_object_id = OBJECT_ID('dbo.OrdenItem') AND referenced_object_id = OBJECT_ID('dbo.Ordenes'))
    BEGIN
      ALTER TABLE dbo.OrdenItem ADD CONSTRAINT FK_OrdenItem_Ordenes_OrdenId FOREIGN KEY (OrdenId) REFERENCES dbo.Ordenes(OrdenId);
    END
  END
END
";
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = @"
IF OBJECT_ID('dbo.OrdenItem','U') IS NOT NULL
BEGIN
  DROP TABLE dbo.OrdenItem;
END

IF OBJECT_ID('dbo.Ordenes','U') IS NOT NULL
BEGIN
  IF COL_LENGTH('dbo.Ordenes','Status') IS NOT NULL
  BEGIN
    DECLARE @dfname nvarchar(128);
    SELECT @dfname = dc.name
    FROM sys.default_constraints dc
    JOIN sys.columns c ON dc.parent_object_id = c.object_id AND dc.parent_column_id = c.column_id
    WHERE c.object_id = OBJECT_ID(N'dbo.Ordenes') AND c.name = 'Status';

    IF @dfname IS NOT NULL
      EXEC('ALTER TABLE dbo.Ordenes DROP CONSTRAINT [' + @dfname + ']');

    ALTER TABLE dbo.Ordenes DROP COLUMN Status;
  END
END
";
            migrationBuilder.Sql(sql);
        }
    }
}
