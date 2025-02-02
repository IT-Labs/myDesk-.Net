﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyDesk.Data.Migrations
{
    public partial class DateTimeToDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Reservation",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Reservation",
                type: "date",
                nullable: false,
                defaultValueSql: "('0001-01-01T00:00:00.0000000')",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "('0001-01-01T00:00:00.0000000')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Reservation",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Reservation",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "('0001-01-01T00:00:00.0000000')",
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldDefaultValueSql: "('0001-01-01T00:00:00.0000000')");
        }
    }
}
