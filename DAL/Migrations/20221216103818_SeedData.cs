using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Prive",
                table: "Favors",
                newName: "Price");

            migrationBuilder.InsertData(
                table: "FavorNames",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Первинна консультація" },
                    { 2, "Повторна консультація" },
                    { 3, "Лабораторні дослідження" },
                    { 4, "Ультразвукова діагностика" },
                    { 5, "Рентгенографія" },
                    { 6, "Комп'ютерна томографія" }
                });

            migrationBuilder.InsertData(
                table: "FavorTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Консультація" },
                    { 2, "Діагностика" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "FullName", "Phone" },
                values: new object[,]
                {
                    { 1, "Петренко Петро", "380961234567" },
                    { 2, "Іваненко Іван", "380501234567" },
                    { 3, "Семененко Семен", "380661234567" },
                    { 4, "Ольченко Ольга", "380931234567" }
                });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "Дослідження та діагностика" },
                    { 2, "Терапія" },
                    { 3, "Хірургія" },
                    { 4, "Педіатрія" },
                    { 5, "Дерматологія" },
                    { 6, "Психотерапія" }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "Experience", "FirstName", "LastName", "MiddleName", "SpecializationId" },
                values: new object[,]
                {
                    { 1, 3, "Анна", "Мельник", "Олексіївна", 1 },
                    { 2, 10, "Юрій", "Дрогобич", "Михайлович", 2 },
                    { 3, 15, "Микола", "Амосов", "Михайлович", 3 },
                    { 4, 12, "Олександр", "Тур", "Федорович", 4 },
                    { 5, 7, "Сергій", "Шевченко", "Олександрович", 5 },
                    { 6, 9, "Ольга", "Кравчук", "Ігорівна", 6 }
                });

            migrationBuilder.InsertData(
                table: "Favors",
                columns: new[] { "Id", "FavorNameId", "FavorTypeId", "Price", "SpecializationId" },
                values: new object[,]
                {
                    { 1, 3, 2, 200m, 1 },
                    { 2, 4, 2, 500m, 1 },
                    { 3, 5, 2, 400m, 1 },
                    { 4, 6, 2, 700m, 1 },
                    { 5, 1, 1, 350m, 2 },
                    { 6, 2, 1, 300m, 2 },
                    { 7, 1, 1, 350m, 3 },
                    { 8, 2, 1, 300m, 3 },
                    { 9, 1, 1, 300m, 4 },
                    { 10, 2, 1, 250m, 4 },
                    { 11, 1, 1, 350m, 5 },
                    { 12, 2, 1, 300m, 5 },
                    { 13, 1, 1, 400m, 6 },
                    { 14, 2, 1, 350m, 6 }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "Date", "DoctorId", "FavorId", "PatientId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 12, 12, 15, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 1 },
                    { 2, new DateTime(2022, 12, 12, 16, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, 1 },
                    { 3, new DateTime(2022, 12, 12, 17, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, 2 },
                    { 4, new DateTime(2022, 12, 12, 17, 0, 0, 0, DateTimeKind.Unspecified), 2, 5, 1 },
                    { 5, new DateTime(2022, 12, 12, 17, 0, 0, 0, DateTimeKind.Unspecified), 3, 7, 2 },
                    { 6, new DateTime(2022, 12, 12, 17, 0, 0, 0, DateTimeKind.Unspecified), 4, 9, 3 },
                    { 7, new DateTime(2022, 12, 12, 17, 0, 0, 0, DateTimeKind.Unspecified), 5, 11, 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Favors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Favors",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Favors",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Favors",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Favors",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Favors",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Favors",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "FavorNames",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FavorNames",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Favors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Favors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Favors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Favors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Favors",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Favors",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Favors",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "FavorNames",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FavorNames",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FavorNames",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FavorNames",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "FavorTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FavorTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Favors",
                newName: "Prive");
        }
    }
}
