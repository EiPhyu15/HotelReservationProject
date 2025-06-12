using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelReservationSystemProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateinvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_RoomBooking_Room_RoomId",
            //    table: "RoomBooking");

            //migrationBuilder.DropIndex(
            //    name: "IX_RoomBooking_RoomId",
            //    table: "RoomBooking");

            //migrationBuilder.DropColumn(
            //    name: "RoomId",
            //    table: "RoomBooking");

            //migrationBuilder.AddColumn<int>(
            //    name: "InvoiceId",
            //    table: "Payment",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalAmount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceDate = table.Column<DateOnly>(type: "date", nullable: false),
                    RoomBookingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoice_RoomBooking_RoomBookingId",
                        column: x => x.RoomBookingId,
                        principalTable: "RoomBooking",
                        principalColumn: "RoomBookingId",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Payment_InvoiceId",
            //    table: "Payment",
            //    column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_RoomBookingId",
                table: "Invoice",
                column: "RoomBookingId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Payment_Invoice_InvoiceId",
            //    table: "Payment",
            //    column: "InvoiceId",
            //    principalTable: "Invoice",
            //    principalColumn: "InvoiceId",
            //    onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Payment_Invoice_InvoiceId",
            //    table: "Payment");

            //migrationBuilder.DropTable(
            //    name: "Invoice");

            //migrationBuilder.DropIndex(
            //    name: "IX_Payment_InvoiceId",
            //    table: "Payment");

            //migrationBuilder.DropColumn(
            //    name: "InvoiceId",
            //    table: "Payment");

            //migrationBuilder.AddColumn<int>(
            //    name: "RoomId",
            //    table: "RoomBooking",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.CreateIndex(
            //    name: "IX_RoomBooking_RoomId",
            //    table: "RoomBooking",
            //    column: "RoomId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_RoomBooking_Room_RoomId",
            //    table: "RoomBooking",
            //    column: "RoomId",
            //    principalTable: "Room",
            //    principalColumn: "RoomId",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
