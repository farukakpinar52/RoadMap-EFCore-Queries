﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Query.Migrations
{
    /// <inheritdoc />
    public partial class mig_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UrunParca",
                columns: table => new
                {
                    UrunId = table.Column<int>(type: "int", nullable: false),
                    ParcaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrunParca", x => new { x.UrunId, x.ParcaId });
                    table.ForeignKey(
                        name: "FK_UrunParca_Parcas_ParcaId",
                        column: x => x.ParcaId,
                        principalTable: "Parcas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UrunParca_Uruns_UrunId",
                        column: x => x.UrunId,
                        principalTable: "Uruns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UrunParca_ParcaId",
                table: "UrunParca",
                column: "ParcaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UrunParca");
        }
    }
}
