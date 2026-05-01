using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ServiceRequestMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class editSeededdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Requests",
                keyColumn: "Id",
                keyValue: new Guid("6c5db5ca-9f81-489f-b1c0-ea482936cfd4"));

            migrationBuilder.DeleteData(
                table: "Requests",
                keyColumn: "Id",
                keyValue: new Guid("a04a6597-f7ec-4fff-8e8c-2daa52bb2948"));

            migrationBuilder.DeleteData(
                table: "Requests",
                keyColumn: "Id",
                keyValue: new Guid("ea8a56ca-faf9-4d0f-a32b-bd66a35c4b83"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                value: new DateTime(2026, 5, 1, 15, 17, 8, 796, DateTimeKind.Local).AddTicks(9466));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "CreatedDate",
                value: new DateTime(2026, 5, 1, 15, 17, 8, 796, DateTimeKind.Local).AddTicks(9470));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("d1111111-1111-1111-1111-111111111111"),
                column: "CreatedDate",
                value: new DateTime(2026, 5, 1, 15, 17, 8, 796, DateTimeKind.Local).AddTicks(9502));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("d2222222-2222-2222-2222-222222222222"),
                column: "CreatedDate",
                value: new DateTime(2026, 5, 1, 15, 17, 8, 796, DateTimeKind.Local).AddTicks(9505));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("d3333333-3333-3333-3333-333333333333"),
                column: "CreatedDate",
                value: new DateTime(2026, 5, 1, 15, 17, 8, 796, DateTimeKind.Local).AddTicks(9508));

            migrationBuilder.InsertData(
                table: "Requests",
                columns: new[] { "Id", "AssignedStaffId", "CategoryItemId", "CreatedBy", "CreatedDate", "Description", "LastUpdatedBy", "LastUpdatedDate", "RejectionReason", "Status", "Title" },
                values: new object[,]
                {
                    { new Guid("6c5db5ca-9f81-489f-b1c0-ea482936cfd4"), null, new Guid("d1111111-1111-1111-1111-111111111111"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 5, 1, 15, 17, 8, 796, DateTimeKind.Local).AddTicks(9584), "Left click is not responding", null, null, null, "New", "Broken Mouse" },
                    { new Guid("a04a6597-f7ec-4fff-8e8c-2daa52bb2948"), new Guid("44444444-4444-4444-4444-444444444444"), new Guid("d2222222-2222-2222-2222-222222222222"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 5, 1, 13, 17, 8, 796, DateTimeKind.Local).AddTicks(9590), "Monitor screen keeps turning off", null, null, null, "Assigned", "Screen Flickering" },
                    { new Guid("ea8a56ca-faf9-4d0f-a32b-bd66a35c4b83"), new Guid("44444444-4444-4444-4444-444444444444"), new Guid("d3333333-3333-3333-3333-333333333333"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 4, 30, 15, 17, 8, 796, DateTimeKind.Local).AddTicks(9600), "Need to activate Windows 11", null, null, null, "InProgress", "Windows Activation" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedDate", "PasswordHash" },
                values: new object[] { new DateTime(2026, 5, 1, 15, 17, 8, 796, DateTimeKind.Local).AddTicks(9199), "123" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedDate", "PasswordHash" },
                values: new object[] { new DateTime(2026, 5, 1, 15, 17, 8, 796, DateTimeKind.Local).AddTicks(9217), "123" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedDate", "PasswordHash" },
                values: new object[] { new DateTime(2026, 5, 1, 15, 17, 8, 796, DateTimeKind.Local).AddTicks(9221), "123" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedDate", "PasswordHash" },
                values: new object[] { new DateTime(2026, 5, 1, 15, 17, 8, 796, DateTimeKind.Local).AddTicks(9225), "123" });
        }
    }
}
