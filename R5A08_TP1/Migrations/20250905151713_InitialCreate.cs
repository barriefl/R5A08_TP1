using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace R5A08_TP1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "t_e_marque_marq",
                schema: "public",
                columns: table => new
                {
                    marq_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    marq_nom = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("marque_pkey", x => x.marq_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_typeproduit_typr",
                schema: "public",
                columns: table => new
                {
                    typr_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    typr_nom = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("typeproduit_pkey", x => x.typr_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_produit_prod",
                schema: "public",
                columns: table => new
                {
                    prod_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    marq_id = table.Column<int>(type: "integer", nullable: true),
                    typr_id = table.Column<int>(type: "integer", nullable: true),
                    prod_nomproduit = table.Column<string>(type: "text", nullable: false),
                    prod_description = table.Column<string>(type: "text", nullable: false),
                    prod_nomphoto = table.Column<string>(type: "text", nullable: false),
                    prod_uriphoto = table.Column<string>(type: "text", nullable: false),
                    prod_stockreel = table.Column<int>(type: "integer", nullable: false),
                    prod_stockmin = table.Column<int>(type: "integer", nullable: false),
                    prod_stockmax = table.Column<int>(type: "integer", nullable: false),
                    TypeProduitNavigationIdTypeProduit = table.Column<int>(type: "integer", nullable: false),
                    MarqueNavigationIdMarque = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("produit_pkey", x => x.prod_id);
                    table.ForeignKey(
                        name: "FK_produits_marque",
                        column: x => x.MarqueNavigationIdMarque,
                        principalSchema: "public",
                        principalTable: "t_e_marque_marq",
                        principalColumn: "marq_id");
                    table.ForeignKey(
                        name: "FK_type_produit_produits",
                        column: x => x.TypeProduitNavigationIdTypeProduit,
                        principalSchema: "public",
                        principalTable: "t_e_typeproduit_typr",
                        principalColumn: "typr_id");
                });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_e_produit_prod",
                schema: "public");

            migrationBuilder.DropTable(
                name: "t_e_marque_marq",
                schema: "public");

            migrationBuilder.DropTable(
                name: "t_e_typeproduit_typr",
                schema: "public");
        }
    }
}
