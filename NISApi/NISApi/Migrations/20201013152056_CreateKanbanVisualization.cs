using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NISApi.Migrations
{
    public partial class CreateKanbanVisualization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonTasks",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    DeletedById = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DeletedBy = table.Column<string>(nullable: true),
                    Action = table.Column<string>(maxLength: 1, nullable: true),
                    CreatedDateTimeUtc = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedDateTimeUtc = table.Column<DateTimeOffset>(nullable: true),
                    DeletedDateTimeUtc = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Title = table.Column<string>(nullable: false),
                    Status = table.Column<string>(maxLength: 50, nullable: false),
                    Summary = table.Column<string>(nullable: true),
                    TaskType = table.Column<string>(maxLength: 50, nullable: false),
                    Priority = table.Column<string>(maxLength: 50, nullable: false),
                    ReferenceEntity = table.Column<string>(nullable: true),
                    ReferenceNumber = table.Column<string>(nullable: true),
                    ReferenceDate = table.Column<DateTimeOffset>(nullable: true),
                    DateToBeCompleted = table.Column<DateTimeOffset>(nullable: true),
                    Colour = table.Column<string>(nullable: true),
                    User = table.Column<string>(nullable: true),
                    UserID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonTasks", x => x.ID);
                    table.UniqueConstraint("AK_PersonTasks_UserID_ID", x => new { x.UserID, x.ID });
                });

            migrationBuilder.CreateTable(
                name: "TableTaskPriority",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    DeletedById = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DeletedBy = table.Column<string>(nullable: true),
                    Action = table.Column<string>(maxLength: 1, nullable: true),
                    CreatedDateTimeUtc = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedDateTimeUtc = table.Column<DateTimeOffset>(nullable: true),
                    DeletedDateTimeUtc = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Code = table.Column<string>(maxLength: 10, nullable: false),
                    ShortDescription = table.Column<string>(maxLength: 50, nullable: false),
                    LongDescription = table.Column<string>(maxLength: 90, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableTaskPriority", x => x.ID);
                    table.UniqueConstraint("AK_TableTaskPriority_Code", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "TableTaskReferenceType",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    DeletedById = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DeletedBy = table.Column<string>(nullable: true),
                    Action = table.Column<string>(maxLength: 1, nullable: true),
                    CreatedDateTimeUtc = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedDateTimeUtc = table.Column<DateTimeOffset>(nullable: true),
                    DeletedDateTimeUtc = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Code = table.Column<string>(maxLength: 10, nullable: false),
                    ShortDescription = table.Column<string>(maxLength: 50, nullable: false),
                    LongDescription = table.Column<string>(maxLength: 90, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableTaskReferenceType", x => x.ID);
                    table.UniqueConstraint("AK_TableTaskReferenceType_Code", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "TableTaskStatus",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    DeletedById = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DeletedBy = table.Column<string>(nullable: true),
                    Action = table.Column<string>(maxLength: 1, nullable: true),
                    CreatedDateTimeUtc = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedDateTimeUtc = table.Column<DateTimeOffset>(nullable: true),
                    DeletedDateTimeUtc = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Code = table.Column<string>(maxLength: 10, nullable: false),
                    ShortDescription = table.Column<string>(maxLength: 50, nullable: false),
                    LongDescription = table.Column<string>(maxLength: 90, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableTaskStatus", x => x.ID);
                    table.UniqueConstraint("AK_TableTaskStatus_Code", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "TableTaskType",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    DeletedById = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DeletedBy = table.Column<string>(nullable: true),
                    Action = table.Column<string>(maxLength: 1, nullable: true),
                    CreatedDateTimeUtc = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedDateTimeUtc = table.Column<DateTimeOffset>(nullable: true),
                    DeletedDateTimeUtc = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Code = table.Column<string>(maxLength: 10, nullable: false),
                    ShortDescription = table.Column<string>(maxLength: 50, nullable: false),
                    LongDescription = table.Column<string>(maxLength: 90, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableTaskType", x => x.ID);
                    table.UniqueConstraint("AK_TableTaskType_Code", x => x.Code);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonTasks_UserID",
                table: "PersonTasks",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TableTaskPriority_ShortDescription",
                table: "TableTaskPriority",
                column: "ShortDescription");

            migrationBuilder.CreateIndex(
                name: "IX_TableTaskReferenceType_ShortDescription",
                table: "TableTaskReferenceType",
                column: "ShortDescription");

            migrationBuilder.CreateIndex(
                name: "IX_TableTaskStatus_ShortDescription",
                table: "TableTaskStatus",
                column: "ShortDescription");

            migrationBuilder.CreateIndex(
                name: "IX_TableTaskType_ShortDescription",
                table: "TableTaskType",
                column: "ShortDescription");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonTasks");

            migrationBuilder.DropTable(
                name: "TableTaskPriority");

            migrationBuilder.DropTable(
                name: "TableTaskReferenceType");

            migrationBuilder.DropTable(
                name: "TableTaskStatus");

            migrationBuilder.DropTable(
                name: "TableTaskType");
        }
    }
}
