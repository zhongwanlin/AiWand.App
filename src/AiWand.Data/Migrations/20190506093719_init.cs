﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AiWand.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CodeDocument",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 32, nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Reviser = table.Column<string>(nullable: true),
                    ReviseTime = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    DocumentNo = table.Column<string>(nullable: true),
                    DocumentName = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    CompanyName = table.Column<string>(nullable: true),
                    CompanyAbb = table.Column<string>(nullable: true),
                    Language = table.Column<string>(nullable: true),
                    DownloadTimes = table.Column<int>(nullable: false),
                    DocumentStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeDocument", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CodePath",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 32, nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Reviser = table.Column<string>(nullable: true),
                    ReviseTime = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    DocumentId = table.Column<string>(nullable: true),
                    CodeFilePath = table.Column<string>(nullable: true),
                    CodeFile = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodePath", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 32, nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Reviser = table.Column<string>(nullable: true),
                    ReviseTime = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    RegisterTime = table.Column<DateTime>(nullable: false),
                    LoginTime = table.Column<DateTime>(nullable: true),
                    IP = table.Column<string>(nullable: true),
                    LastLoginTime = table.Column<DateTime>(nullable: true),
                    LastIP = table.Column<string>(nullable: true),
                    RealName = table.Column<string>(nullable: true),
                    HeadImage = table.Column<string>(nullable: true),
                    UserType = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CodeDocument");

            migrationBuilder.DropTable(
                name: "CodePath");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
