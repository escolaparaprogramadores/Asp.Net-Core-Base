using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
    public partial class desafio2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Senha = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Telefone",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Numero = table.Column<string>(nullable: true),
                    Ddd = table.Column<string>(nullable: true),
                    UsuarioId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Telefone_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioTelefones",
                columns: table => new
                {
                    TelefoneId = table.Column<Guid>(nullable: false),
                    UsuarioId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioTelefones", x => new { x.UsuarioId, x.TelefoneId });
                    table.ForeignKey(
                        name: "FK_UsuarioTelefones_Telefone_TelefoneId",
                        column: x => x.TelefoneId,
                        principalTable: "Telefone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioTelefones_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Telefone_UsuarioId",
                table: "Telefone",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioTelefones_TelefoneId",
                table: "UsuarioTelefones",
                column: "TelefoneId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioTelefones");

            migrationBuilder.DropTable(
                name: "Telefone");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
