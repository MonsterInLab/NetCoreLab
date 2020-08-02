using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.EFCCore31.Migrations
{
    public partial class InitCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SysUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Password = table.Column<string>(maxLength: 64, nullable: false),
                    Status = table.Column<byte>(nullable: false),
                    Phone = table.Column<string>(maxLength: 20, nullable: true),
                    Mobile = table.Column<string>(maxLength: 20, nullable: true),
                    Address = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    QQ = table.Column<long>(nullable: true),
                    WeChat = table.Column<string>(maxLength: 50, nullable: true),
                    Sex = table.Column<byte>(nullable: true),
                    LastLoginTime = table.Column<DateTime>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateId = table.Column<int>(nullable: false),
                    LastModifyTime = table.Column<DateTime>(nullable: true),
                    LastModifyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysUser", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SysUser");
        }
    }
}
