using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AspCoreStudy.Migrations
{
    public partial class BancoInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Palavras",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(type: "TEXT", nullable: true),
                    pontuacao = table.Column<int>(type: "INTEGER", nullable: false),
                    ativo = table.Column<bool>(type: "INTEGER", nullable: false),
                    dataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    dataAlteracao = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Palavras", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Palavras");
        }
    }
}
