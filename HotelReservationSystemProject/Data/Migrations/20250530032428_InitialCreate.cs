using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelReservationSystemProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Guest",
                columns: table => new
                {
                    GuestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuestFName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuestLName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassportNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guest", x => x.GuestId);
                });

            migrationBuilder.CreateTable(
                name: "Manager",
                columns: table => new
                {
                    ManagerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManagerFName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerLName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerInfo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manager", x => x.ManagerId);
                });

            migrationBuilder.CreateTable(
                name: "Receptionist",
                columns: table => new
                {
                    ReceptionistId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceptionistFName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceptionistLName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceptionistInfo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receptionist", x => x.ReceptionistId);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    RoomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.RoomId);
                });

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    ReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GeneratedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ReportTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: false),
                    ReceptionistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.ReportId);
                    table.ForeignKey(
                        name: "FK_Report_Manager_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Manager",
                        principalColumn: "ManagerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Report_Receptionist_ReceptionistId",
                        column: x => x.ReceptionistId,
                        principalTable: "Receptionist",
                        principalColumn: "ReceptionistId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomBooking",
                columns: table => new
                {
                    RoomBookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckInDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CheckOutDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuestId = table.Column<int>(type: "int", nullable: false),
                    ReceptionistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomBooking", x => x.RoomBookingId);
                    table.ForeignKey(
                        name: "FK_RoomBooking_Guest_GuestId",
                        column: x => x.GuestId,
                        principalTable: "Guest",
                        principalColumn: "GuestId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomBooking_Receptionist_ReceptionistId",
                        column: x => x.ReceptionistId,
                        principalTable: "Receptionist",
                        principalColumn: "ReceptionistId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomItems",
                columns: table => new
                {
                    RoomItemsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomItems", x => x.RoomItemsId);
                    table.ForeignKey(
                        name: "FK_RoomItems_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PaymentAmount = table.Column<double>(type: "float", nullable: false),
                    paymentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceptionistId = table.Column<int>(type: "int", nullable: false),
                    RoomBookingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payment_Receptionist_ReceptionistId",
                        column: x => x.ReceptionistId,
                        principalTable: "Receptionist",
                        principalColumn: "ReceptionistId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payment_RoomBooking_RoomBookingId",
                        column: x => x.RoomBookingId,
                        principalTable: "RoomBooking",
                        principalColumn: "RoomBookingId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "RoomBookingDetails",
                columns: table => new
                {
                    RoomBookingDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomPrice = table.Column<double>(type: "float", nullable: false),
                    RoomBookingId = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomBookingDetails", x => x.RoomBookingDetailsId);
                    table.ForeignKey(
                        name: "FK_RoomBookingDetails_RoomBooking_RoomBookingId",
                        column: x => x.RoomBookingId,
                        principalTable: "RoomBooking",
                        principalColumn: "RoomBookingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomBookingDetails_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payment_ReceptionistId",
                table: "Payment",
                column: "ReceptionistId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_RoomBookingId",
                table: "Payment",
                column: "RoomBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_ManagerId",
                table: "Report",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_ReceptionistId",
                table: "Report",
                column: "ReceptionistId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomBooking_GuestId",
                table: "RoomBooking",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomBooking_ReceptionistId",
                table: "RoomBooking",
                column: "ReceptionistId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomBookingDetails_RoomBookingId",
                table: "RoomBookingDetails",
                column: "RoomBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomBookingDetails_RoomId",
                table: "RoomBookingDetails",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomItems_RoomId",
                table: "RoomItems",
                column: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "RoomBookingDetails");

            migrationBuilder.DropTable(
                name: "RoomItems");

            migrationBuilder.DropTable(
                name: "Manager");

            migrationBuilder.DropTable(
                name: "RoomBooking");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "Guest");

            migrationBuilder.DropTable(
                name: "Receptionist");
        }
    }
}
