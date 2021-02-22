using Microsoft.EntityFrameworkCore.Migrations;

namespace TrendyShop.Migrations
{
    public partial class AddedExcelPathsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<string>(
                name: "ConsumptionIncome",
                table: "YearStatistics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConsumptionProfit",
                table: "YearStatistics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DailyLodgings",
                table: "YearStatistics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DamageIncome",
                table: "YearStatistics",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DoubleLodgings",
                table: "YearStatistics",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "GrossIncome",
                table: "YearStatistics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseExpenses",
                table: "YearStatistics",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Lodgings",
                table: "YearStatistics",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Month",
                table: "YearStatistics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RentIncome",
                table: "YearStatistics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Salary",
                table: "YearStatistics",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ExcelPaths",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcelPaths", x => x.Id);
                });
            migrationBuilder.DropColumn(
                name: "Consumo",
                table: "YearStatistics");

            migrationBuilder.DropColumn(
                name: "GananciaPorConsumo",
                table: "YearStatistics");

            migrationBuilder.DropColumn(
                name: "IngresoBruto",
                table: "YearStatistics");

            migrationBuilder.DropColumn(
                name: "IngresoPorDaños",
                table: "YearStatistics");

            migrationBuilder.DropColumn(
                name: "IngresoPorRenta",
                table: "YearStatistics");

            migrationBuilder.DropColumn(
                name: "Mes",
                table: "YearStatistics");

            migrationBuilder.DropColumn(
                name: "OtrosGastos",
                table: "YearStatistics");

            migrationBuilder.DropColumn(
                name: "Reservas",
                table: "YearStatistics");

            migrationBuilder.DropColumn(
                name: "ReservasDiarias",
                table: "YearStatistics");

            migrationBuilder.DropColumn(
                name: "ReservasDobles",
                table: "YearStatistics");

            migrationBuilder.DropColumn(
                name: "Salario",
                table: "YearStatistics");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExcelPaths");

            migrationBuilder.DropColumn(
                name: "ConsumptionIncome",
                table: "YearStatistics");

            migrationBuilder.DropColumn(
                name: "ConsumptionProfit",
                table: "YearStatistics");

            migrationBuilder.DropColumn(
                name: "DailyLodgings",
                table: "YearStatistics");

            migrationBuilder.DropColumn(
                name: "DamageIncome",
                table: "YearStatistics");

            migrationBuilder.DropColumn(
                name: "DoubleLodgings",
                table: "YearStatistics");

            migrationBuilder.DropColumn(
                name: "GrossIncome",
                table: "YearStatistics");

            migrationBuilder.DropColumn(
                name: "HouseExpenses",
                table: "YearStatistics");

            migrationBuilder.DropColumn(
                name: "Lodgings",
                table: "YearStatistics");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "YearStatistics");

            migrationBuilder.DropColumn(
                name: "RentIncome",
                table: "YearStatistics");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "YearStatistics");

            migrationBuilder.AddColumn<float>(
                name: "Consumo",
                table: "YearStatistics",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "GananciaPorConsumo",
                table: "YearStatistics",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "IngresoBruto",
                table: "YearStatistics",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "IngresoPorDaños",
                table: "YearStatistics",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "IngresoPorRenta",
                table: "YearStatistics",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Mes",
                table: "YearStatistics",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "OtrosGastos",
                table: "YearStatistics",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "Reservas",
                table: "YearStatistics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "ReservasDiarias",
                table: "YearStatistics",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "ReservasDobles",
                table: "YearStatistics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Salario",
                table: "YearStatistics",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
