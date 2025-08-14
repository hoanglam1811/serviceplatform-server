using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class addCustomerRegis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "CustomerRegistrations",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrainingExperience",
                table: "CustomerRegistrations",
                type: "longtext",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "CustomerRegistrations");

            migrationBuilder.DropColumn(
                name: "TrainingExperience",
                table: "CustomerRegistrations");
        }
    }
}
