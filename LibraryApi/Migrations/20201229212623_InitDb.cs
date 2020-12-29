using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryApi.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SureName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Isbn = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PageCount = table.Column<int>(type: "int", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Books_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryId",
                table: "Books",
                column: "CategoryId");
            var sp = @"CREATE PROCEDURE [dbo].[GetCategory]          
                AS
                BEGIN
                    SET NOCOUNT ON;
               	    SELECT * from Categories
                END";

            migrationBuilder.Sql(sp);
            var data = @"USE [Library]
                   GO
                   SET IDENTITY_INSERT [dbo].[Authors] ON 
                   GO
                   INSERT [dbo].[Authors] ([Id], [Name], [SureName], [BirthDate], [CreateAt], [EditAt]) VALUES (3, N'amir', N'karamian', CAST(N'1990-12-30T00:28:19.2609884' AS DateTime2), CAST(N'2020-12-30T00:28:19.2649572' AS DateTime2), NULL)
                   GO
                   SET IDENTITY_INSERT [dbo].[Authors] OFF
                   GO
                   SET IDENTITY_INSERT [dbo].[Categories] ON 
                   GO
                   INSERT [dbo].[Categories] ([Id], [CategoryName], [CreateAt], [EditAt]) VALUES (7, N'Category1', CAST(N'2020-12-30T00:28:19.2650620' AS DateTime2), NULL)
                   GO
                   INSERT [dbo].[Categories] ([Id], [CategoryName], [CreateAt], [EditAt]) VALUES (8, N'Category2', CAST(N'2020-12-30T00:28:19.2652426' AS DateTime2), NULL)
                   GO
                   INSERT [dbo].[Categories] ([Id], [CategoryName], [CreateAt], [EditAt]) VALUES (9, N'Category3', CAST(N'2020-12-30T00:28:19.2652447' AS DateTime2), NULL)
                   GO
                   SET IDENTITY_INSERT [dbo].[Categories] OFF
                   GO
                   SET IDENTITY_INSERT [dbo].[Books] ON 
                   GO
                   INSERT [dbo].[Books] ([Id], [Name], [Isbn], [PageCount], [PublishDate], [AuthorId], [CategoryId], [CreateAt], [EditAt]) VALUES (2, N'book1', N'123', 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 3, 7, CAST(N'2020-12-30T00:28:19.2651854' AS DateTime2), NULL)
                   GO
                   INSERT [dbo].[Books] ([Id], [Name], [Isbn], [PageCount], [PublishDate], [AuthorId], [CategoryId], [CreateAt], [EditAt]) VALUES (3, N'book2', N'1234', 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 3, 7, CAST(N'2020-12-30T00:28:19.2652438' AS DateTime2), NULL)
                   GO
                   INSERT [dbo].[Books] ([Id], [Name], [Isbn], [PageCount], [PublishDate], [AuthorId], [CategoryId], [CreateAt], [EditAt]) VALUES (4, N'book3', N'1235', 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 3, 7, CAST(N'2020-12-30T00:28:19.2652450' AS DateTime2), NULL)
                   GO
                   SET IDENTITY_INSERT [dbo].[Books] OFF
                   GO";
            migrationBuilder.Sql(data);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
