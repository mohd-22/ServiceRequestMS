using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ServiceRequestMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class editSeededdataAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Requests",
                keyColumn: "Id",
                keyValue: new Guid("a9e978f3-9d1b-41b8-9be6-b89e0c94a742"));

            migrationBuilder.DeleteData(
                table: "Requests",
                keyColumn: "Id",
                keyValue: new Guid("bb8c7360-e652-46a3-b3f5-b509c58871f3"));

            migrationBuilder.DeleteData(
                table: "Requests",
                keyColumn: "Id",
                keyValue: new Guid("cd159ed5-97be-41c4-a7c1-fb519c4a6849"));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                column: "CreatedDate",
                value: new DateTime(2026, 5, 1, 15, 28, 17, 440, DateTimeKind.Local).AddTicks(7752));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "CreatedDate",
                value: new DateTime(2026, 5, 1, 15, 28, 17, 440, DateTimeKind.Local).AddTicks(7755));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("d1111111-1111-1111-1111-111111111111"),
                column: "CreatedDate",
                value: new DateTime(2026, 5, 1, 15, 28, 17, 440, DateTimeKind.Local).AddTicks(7780));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("d2222222-2222-2222-2222-222222222222"),
                column: "CreatedDate",
                value: new DateTime(2026, 5, 1, 15, 28, 17, 440, DateTimeKind.Local).AddTicks(7783));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("d3333333-3333-3333-3333-333333333333"),
                column: "CreatedDate",
                value: new DateTime(2026, 5, 1, 15, 28, 17, 440, DateTimeKind.Local).AddTicks(7785));

            migrationBuilder.InsertData(
                table: "Requests",
                columns: new[] { "Id", "AssignedStaffId", "CategoryItemId", "CreatedBy", "CreatedDate", "Description", "LastUpdatedBy", "LastUpdatedDate", "RejectionReason", "Status", "Title" },
                values: new object[,]
                {
                    { new Guid("0421ab5f-b430-4da4-a2d6-7635ee3a5c65"), new Guid("44444444-4444-4444-4444-444444444444"), new Guid("d3333333-3333-3333-3333-333333333333"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 4, 30, 15, 28, 17, 440, DateTimeKind.Local).AddTicks(8088), "Need to activate Windows 11", null, null, null, "InProgress", "Windows Activation" },
                    { new Guid("11034153-decd-48a5-a378-f7bf5dd19aba"), null, new Guid("d1111111-1111-1111-1111-111111111111"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 5, 1, 15, 28, 17, 440, DateTimeKind.Local).AddTicks(8070), "Left click is not responding", null, null, null, "New", "Broken Mouse" },
                    { new Guid("a620ca8a-82cd-4b1a-a4fd-ec2db8fe8c2c"), new Guid("44444444-4444-4444-4444-444444444444"), new Guid("d2222222-2222-2222-2222-222222222222"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 5, 1, 13, 28, 17, 440, DateTimeKind.Local).AddTicks(8079), "Monitor screen keeps turning off", null, null, null, "Assigned", "Screen Flickering" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedDate", "PasswordHash" },
                values: new object[] { new DateTime(2026, 5, 1, 15, 28, 17, 440, DateTimeKind.Local).AddTicks(7551), "AQAAAAIAAYagAAAAEKkB6aXFw8CerrUrN0OsWO0pBbCJt/mSGfsTJ9XMP0kCkUiuUZbTHez2JbMQ36JSLA==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedDate", "PasswordHash" },
                values: new object[] { new DateTime(2026, 5, 1, 15, 28, 17, 440, DateTimeKind.Local).AddTicks(7575), "AQAAAAIAAYagAAAAEKkB6aXFw8CerrUrN0OsWO0pBbCJt/mSGfsTJ9XMP0kCkUiuUZbTHez2JbMQ36JSLA==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedDate", "PasswordHash" },
                values: new object[] { new DateTime(2026, 5, 1, 15, 28, 17, 440, DateTimeKind.Local).AddTicks(7579), "AQAAAAIAAYagAAAAEKkB6aXFw8CerrUrN0OsWO0pBbCJt/mSGfsTJ9XMP0kCkUiuUZbTHez2JbMQ36JSLA==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedDate", "PasswordHash" },
                values: new object[] { new DateTime(2026, 5, 1, 15, 28, 17, 440, DateTimeKind.Local).AddTicks(7582), "AQAAAAIAAYagAAAAEKkB6aXFw8CerrUrN0OsWO0pBbCJt/mSGfsTJ9XMP0kCkUiuUZbTHez2JbMQ36JSLA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Requests",
                keyColumn: "Id",
                keyValue: new Guid("0421ab5f-b430-4da4-a2d6-7635ee3a5c65"));

            migrationBuilder.DeleteData(
                table: "Requests",
                keyColumn: "Id",
                keyValue: new Guid("11034153-decd-48a5-a378-f7bf5dd19aba"));

            migrationBuilder.DeleteData(
                table: "Requests",
                keyColumn: "Id",
                keyValue: new Guid("a620ca8a-82cd-4b1a-a4fd-ec2db8fe8c2c"));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                column: "CreatedDate",
                value: new DateTime(2026, 5, 1, 15, 20, 27, 525, DateTimeKind.Local).AddTicks(5896));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "CreatedDate",
                value: new DateTime(2026, 5, 1, 15, 20, 27, 525, DateTimeKind.Local).AddTicks(5901));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("d1111111-1111-1111-1111-111111111111"),
                column: "CreatedDate",
                value: new DateTime(2026, 5, 1, 15, 20, 27, 525, DateTimeKind.Local).AddTicks(5943));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("d2222222-2222-2222-2222-222222222222"),
                column: "CreatedDate",
                value: new DateTime(2026, 5, 1, 15, 20, 27, 525, DateTimeKind.Local).AddTicks(5946));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("d3333333-3333-3333-3333-333333333333"),
                column: "CreatedDate",
                value: new DateTime(2026, 5, 1, 15, 20, 27, 525, DateTimeKind.Local).AddTicks(5949));

            migrationBuilder.InsertData(
                table: "Requests",
                columns: new[] { "Id", "AssignedStaffId", "CategoryItemId", "CreatedBy", "CreatedDate", "Description", "LastUpdatedBy", "LastUpdatedDate", "RejectionReason", "Status", "Title" },
                values: new object[,]
                {
                    { new Guid("a9e978f3-9d1b-41b8-9be6-b89e0c94a742"), new Guid("44444444-4444-4444-4444-444444444444"), new Guid("d2222222-2222-2222-2222-222222222222"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 5, 1, 13, 20, 27, 525, DateTimeKind.Local).AddTicks(6037), "Monitor screen keeps turning off", null, null, null, "Assigned", "Screen Flickering" },
                    { new Guid("bb8c7360-e652-46a3-b3f5-b509c58871f3"), new Guid("44444444-4444-4444-4444-444444444444"), new Guid("d3333333-3333-3333-3333-333333333333"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 4, 30, 15, 20, 27, 525, DateTimeKind.Local).AddTicks(6048), "Need to activate Windows 11", null, null, null, "InProgress", "Windows Activation" },
                    { new Guid("cd159ed5-97be-41c4-a7c1-fb519c4a6849"), null, new Guid("d1111111-1111-1111-1111-111111111111"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 5, 1, 15, 20, 27, 525, DateTimeKind.Local).AddTicks(6023), "Left click is not responding", null, null, null, "New", "Broken Mouse" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedDate", "PasswordHash" },
                values: new object[] { new DateTime(2026, 5, 1, 15, 20, 27, 525, DateTimeKind.Local).AddTicks(5020), "123456" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedDate", "PasswordHash" },
                values: new object[] { new DateTime(2026, 5, 1, 15, 20, 27, 525, DateTimeKind.Local).AddTicks(5039), "123456" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedDate", "PasswordHash" },
                values: new object[] { new DateTime(2026, 5, 1, 15, 20, 27, 525, DateTimeKind.Local).AddTicks(5163), "123456" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedDate", "PasswordHash" },
                values: new object[] { new DateTime(2026, 5, 1, 15, 20, 27, 525, DateTimeKind.Local).AddTicks(5167), "123456" });
        }
    }
}
