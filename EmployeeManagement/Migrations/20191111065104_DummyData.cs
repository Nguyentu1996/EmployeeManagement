using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagement.Migrations
{
    public partial class DummyData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "Id", "IsDelete", "Name" },
                values: new object[] { 1, false, "Kinh doanh" });

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "Id", "IsDelete", "Name" },
                values: new object[] { 2, false, "Nhân sự" });

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "Id", "IsDelete", "Name" },
                values: new object[] { 3, false, "Lập trình" });

            migrationBuilder.InsertData(
                table: "Position",
                columns: new[] { "Id", "IsDelete", "Name" },
                values: new object[] { 1, false, "Nhân viên" });

            migrationBuilder.InsertData(
                table: "Position",
                columns: new[] { "Id", "IsDelete", "Name" },
                values: new object[] { 2, false, "Phó phòng" });

            migrationBuilder.InsertData(
                table: "Position",
                columns: new[] { "Id", "IsDelete", "Name" },
                values: new object[] { 3, false, "Trưởng phòng" });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "Id", "Address", "CreateDate", "DepartmentId", "DOB", "EditDate", "Email", "FullName", "IdNumber", "Image", "IsDelete", "PhoneNumber", "PositionId", "Sex", "TaxId" },
                values: new object[] { 3, "Huế", new DateTime(2019, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2000, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "tri@gmail.com", "Trí", "2521425125", null, false, "0785425425", 1, true, "021323652" });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "Id", "Address", "CreateDate", "DepartmentId", "DOB", "EditDate", "Email", "FullName", "IdNumber", "Image", "IsDelete", "PhoneNumber", "PositionId", "Sex", "TaxId" },
                values: new object[] { 4, "Huế", new DateTime(2019, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2000, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "nhan@gmail.com", "Nhân", "2122225254", null, false, "0765854752", 1, true, "232114225" });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "Id", "Address", "CreateDate", "DepartmentId", "DOB", "EditDate", "Email", "FullName", "IdNumber", "Image", "IsDelete", "PhoneNumber", "PositionId", "Sex", "TaxId" },
                values: new object[] { 5, "Huế", new DateTime(2019, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(1990, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "bao@gmail.com", "Báo", "2121252142", null, false, "0325124256", 1, true, "322521242" });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "Id", "Address", "CreateDate", "DepartmentId", "DOB", "EditDate", "Email", "FullName", "IdNumber", "Image", "IsDelete", "PhoneNumber", "PositionId", "Sex", "TaxId" },
                values: new object[] { 2, "Huế", new DateTime(2019, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2000, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "tu@gmail.com", "Tú", "2021252125", null, false, "0325122422", 2, true, "325125425" });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "Id", "Address", "CreateDate", "DepartmentId", "DOB", "EditDate", "Email", "FullName", "IdNumber", "Image", "IsDelete", "PhoneNumber", "PositionId", "Sex", "TaxId" },
                values: new object[] { 1, "Huế", new DateTime(2019, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(1995, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "lnpttor@gmail.com", "Lê Nguyễn Phước Thành", "2514254253", null, false, "0325123657", 3, true, "231251254" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
