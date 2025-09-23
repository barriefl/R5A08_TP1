using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace R5A08_TP1.Migrations
{
    /// <inheritdoc />
    public partial class ContrainteCleEnTropMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_produits_marque",
                schema: "public",
                table: "t_e_produit_prod");

            migrationBuilder.DropForeignKey(
                name: "FK_produits_type_produit",
                schema: "public",
                table: "t_e_produit_prod");

            migrationBuilder.DropIndex(
                name: "IX_t_e_produit_prod_MarqueNavigationIdMarque",
                schema: "public",
                table: "t_e_produit_prod");

            migrationBuilder.DropIndex(
                name: "IX_t_e_produit_prod_TypeProduitNavigationIdTypeProduit",
                schema: "public",
                table: "t_e_produit_prod");

            migrationBuilder.DropColumn(
                name: "MarqueNavigationIdMarque",
                schema: "public",
                table: "t_e_produit_prod");

            migrationBuilder.DropColumn(
                name: "TypeProduitNavigationIdTypeProduit",
                schema: "public",
                table: "t_e_produit_prod");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_produit_prod_marq_id",
                schema: "public",
                table: "t_e_produit_prod",
                column: "marq_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_produit_prod_typr_id",
                schema: "public",
                table: "t_e_produit_prod",
                column: "typr_id");

            migrationBuilder.AddForeignKey(
                name: "FK_produits_marque",
                schema: "public",
                table: "t_e_produit_prod",
                column: "marq_id",
                principalSchema: "public",
                principalTable: "t_e_marque_marq",
                principalColumn: "marq_id");

            migrationBuilder.AddForeignKey(
                name: "FK_produits_type_produit",
                schema: "public",
                table: "t_e_produit_prod",
                column: "typr_id",
                principalSchema: "public",
                principalTable: "t_e_typeproduit_typr",
                principalColumn: "typr_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_produits_marque",
                schema: "public",
                table: "t_e_produit_prod");

            migrationBuilder.DropForeignKey(
                name: "FK_produits_type_produit",
                schema: "public",
                table: "t_e_produit_prod");

            migrationBuilder.DropIndex(
                name: "IX_t_e_produit_prod_marq_id",
                schema: "public",
                table: "t_e_produit_prod");

            migrationBuilder.DropIndex(
                name: "IX_t_e_produit_prod_typr_id",
                schema: "public",
                table: "t_e_produit_prod");

            migrationBuilder.AddColumn<int>(
                name: "MarqueNavigationIdMarque",
                schema: "public",
                table: "t_e_produit_prod",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeProduitNavigationIdTypeProduit",
                schema: "public",
                table: "t_e_produit_prod",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_produit_prod_MarqueNavigationIdMarque",
                schema: "public",
                table: "t_e_produit_prod",
                column: "MarqueNavigationIdMarque");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_produit_prod_TypeProduitNavigationIdTypeProduit",
                schema: "public",
                table: "t_e_produit_prod",
                column: "TypeProduitNavigationIdTypeProduit");

            migrationBuilder.AddForeignKey(
                name: "FK_produits_marque",
                schema: "public",
                table: "t_e_produit_prod",
                column: "MarqueNavigationIdMarque",
                principalSchema: "public",
                principalTable: "t_e_marque_marq",
                principalColumn: "marq_id");

            migrationBuilder.AddForeignKey(
                name: "FK_produits_type_produit",
                schema: "public",
                table: "t_e_produit_prod",
                column: "TypeProduitNavigationIdTypeProduit",
                principalSchema: "public",
                principalTable: "t_e_typeproduit_typr",
                principalColumn: "typr_id");
        }
    }
}
