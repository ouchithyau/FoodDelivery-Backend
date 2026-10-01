using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRestaurantDeliveryDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Restaurants",
                newName: "IsOpen");

            migrationBuilder.AddColumn<decimal>(
                name: "DeliveryFee",
                table: "Restaurants",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "EstimatedDeliveryTimeMinutes",
                table: "Restaurants",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "Restaurants",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsVegetarian",
                table: "MenuItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryFee",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "EstimatedDeliveryTimeMinutes",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "IsVegetarian",
                table: "MenuItems");

            migrationBuilder.RenameColumn(
                name: "IsOpen",
                table: "Restaurants",
                newName: "IsActive");
        }
    }
}
