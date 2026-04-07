using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PatinhasMagicasAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddPasskeyCredentials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PasskeyCredentials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    CredentialId = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    UserHandle = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    PublicKey = table.Column<string>(type: "text", nullable: false),
                    SignatureCounter = table.Column<long>(type: "bigint", nullable: false),
                    FriendlyName = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    CredType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    AaGuid = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Transports = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUsedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasskeyCredentials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PasskeyCredentials_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PasskeyCredentials_CredentialId",
                table: "PasskeyCredentials",
                column: "CredentialId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PasskeyCredentials_UsuarioId",
                table: "PasskeyCredentials",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PasskeyCredentials");
        }
    }
}
