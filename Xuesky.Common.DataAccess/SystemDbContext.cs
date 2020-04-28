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
        public DbSet<SysRole> SysRoles { get; set; }

        public DbSet<SysRoleModule> SysRoleModules { get; set; }

        public DbSet<TeacherInfo> TeacherInfos { get; set; }

        public DbSet<ClassInfo> ClassInfos { get; set; }
        public DbSet<StuInfo> StuInfos { get; set; }
    }
}
