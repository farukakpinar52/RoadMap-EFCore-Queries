using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Query.Migrations
{
    /// <inheritdoc />
    public partial class mig_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UrunParca_Parcas_ParcaId",
                table: "UrunParca");

            migrationBuilder.DropForeignKey(
                name: "FK_UrunParca_Uruns_UrunId",
                table: "UrunParca");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UrunParca",
                table: "UrunParca");

            migrationBuilder.RenameTable(
                name: "UrunParca",
                newName: "UrunParcas");

            migrationBuilder.RenameIndex(
                name: "IX_UrunParca_ParcaId",
                table: "UrunParcas",
                newName: "IX_UrunParcas_ParcaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UrunParcas",
                table: "UrunParcas",
                columns: new[] { "UrunId", "ParcaId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UrunParcas_Parcas_ParcaId",
                table: "UrunParcas",
                column: "ParcaId",
                principalTable: "Parcas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UrunParcas_Uruns_UrunId",
                table: "UrunParcas",
                column: "UrunId",
                principalTable: "Uruns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UrunParcas_Parcas_ParcaId",
                table: "UrunParcas");

            migrationBuilder.DropForeignKey(
                name: "FK_UrunParcas_Uruns_UrunId",
                table: "UrunParcas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UrunParcas",
                table: "UrunParcas");

            migrationBuilder.RenameTable(
                name: "UrunParcas",
                newName: "UrunParca");

            migrationBuilder.RenameIndex(
                name: "IX_UrunParcas_ParcaId",
                table: "UrunParca",
                newName: "IX_UrunParca_ParcaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UrunParca",
                table: "UrunParca",
                columns: new[] { "UrunId", "ParcaId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UrunParca_Parcas_ParcaId",
                table: "UrunParca",
                column: "ParcaId",
                principalTable: "Parcas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UrunParca_Uruns_UrunId",
                table: "UrunParca",
                column: "UrunId",
                principalTable: "Uruns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
