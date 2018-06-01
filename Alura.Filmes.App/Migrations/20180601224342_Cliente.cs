using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Alura.Filmes.App.Migrations
{
    public partial class Cliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    customer_id = table.Column<byte>(type: "tinyint", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    email = table.Column<string>(type: "varchar(50)", nullable: true),
                    first_name = table.Column<string>(type: "varchar(45)", nullable: false),
                    last_name = table.Column<string>(type: "varchar(45)", nullable: false),
                    create_date = table.Column<DateTime>(type: "datetime", nullable: false, computedColumnSql: "getdate()"),
                    last_update = table.Column<DateTime>(type: "datetime", nullable: false, computedColumnSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.customer_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customer");
        }
    }
}
