using Microsoft.EntityFrameworkCore.Migrations;

namespace RentalRides.Data.Migrations
{
    public partial class UpdateToDecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalCost",
                table: "BookingDetails",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TotalCost",
                table: "BookingDetails",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
