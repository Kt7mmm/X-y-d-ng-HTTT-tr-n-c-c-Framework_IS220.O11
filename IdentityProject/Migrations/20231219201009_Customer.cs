using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Migrations
{
    public partial class Customer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    bi_id = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cus_email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    bi_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    tk_count = table.Column<int>(type: "int", nullable: false),
                    bi_value = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.bi_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            /*
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    c_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cus_name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cus_phone = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cus_gender = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cus_email = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cus_dob = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    cus_type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cus_point = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.c_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            */
            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    dis_id = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dis_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dis_start = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    dis_end = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    dis_percent = table.Column<float>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.dis_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    mv_id = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mv_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mv_start = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    mv_end = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    mv_duration = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    mv_restrict = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mv_cap = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mv_link_poster = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mv_link_trailer = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mv_detail = table.Column<string>(type: "varchar(7000)", maxLength: 7000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.mv_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MovieTypes",
                columns: table => new
                {
                    type_id = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    type_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieTypes", x => x.type_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    r_id = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    r_capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.r_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Years",
                columns: table => new
                {
                    yre_id = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    yre_year = table.Column<int>(type: "int", nullable: false),
                    yre_count = table.Column<int>(type: "int", nullable: false),
                    yre_value = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Years", x => x.yre_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ApplyDiscounts",
                columns: table => new
                {
                    dis_id = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    bi_id = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyDiscounts", x => new { x.dis_id, x.bi_id });
                    table.ForeignKey(
                        name: "FK_ApplyDiscounts_Bills_bi_id",
                        column: x => x.bi_id,
                        principalTable: "Bills",
                        principalColumn: "bi_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplyDiscounts_Discounts_dis_id",
                        column: x => x.dis_id,
                        principalTable: "Discounts",
                        principalColumn: "dis_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ChooseTypes",
                columns: table => new
                {
                    type_id = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mv_id = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChooseTypes", x => new { x.type_id, x.mv_id });
                    table.ForeignKey(
                        name: "FK_ChooseTypes_Movies_mv_id",
                        column: x => x.mv_id,
                        principalTable: "Movies",
                        principalColumn: "mv_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChooseTypes_MovieTypes_type_id",
                        column: x => x.type_id,
                        principalTable: "MovieTypes",
                        principalColumn: "type_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    st_id = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    r_id = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    st_type = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => new { x.st_id, x.r_id });
                    table.ForeignKey(
                        name: "FK_Seats_Rooms_r_id",
                        column: x => x.r_id,
                        principalTable: "Rooms",
                        principalColumn: "r_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Slots",
                columns: table => new
                {
                    sl_id = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    r_id = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mv_id = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sl_duration = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    sl_start = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    sl_end = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    sl_price = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slots", x => new { x.sl_id, x.r_id, x.mv_id });
                    table.ForeignKey(
                        name: "FK_Slots_Movies_mv_id",
                        column: x => x.mv_id,
                        principalTable: "Movies",
                        principalColumn: "mv_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Slots_Rooms_r_id",
                        column: x => x.r_id,
                        principalTable: "Rooms",
                        principalColumn: "r_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Months",
                columns: table => new
                {
                    mre_id = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mre_yre_id = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mre_month = table.Column<int>(type: "int", nullable: false),
                    mre_count = table.Column<int>(type: "int", nullable: false),
                    mre_value = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Months", x => new { x.mre_id, x.mre_yre_id });
                    table.ForeignKey(
                        name: "FK_Months_Years_mre_yre_id",
                        column: x => x.mre_yre_id,
                        principalTable: "Years",
                        principalColumn: "yre_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    tk_id = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sl_id = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    st_id = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mv_id = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    r_id = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tk_value = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    tk_type = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    bi_id = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tk_available = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => new { x.tk_id, x.sl_id, x.st_id });
                    table.ForeignKey(
                        name: "FK_Tickets_Bills_bi_id",
                        column: x => x.bi_id,
                        principalTable: "Bills",
                        principalColumn: "bi_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_Seats_st_id_r_id",
                        columns: x => new { x.st_id, x.r_id },
                        principalTable: "Seats",
                        principalColumns: new[] { "st_id", "r_id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_Slots_sl_id_r_id_mv_id",
                        columns: x => new { x.sl_id, x.r_id, x.mv_id },
                        principalTable: "Slots",
                        principalColumns: new[] { "sl_id", "r_id", "mv_id" },
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyDiscounts_bi_id",
                table: "ApplyDiscounts",
                column: "bi_id");

            migrationBuilder.CreateIndex(
                name: "IX_ChooseTypes_mv_id",
                table: "ChooseTypes",
                column: "mv_id");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                table: "Customers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_cus_phone",
                table: "Customers",
                column: "cus_phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Months_mre_yre_id",
                table: "Months",
                column: "mre_yre_id");

            migrationBuilder.CreateIndex(
                name: "IX_MovieTypes_type_name",
                table: "MovieTypes",
                column: "type_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seats_r_id",
                table: "Seats",
                column: "r_id");

            migrationBuilder.CreateIndex(
                name: "IX_Slots_mv_id",
                table: "Slots",
                column: "mv_id");

            migrationBuilder.CreateIndex(
                name: "IX_Slots_r_id",
                table: "Slots",
                column: "r_id");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_bi_id",
                table: "Tickets",
                column: "bi_id");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_sl_id_r_id_mv_id",
                table: "Tickets",
                columns: new[] { "sl_id", "r_id", "mv_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_st_id_r_id",
                table: "Tickets",
                columns: new[] { "st_id", "r_id" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplyDiscounts");

            migrationBuilder.DropTable(
                name: "ChooseTypes");
            /*
            migrationBuilder.DropTable(
                name: "Customers");
            */
            migrationBuilder.DropTable(
                name: "Months");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "MovieTypes");

            migrationBuilder.DropTable(
                name: "Years");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Slots");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
