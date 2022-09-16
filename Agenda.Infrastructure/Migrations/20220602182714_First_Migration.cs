using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agenda.Infrastructure.Migrations
{
    public partial class First_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InteractionTypes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InteractionTypes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PhoneTypes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneTypes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_user",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    userName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UserRoleId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_user", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_user_UserRoles_UserRoleId",
                        column: x => x.UserRoleId,
                        principalTable: "UserRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_contact",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_contact", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_contact_tb_user_UserId",
                        column: x => x.UserId,
                        principalTable: "tb_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_interaction",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    InteractionTypeId = table.Column<int>(type: "int", nullable: false),
                    message = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_interaction", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_interaction_InteractionTypes_InteractionTypeId",
                        column: x => x.InteractionTypeId,
                        principalTable: "InteractionTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_interaction_tb_user_UserId",
                        column: x => x.UserId,
                        principalTable: "tb_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_phone",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    number = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    ddd = table.Column<int>(type: "int", maxLength: 2, nullable: false),
                    formattedNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContactId = table.Column<int>(type: "int", nullable: false),
                    PhoneTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_phone", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_phone_PhoneTypes_PhoneTypeId",
                        column: x => x.PhoneTypeId,
                        principalTable: "PhoneTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_phone_tb_contact_ContactId",
                        column: x => x.ContactId,
                        principalTable: "tb_contact",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "InteractionTypes",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Create Contact" },
                    { 2, "Update Contact" },
                    { 3, "Delete Contact" },
                    { 4, "View Contact" },
                    { 5, "View Phones" }
                });

            migrationBuilder.InsertData(
                table: "PhoneTypes",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Residencial" },
                    { 2, "Cellphone" },
                    { 3, "Commercial" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Common" }
                });

            migrationBuilder.InsertData(
                table: "tb_user",
                columns: new[] { "id", "CreatedAt", "email", "name", "password", "UpdatedAt", "UserRoleId", "userName" },
                values: new object[] { 1, new DateTime(2022, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@api.com", "Admin Root Application", "AQAAAAEAAAPoAAAAEIPCxPofH8YJsYljlF4SGhisL9z4jIuhMOc+WTKFZb28XNSRMXUmraNC2WYwd9vYHQ==", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_tb_contact_UserId",
                table: "tb_contact",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_interaction_InteractionTypeId",
                table: "tb_interaction",
                column: "InteractionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_interaction_UserId",
                table: "tb_interaction",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_phone_ContactId",
                table: "tb_phone",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_phone_PhoneTypeId",
                table: "tb_phone",
                column: "PhoneTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_user_UserRoleId",
                table: "tb_user",
                column: "UserRoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_interaction");

            migrationBuilder.DropTable(
                name: "tb_phone");

            migrationBuilder.DropTable(
                name: "InteractionTypes");

            migrationBuilder.DropTable(
                name: "PhoneTypes");

            migrationBuilder.DropTable(
                name: "tb_contact");

            migrationBuilder.DropTable(
                name: "tb_user");

            migrationBuilder.DropTable(
                name: "UserRoles");
        }
    }
}
