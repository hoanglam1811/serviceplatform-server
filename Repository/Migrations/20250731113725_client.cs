using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class client : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrentCondition",
                table: "Clients",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TargetGoal",
                table: "Clients",
                type: "longtext",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentCondition",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "TargetGoal",
                table: "Clients");
        }
    }
}
