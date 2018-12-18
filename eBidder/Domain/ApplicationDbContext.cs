using System.Data.Entity;

namespace eBidder.Domain
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("name=ApplicationDBConnectionString")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ApplicationDbContext>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Migrations.Configuration>("ApplicationDBConnectionString"));
        }

        public DbSet<User> Users { get; set; }
        
        public DbSet<Auction> Auctions { get; set; }

        public DbSet<AuctionItem> AuctionItems { get; set; }

        public DbSet<AuctionRecord> AuctionRecords { get; set; }

        public DbSet<Bid> Bids { get; set; }

        public DbSet<TransactionLog> TransactionLogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            SetAuctionEntity(modelBuilder);
            SetAuctionRecordEntity(modelBuilder);
            SetBidEntity(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void SetBidEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bid>()
                .HasRequired(b => b.Bidder);
        }

        private static void SetAuctionRecordEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuctionRecord>()
                .HasRequired(ar => ar.User);
        }

        private static void SetAuctionEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Auction>()
                .HasRequired(a => a.AuctionItem);
            modelBuilder.Entity<Auction>()
                .HasRequired(a => a.Seller);
            modelBuilder.Entity<Auction>()
                .HasOptional(a => a.CurrentBid);
        }
    }
}