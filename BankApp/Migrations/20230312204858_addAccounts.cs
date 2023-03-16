using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace BankApp.Migrations
{
    public partial class addAccounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            // insert account holders
            migrationBuilder.InsertData(
                table: "AccountHolders",
                columns: new[] { "Id", "FirstName", "LastName", "DateOfBirth", "IdNumber", "ResidentialAddress", "MobileNumber", "EmailAddress" },
                values: new object[,]
                {
                { 1, "John", "Doe", new DateTime(1980, 1, 1), "1234567890", "123 Main St", "555-1234", "john.doe@example.com" },
                { 2, "Jane", "Doe", new DateTime(1985, 1, 1), "0987654321", "456 Park Ave", "555-5678", "jane.doe@example.com" }
                });

            // insert bank accounts
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountNumber", "AccountType", "Name", "IsActive", "AvailableBalance", "LastModified", "AccountHolderId" },
                values: new object[,]
                {
                { 1, "000001", "Cheque", "John Doe's Cheque Account", true, 10000.00m, DateTime.Now, 1 },
                { 2, "000002", "Savings", "John Doe's Savings Account", true, 20000.00m, DateTime.Now, 1 },
                { 3, "000003", "Fixed Deposit", "John Doe's Fixed Deposit Account", true, 50000.00m, DateTime.Now, 1 },
                { 4, "000004", "Cheque", "Jane Doe's Cheque Account", true, 5000.00m, DateTime.Now, 2 },
                { 5, "000005", "Savings", "Jane Doe's Savings Account", true, 10000.00m, DateTime.Now, 2 }
                });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
