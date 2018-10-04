namespace CF.Data.Context
{
    using CF.Data.Entities;
    using System.Data.Entity;

    public partial class CfDb : DbContext
    {
        public CfDb()
            : base("name=CfDb")
        {
            Database.SetInitializer<CfDb>(new ContextHandler());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CfDb, Migrations.Configuration>(useSuppliedContext: true));

            this.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<OrderPaid> OrderPaids { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<Table> Tables { get; set; }
        public virtual DbSet<Zone> Zones { get; set; }
        public virtual DbSet<License> Licenses { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}