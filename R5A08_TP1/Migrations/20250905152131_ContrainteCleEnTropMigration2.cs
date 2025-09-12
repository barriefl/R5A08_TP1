using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace R5A08_TP1.Migrations
{
    /// <inheritdoc />
    public partial class ContrainteCleEnTropMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TypeProduitNavigationIdTypeProduit",
                schema: "public",
                table: "t_e_produit_prod",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "MarqueNavigationIdMarque",
                schema: "public",
                table: "t_e_produit_prod",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TypeProduitNavigationIdTypeProduit",
                schema: "public",
                table: "t_e_produit_prod",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MarqueNavigationIdMarque",
                schema: "public",
                table: "t_e_produit_prod",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
