using FreeSql;

namespace Xuesky.Common.DataAccess
{
    public class SystemDbContext : DbContext
    {
        public SystemDbContext() : base()
        {
        }
        public SystemDbContext(IFreeSql fsql) : base(fsql, null) { }
        public DbSet<SysUser> SysUsers { get; set; }
        public DbSet<SysModule> SysModules { get; set; }
    }
}
