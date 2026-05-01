using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ServiceRequestMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RejectionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignedStaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_Items_CategoryItemId",
                        column: x => x.CategoryItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requests_Users_AssignedStaffId",
                        column: x => x.AssignedStaffId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requests_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachments_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attachments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "LastUpdatedBy", "LastUpdatedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2026, 5, 1, 15, 17, 8, 796, DateTimeKind.Local).AddTicks(9466), "Physical devices and equipment", null, null, "Hardware" },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2026, 5, 1, 15, 17, 8, 796, DateTimeKind.Local).AddTicks(9470), "Applications and licenses", null, null, "Software" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Email", "FullName", "IsActive", "LastUpdatedBy", "LastUpdatedDate", "PasswordHash", "PhoneNumber", "Role", "UserName" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2026, 5, 1, 15, 17, 8, 796, DateTimeKind.Local).AddTicks(9199), "", "System Admin", true, null, null, "123", "", "Admin", "admin" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2026, 5, 1, 15, 17, 8, 796, DateTimeKind.Local).AddTicks(9217), "", "Project Manager", true, null, null, "123", "", "Manager", "manager" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2026, 5, 1, 15, 17, 8, 796, DateTimeKind.Local).AddTicks(9221), "", "Ahmad Employee", true, null, null, "123", "", "Employee", "ahmad" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2026, 5, 1, 15, 17, 8, 796, DateTimeKind.Local).AddTicks(9225), "", "Tech Staff", true, null, null, "123", "", "Staff", "staff" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "CategoryId", "CreatedBy", "CreatedDate", "Description", "LastUpdatedBy", "LastUpdatedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("d1111111-1111-1111-1111-111111111111"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2026, 5, 1, 15, 17, 8, 796, DateTimeKind.Local).AddTicks(9502), "Pointing devices", null, null, "Mouse" },
                    { new Guid("d2222222-2222-2222-2222-222222222222"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2026, 5, 1, 15, 17, 8, 796, DateTimeKind.Local).AddTicks(9505), "Display screens", null, null, "Monitor" },
                    { new Guid("d3333333-3333-3333-3333-333333333333"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2026, 5, 1, 15, 17, 8, 796, DateTimeKind.Local).AddTicks(9508), "OS Activation", null, null, "Windows License" }
                });

            migrationBuilder.InsertData(
                table: "Requests",
                columns: new[] { "Id", "AssignedStaffId", "CategoryItemId", "CreatedBy", "CreatedDate", "Description", "LastUpdatedBy", "LastUpdatedDate", "RejectionReason", "Status", "Title" },
                values: new object[,]
                {
                    { new Guid("6c5db5ca-9f81-489f-b1c0-ea482936cfd4"), null, new Guid("d1111111-1111-1111-1111-111111111111"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 5, 1, 15, 17, 8, 796, DateTimeKind.Local).AddTicks(9584), "Left click is not responding", null, null, null, "New", "Broken Mouse" },
                    { new Guid("a04a6597-f7ec-4fff-8e8c-2daa52bb2948"), new Guid("44444444-4444-4444-4444-444444444444"), new Guid("d2222222-2222-2222-2222-222222222222"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 5, 1, 13, 17, 8, 796, DateTimeKind.Local).AddTicks(9590), "Monitor screen keeps turning off", null, null, null, "Assigned", "Screen Flickering" },
                    { new Guid("ea8a56ca-faf9-4d0f-a32b-bd66a35c4b83"), new Guid("44444444-4444-4444-4444-444444444444"), new Guid("d3333333-3333-3333-3333-333333333333"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 4, 30, 15, 17, 8, 796, DateTimeKind.Local).AddTicks(9600), "Need to activate Windows 11", null, null, null, "InProgress", "Windows Activation" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_RequestId",
                table: "Attachments",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_UserId",
                table: "Attachments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_RequestId",
                table: "Comments",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CategoryId",
                table: "Items",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_AssignedStaffId",
                table: "Requests",
                column: "AssignedStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CategoryItemId",
                table: "Requests",
                column: "CategoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CreatedBy",
                table: "Requests",
                column: "CreatedBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
