using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Alura.Filmes.App.Migrations
{
    public partial class Heranca : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "staff",
                columns: table => new
                {
                    staff_id = table.Column<byte>(nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    email = table.Column<string>(type: "varchar(50)", nullable: true),
                    username = table.Column<string>(type: "varchar(16)", nullable: false),
                    first_name = table.Column<string>(type: "varchar(45)", nullable: false),
                    password = table.Column<string>(type: "varchar(40)", nullable: true),
                    last_name = table.Column<string>(type: "varchar(45)", nullable: false),
                    last_update = table.Column<DateTime>(type: "datetime", nullable: false, computedColumnSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_staff", x => x.staff_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "staff");
        }
    }
}
