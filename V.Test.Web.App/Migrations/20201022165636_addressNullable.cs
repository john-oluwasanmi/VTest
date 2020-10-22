using Microsoft.EntityFrameworkCore.Migrations;

namespace V.Test.Web.App.Migrations
{
    public partial class addressNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "Organisation",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "Organisation",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
