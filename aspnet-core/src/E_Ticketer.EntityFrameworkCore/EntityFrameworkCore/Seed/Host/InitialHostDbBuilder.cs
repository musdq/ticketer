namespace E_Ticketer.EntityFrameworkCore.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly E_TicketerDbContext _context;

        public InitialHostDbBuilder(E_TicketerDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();

            _context.SaveChanges();
        }
    }
}
