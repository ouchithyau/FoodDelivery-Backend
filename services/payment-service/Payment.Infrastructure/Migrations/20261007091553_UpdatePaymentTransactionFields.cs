using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePaymentTransactionFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "PaymentTransactions",
                newName: "TotalAmount");

            migrationBuilder.AddColumn<decimal>(
                name: "DeliveryFee",
                table: "PaymentTransactions",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PlatformFee",
                table: "PaymentTransactions",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "RestaurantId",
                table: "PaymentTransactions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "RestaurantPayout",
                table: "PaymentTransactions",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "StripePaymentIntentId",
                table: "PaymentTransactions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SubTotal",
                table: "PaymentTransactions",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryFee",
                table: "PaymentTransactions");

            migrationBuilder.DropColumn(
                name: "PlatformFee",
                table: "PaymentTransactions");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "PaymentTransactions");

            migrationBuilder.DropColumn(
                name: "RestaurantPayout",
                table: "PaymentTransactions");

            migrationBuilder.DropColumn(
                name: "StripePaymentIntentId",
                table: "PaymentTransactions");

            migrationBuilder.DropColumn(
                name: "SubTotal",
                table: "PaymentTransactions");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "PaymentTransactions",
                newName: "Amount");
        }
    }
}
