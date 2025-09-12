using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace R5A08_TP1.Migrations
{
    /// <inheritdoc />
    public partial class ContrainteCleEnTropMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_type_produit_produits",
                schema: "public",
                table: "t_e_produit_prod");

            migrationBuilder.AddForeignKey(
                name: "FK_produits_type_produit",
                schema: "public",
                table: "t_e_produit_prod",
                column: "TypeProduitNavigationIdTypeProduit",
                principalSchema: "public",
                principalTable: "t_e_typeproduit_typr",
                principalColumn: "typr_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_produits_type_produit",
                schema: "public",
                table: "t_e_produit_prod");

            migrationBuilder.AddForeignKey(
                name: "FK_type_produit_produits",
                schema: "public",
                table: "t_e_produit_prod",
                column: "TypeProduitNavigationIdTypeProduit",
                principalSchema: "public",
                principalTable: "t_e_typeproduit_typr",
                principalColumn: "typr_id");
        }
    }
}
