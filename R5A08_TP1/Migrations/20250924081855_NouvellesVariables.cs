using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace R5A08_TP1.Migrations
{
    /// <inheritdoc />
    public partial class NouvellesVariables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "t_e_brand_bran",
                schema: "public",
                columns: table => new
                {
                    bran_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    bran_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("brand_pkey", x => x.bran_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_producttype_prty",
                schema: "public",
                columns: table => new
                {
                    prty_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    prty_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("producttype_pkey", x => x.prty_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_product_prod",
                schema: "public",
                columns: table => new
                {
                    prod_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    bran_id = table.Column<int>(type: "integer", nullable: true),
                    prty_id = table.Column<int>(type: "integer", nullable: true),
                    prod_nameproduct = table.Column<string>(type: "text", nullable: false),
                    prod_description = table.Column<string>(type: "text", nullable: false),
                    prod_photoname = table.Column<string>(type: "text", nullable: false),
                    prod_uriphoto = table.Column<string>(type: "text", nullable: false),
                    prod_realstock = table.Column<int>(type: "integer", nullable: false),
                    prod_minstock = table.Column<int>(type: "integer", nullable: false),
                    prod_maxstock = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("product_pkey", x => x.prod_id);
                    table.ForeignKey(
                        name: "FK_products_brand",
                        column: x => x.bran_id,
                        principalSchema: "public",
                        principalTable: "t_e_brand_bran",
                        principalColumn: "bran_id");
                    table.ForeignKey(
                        name: "FK_products_product_type",
                        column: x => x.prty_id,
                        principalSchema: "public",
                        principalTable: "t_e_producttype_prty",
                        principalColumn: "prty_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_e_product_prod_bran_id",
                schema: "public",
                table: "t_e_product_prod",
                column: "bran_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_product_prod_prty_id",
                schema: "public",
                table: "t_e_product_prod",
                column: "prty_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_e_product_prod",
                schema: "public");

            migrationBuilder.DropTable(
                name: "t_e_brand_bran",
                schema: "public");

            migrationBuilder.DropTable(
                name: "t_e_producttype_prty",
                schema: "public");

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
                    prod_description = table.Column<string>(type: "text", nullable: false),
                    prod_nomphoto = table.Column<string>(type: "text", nullable: false),
                    prod_nomproduit = table.Column<string>(type: "text", nullable: false),
                    prod_stockmax = table.Column<int>(type: "integer", nullable: false),
                    prod_stockmin = table.Column<int>(type: "integer", nullable: false),
                    prod_stockreel = table.Column<int>(type: "integer", nullable: false),
                    prod_uriphoto = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("produit_pkey", x => x.prod_id);
                    table.ForeignKey(
                        name: "FK_produits_marque",
                        column: x => x.marq_id,
                        principalSchema: "public",
                        principalTable: "t_e_marque_marq",
                        principalColumn: "marq_id");
                    table.ForeignKey(
                        name: "FK_produits_type_produit",
                        column: x => x.typr_id,
                        principalSchema: "public",
                        principalTable: "t_e_typeproduit_typr",
                        principalColumn: "typr_id");
                });

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
        }
    }
}
