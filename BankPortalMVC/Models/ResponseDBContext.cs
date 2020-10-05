using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankPortalMVC.Models
{
    public class ResponseDBContext:DbContext
    {
        public ResponseDBContext(DbContextOptions options) : base(options)
        { }
        public ResponseDBContext()
        {

        }
        public DbSet<ViewAccountCustRes> viewAccountCustRes { get; set; }
        public DbSet<ViewAccountStatementResponse> viewAccountStatementResponses { get; set; }
        public DbSet<ViewDepositResponse> viewDepositResponses { get; set; }
        public DbSet<ViewTransferResponse> viewTransferResponses { get; set; }
        public DbSet<ViewWithdrawResponse> viewWithdrawResponses { get; set; }
        public DbSet<ViewAccountDetailsResponse> viewAccountDetailsResponses { get; set; }
        public DbSet<ViewCurrentServiceResponse> viewCurrentServiceResponses { get; set; }
        public DbSet<ViewNewCustomerResponse> viewNewCustomerResponses { get; set; }
        public DbSet<ViewSavingsServiceResponse> viewSavingsServiceResponses { get; set; }
    }
}
