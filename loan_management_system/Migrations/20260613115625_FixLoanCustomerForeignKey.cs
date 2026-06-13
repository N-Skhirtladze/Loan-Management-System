using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace loan_management_system.Migrations
{
    /// <inheritdoc />
    public partial class FixLoanCustomerForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Customers_CustomersID",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Loans_LoadID",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Loans_CustomersID",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "CustomersID",
                table: "Loans");

            migrationBuilder.RenameColumn(
                name: "LoadID",
                table: "Payments",
                newName: "LoansID");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_LoadID",
                table: "Payments",
                newName: "IX_Payments_LoansID");

            migrationBuilder.RenameColumn(
                name: "CustumerID",
                table: "Loans",
                newName: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_CustomerID",
                table: "Loans",
                column: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Customers_CustomerID",
                table: "Loans",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Loans_LoansID",
                table: "Payments",
                column: "LoansID",
                principalTable: "Loans",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Customers_CustomerID",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Loans_LoansID",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Loans_CustomerID",
                table: "Loans");

            migrationBuilder.RenameColumn(
                name: "LoansID",
                table: "Payments",
                newName: "LoadID");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_LoansID",
                table: "Payments",
                newName: "IX_Payments_LoadID");

            migrationBuilder.RenameColumn(
                name: "CustomerID",
                table: "Loans",
                newName: "CustumerID");

            migrationBuilder.AddColumn<int>(
                name: "CustomersID",
                table: "Loans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Loans_CustomersID",
                table: "Loans",
                column: "CustomersID");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Customers_CustomersID",
                table: "Loans",
                column: "CustomersID",
                principalTable: "Customers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Loans_LoadID",
                table: "Payments",
                column: "LoadID",
                principalTable: "Loans",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
