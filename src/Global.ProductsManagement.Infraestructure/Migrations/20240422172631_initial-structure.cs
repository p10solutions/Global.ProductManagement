using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Global.ProductsManagement.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class initialstructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_BRAND",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_BRAND", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TB_CATEGORY",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CATEGORY", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TB_PRODUCT",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "varchar(200)", nullable: false),
                    DETAILS = table.Column<string>(type: "varchar(max)", nullable: false),
                    PRICE = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    DT_CREATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DT_UPDATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CATEGORY_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    STATUS = table.Column<int>(type: "int", nullable: false),
                    BRAND = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PRODUCT", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TB_PRODUCT_TB_BRAND_BRAND",
                        column: x => x.BRAND,
                        principalTable: "TB_BRAND",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_PRODUCT_TB_CATEGORY_CATEGORY_ID",
                        column: x => x.CATEGORY_ID,
                        principalTable: "TB_CATEGORY",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_PRODUCT_BRAND",
                table: "TB_PRODUCT",
                column: "BRAND");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PRODUCT_CATEGORY_ID",
                table: "TB_PRODUCT",
                column: "CATEGORY_ID");


            migrationBuilder.Sql(@"
                INSERT INTO TB_BRAND (ID, NAME) VALUES ('3849fd79-0666-43fe-b390-ce2ef4cbcba7', 'Brand 1');
                INSERT INTO TB_BRAND (ID, NAME) VALUES ('4b6350d5-5f9c-4925-ba4b-703a14763a81', 'Brand 2');
                INSERT INTO TB_BRAND (ID, NAME) VALUES ('dea39053-3104-4e23-8675-fe361ffb733d', 'Brand 3');
                INSERT INTO TB_BRAND (ID, NAME) VALUES ('8bbc8343-f18e-4872-9038-8449d205ed1b', 'Brand 4');
                INSERT INTO TB_BRAND (ID, NAME) VALUES ('94536ead-466a-43ef-9146-a12683c50464', 'Brand 5');
                INSERT INTO TB_BRAND (ID, NAME) VALUES ('f7dcacc4-bd09-4c96-81cb-263bb4f058c2', 'Brand 6');
                INSERT INTO TB_BRAND (ID, NAME) VALUES ('7e8f2363-e812-4e19-8612-448ae2e109cf', 'Brand 7');
                INSERT INTO TB_BRAND (ID, NAME) VALUES ('be75e2cb-e129-4537-81c3-22a2f92ea7ef', 'Brand 8');
                INSERT INTO TB_BRAND (ID, NAME) VALUES ('bf314b5b-2e81-439d-aab2-11bfbd8f0f59', 'Brand 9');
                INSERT INTO TB_BRAND (ID, NAME) VALUES ('2534ac19-952b-4335-9797-81ca22220f35', 'Brand 10');");

            migrationBuilder.Sql(@"
                INSERT INTO TB_CATEGORY (ID, NAME) VALUES ('b8118600-4003-4caa-9344-f1cd1f218aa5', 'Category 1');
                INSERT INTO TB_CATEGORY (ID, NAME) VALUES ('6fe1e3d9-e652-4c58-b912-9209ae32e904', 'Category 2');
                INSERT INTO TB_CATEGORY (ID, NAME) VALUES ('d0b47de8-4a55-47d8-bcdd-209a3c6828e4', 'Category 3');
                INSERT INTO TB_CATEGORY (ID, NAME) VALUES ('03951dea-85ca-4964-9462-84fbdcd45e40', 'Category 4');
                INSERT INTO TB_CATEGORY (ID, NAME) VALUES ('cbedf365-0b50-4132-ba08-a138e645feae', 'Category 5');
                INSERT INTO TB_CATEGORY (ID, NAME) VALUES ('b535ef75-ed0e-4f58-be0c-fc9dd1cf40a0', 'Category 6');
                INSERT INTO TB_CATEGORY (ID, NAME) VALUES ('5a861b6f-7beb-42db-a963-e41daeeb7630', 'Category 7');
                INSERT INTO TB_CATEGORY (ID, NAME) VALUES ('f91d60c2-2d0c-4f51-b88b-636daf0dfb7d', 'Category 8');
                INSERT INTO TB_CATEGORY (ID, NAME) VALUES ('a2e4f926-ff90-4c6c-baf5-af84175b0d3d', 'Category 9');
                INSERT INTO TB_CATEGORY (ID, NAME) VALUES ('30e27b1d-5c77-46d0-8dee-6ebdb4da23ba', 'Category 10');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_PRODUCT");

            migrationBuilder.DropTable(
                name: "TB_BRAND");

            migrationBuilder.DropTable(
                name: "TB_CATEGORY");

            migrationBuilder.Sql(@"DELETE FROM TB_BRAND WHERE ID IN(
                '3849fd79-0666-43fe-b390-ce2ef4cbcba7'
                '4b6350d5-5f9c-4925-ba4b-703a14763a81'
                'dea39053-3104-4e23-8675-fe361ffb733d'
                '8bbc8343-f18e-4872-9038-8449d205ed1b'
                '94536ead-466a-43ef-9146-a12683c50464'
                'f7dcacc4-bd09-4c96-81cb-263bb4f058c2'
                '7e8f2363-e812-4e19-8612-448ae2e109cf'
                'be75e2cb-e129-4537-81c3-22a2f92ea7ef'
                'bf314b5b-2e81-439d-aab2-11bfbd8f0f59'
                '2534ac19-952b-4335-9797-81ca22220f35'
                    )");

            migrationBuilder.Sql(@"DELETE FROM TB_CATEGORY WHERE ID IN(
                'b8118600-4003-4caa-9344-f1cd1f218aa5'
                '6fe1e3d9-e652-4c58-b912-9209ae32e904'
                'd0b47de8-4a55-47d8-bcdd-209a3c6828e4'
                '03951dea-85ca-4964-9462-84fbdcd45e40'
                'cbedf365-0b50-4132-ba08-a138e645feae'
                'b535ef75-ed0e-4f58-be0c-fc9dd1cf40a0'
                '5a861b6f-7beb-42db-a963-e41daeeb7630'
                'f91d60c2-2d0c-4f51-b88b-636daf0dfb7d'
                'a2e4f926-ff90-4c6c-baf5-af84175b0d3d'
                '30e27b1d-5c77-46d0-8dee-6ebdb4da23ba'
            )");
        }
    }
}
