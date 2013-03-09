using System.Data.Entity;
using System.Globalization;
using EFNinjectApplication.CrossCutting.Models;

namespace EFNinjectApplication.Dal
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public AppDbContext()
        {
            Database.SetInitializer<AppDbContext>(null);
#if DEBUG
            Database.SetInitializer(new AppDbContextDbInitializer());
#endif
        }
    }

    public class AppDbContextDbInitializer : DropCreateDatabaseIfModelChanges<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            base.Seed(context);

            for (var customerLoop = 0; customerLoop < 10; customerLoop++)
            {
                var customer = new Customer { FirstName = ((char)(65 + customerLoop)).ToString(CultureInfo.InvariantCulture), LastName = "Doe" };
                context.Customers.Add(customer);

                customer = new Customer { FirstName = ((char)(65 + customerLoop)).ToString(CultureInfo.InvariantCulture), LastName = "Smith" };
                context.Customers.Add(customer);
            }
        }
    }
}