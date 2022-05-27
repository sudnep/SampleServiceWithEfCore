# Register DBContext Factory as dependency

        services
            .AddTransient<IRepository, Repository>()
            .AddDbContextFactory<AppDBContext>(x => x.UseSqlServer(connectionString))
            
# inject  IDbContextFactory<AppDBContext>  to repository

 public class Repository : IRepository
    {
        public string ID { get; set; }
        private readonly IDbContextFactory<AppDBContext> _dbContextFactory;

        public Repository(IDbContextFactory<AppDBContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
  }
  
  
  # create instannce of DBContext for each db operations and dispose it. 
  
     public async Task<List<EventSetting>> GetSettings()
        {
            using (var ctx = _dbContextFactory.CreateDbContext())
            {
                return await ctx.EventSettings.ToListAsync();
            }

        }
  
  
  
  
