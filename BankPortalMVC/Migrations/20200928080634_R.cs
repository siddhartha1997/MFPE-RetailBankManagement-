using Microsoft.EntityFrameworkCore.Migrations;

namespace BankPortalMVC.Migrations
{
    public partial class R : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "viewAccountCustRes",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustId = table.Column<int>(nullable: false),
                    AccId = table.Column<int>(nullable: false),
                    AccType = table.Column<string>(nullable: true),
                    AccBal = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_viewAccountCustRes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "viewAccountDetailsResponses",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccId = table.Column<int>(nullable: false),
                    AccType = table.Column<string>(nullable: true),
                    AccBal = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_viewAccountDetailsResponses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "viewAccountStatementResponses",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<int>(nullable: false),
                    Narration = table.Column<string>(nullable: true),
                    refno = table.Column<int>(nullable: false),
                    valueDate = table.Column<int>(nullable: false),
                    withdrawal = table.Column<double>(nullable: false),
                    deposit = table.Column<double>(nullable: false),
                    closingBalance = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_viewAccountStatementResponses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "viewCurrentServiceResponses",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccId = table.Column<int>(nullable: false),
                    AccBal = table.Column<double>(nullable: false),
                    Message = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_viewCurrentServiceResponses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "viewDepositResponses",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccId = table.Column<int>(nullable: false),
                    DepositStatus = table.Column<string>(nullable: true),
                    AccBal = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_viewDepositResponses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "viewNewCustomerResponses",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(nullable: true),
                    CustomerId = table.Column<int>(nullable: false),
                    CurrentAccountId = table.Column<int>(nullable: false),
                    SavingsAccountId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_viewNewCustomerResponses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "viewSavingsServiceResponses",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccId = table.Column<int>(nullable: false),
                    AccBal = table.Column<double>(nullable: false),
                    Message = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_viewSavingsServiceResponses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "viewTransferResponses",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sbal = table.Column<double>(nullable: false),
                    rbal = table.Column<double>(nullable: false),
                    transferStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_viewTransferResponses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "viewWithdrawResponses",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccId = table.Column<int>(nullable: false),
                    WithdrawStatus = table.Column<string>(nullable: true),
                    AccBal = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_viewWithdrawResponses", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "viewAccountCustRes");

            migrationBuilder.DropTable(
                name: "viewAccountDetailsResponses");

            migrationBuilder.DropTable(
                name: "viewAccountStatementResponses");

            migrationBuilder.DropTable(
                name: "viewCurrentServiceResponses");

            migrationBuilder.DropTable(
                name: "viewDepositResponses");

            migrationBuilder.DropTable(
                name: "viewNewCustomerResponses");

            migrationBuilder.DropTable(
                name: "viewSavingsServiceResponses");

            migrationBuilder.DropTable(
                name: "viewTransferResponses");

            migrationBuilder.DropTable(
                name: "viewWithdrawResponses");
        }
    }
}
