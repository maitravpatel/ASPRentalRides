using Microsoft.EntityFrameworkCore.Migrations;

namespace RentalRides.Data.Migrations
{
    public partial class AddBookingListEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "BookingLists",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                table: "BookingLists");
        }
    }
}
