using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddInvoiceIdempotencyKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Invoice_CustomerId",
                table: "Invoice");

            migrationBuilder.AddColumn<string>(
                name: "IdempotencyKey",
                table: "Invoice",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_CustomerId_IdempotencyKey",
                table: "Invoice",
                columns: new[] { "CustomerId", "IdempotencyKey" },
                unique: true,
                filter: "[IdempotencyKey] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Invoice_CustomerId_IdempotencyKey",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "IdempotencyKey",
                table: "Invoice");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_CustomerId",
                table: "Invoice",
                column: "CustomerId");
        }
    }
}
