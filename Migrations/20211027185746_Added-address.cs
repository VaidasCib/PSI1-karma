﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Karma.Migrations
{
    public partial class Addedaddress : Migration
    {
       protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Events",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "TEXT");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Address",
                table: "Events",
                type: "TEXT",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
