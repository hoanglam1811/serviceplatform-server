using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class a2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Add new column (adjust type, length, nullability to match the old one)
            migrationBuilder.AddColumn<string>(
                name: "BusinessPhone",
                table: "ProviderProfiles",
                type: "varchar(255)", // adjust this to your actual schema
                nullable: true);

            // 3. Drop the old column
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "ProviderProfiles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "ProviderProfiles",
                type: "varchar(255)", // same type as original
                nullable: true);

            migrationBuilder.DropColumn(
                name: "BusinessPhone",
                table: "ProviderProfiles");
        }
    }
}
