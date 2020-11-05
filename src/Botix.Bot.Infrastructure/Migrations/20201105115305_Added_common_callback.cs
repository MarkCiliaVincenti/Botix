using Microsoft.EntityFrameworkCore.Migrations;

namespace Botix.Bot.Infrastructure.Migrations
{
    public partial class Added_common_callback : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MessageCallBack",
                schema: "api",
                table: "CallBackGroups",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MessageCallBack",
                schema: "api",
                table: "CallBackGroups");
        }
    }
}
