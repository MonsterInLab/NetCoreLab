using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.EFCCore31.Migrations
{
    public partial class InitCreate02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(maxLength: 100, nullable: true),
                    ParentCode = table.Column<string>(maxLength: 100, nullable: true),
                    CategoryLevel = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Url = table.Column<string>(maxLength: 1000, nullable: true),
                    State = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 500, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<int>(nullable: false),
                    LastModifierId = table.Column<int>(nullable: true),
                    LastModifyTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysLog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: false),
                    Introduction = table.Column<string>(maxLength: 1000, nullable: false),
                    Detail = table.Column<string>(maxLength: 4000, nullable: true),
                    LogType = table.Column<byte>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<int>(nullable: false),
                    LastModifyTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysMenu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(maxLength: 100, nullable: false),
                    Url = table.Column<string>(maxLength: 500, nullable: true),
                    MenuLevel = table.Column<byte>(nullable: false),
                    MenuType = table.Column<byte>(nullable: false),
                    MenuIcon = table.Column<string>(maxLength: 20, nullable: true),
                    Description = table.Column<string>(maxLength: 100, nullable: true),
                    SourcePath = table.Column<string>(maxLength: 1000, nullable: true),
                    Sort = table.Column<int>(nullable: false),
                    Status = table.Column<byte>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<int>(nullable: false),
                    LastModifyTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysMenu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysRole",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(maxLength: 36, nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Status = table.Column<byte>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateId = table.Column<int>(nullable: false),
                    LastModifyTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysUserMenuMapping",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SysUserId = table.Column<int>(nullable: false),
                    SysMenuId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysUserMenuMapping", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysUserRoleMapping",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SysUserId = table.Column<int>(nullable: false),
                    SysRoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysUserRoleMapping", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Account = table.Column<string>(maxLength: 100, nullable: false),
                    Password = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 200, nullable: true),
                    Mobile = table.Column<string>(maxLength: 30, nullable: true),
                    CompanyId = table.Column<int>(nullable: false),
                    CompanyName = table.Column<string>(maxLength: 500, nullable: true),
                    State = table.Column<int>(nullable: false),
                    UserType = table.Column<int>(nullable: false),
                    LastLoginTime = table.Column<DateTime>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<int>(nullable: false),
                    LastModifierId = table.Column<int>(nullable: true),
                    LastModifyTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SysRoleMenuMapping",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SysRoleId = table.Column<int>(nullable: false),
                    SysMenuId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysRoleMenuMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysRoleMenuMapping_SysMenu_SysMenuId",
                        column: x => x.SysMenuId,
                        principalTable: "SysMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SysRoleMenuMapping_SysRole_SysRoleId",
                        column: x => x.SysRoleId,
                        principalTable: "SysRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SysRoleMenuMapping_SysMenuId",
                table: "SysRoleMenuMapping",
                column: "SysMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_SysRoleMenuMapping_SysRoleId",
                table: "SysRoleMenuMapping",
                column: "SysRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_User_CompanyId",
                table: "User",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "SysLog");

            migrationBuilder.DropTable(
                name: "SysRoleMenuMapping");

            migrationBuilder.DropTable(
                name: "SysUserMenuMapping");

            migrationBuilder.DropTable(
                name: "SysUserRoleMapping");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "SysMenu");

            migrationBuilder.DropTable(
                name: "SysRole");

            migrationBuilder.DropTable(
                name: "Company");
        }
    }
}
