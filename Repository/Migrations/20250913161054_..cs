using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class _ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropIndex(
            //    name: "IX_ProviderProfiles_UserId",
            //    table: "ProviderProfiles");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ProviderProfiles_UserId",
            //    table: "ProviderProfiles",
            //    column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropIndex(
            //    name: "IX_ProviderProfiles_UserId",
            //    table: "ProviderProfiles");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ProviderProfiles_UserId",
            //    table: "ProviderProfiles",
            //    column: "UserId",
            //    unique: true);
        }
    }
}
