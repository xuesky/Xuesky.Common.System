using FreeSql;
using Xuesky.Common.DataAccess;

namespace Xuesky.Common.Test
{
    public static class dbContextHelper
    {
        public static SystemDbContext GetDbContext
        {
            get
            {
                var Fsql = new FreeSqlBuilder()
               .UseConnectionString(FreeSql.DataType.SqlServer, "Data Source=.;Initial Catalog=SystemDb;User ID=sa;Password=123456;")
               .UseLazyLoading(true)
               .UseNoneCommandParameter(true)
               .Build();
                return new SystemDbContext(Fsql);
            }
        }
        public static IFreeSql GetIFreesql
        {
            get
            {
                var Fsql = new FreeSqlBuilder()
               .UseConnectionString(FreeSql.DataType.SqlServer, "Data Source=.;Initial Catalog=SystemDb;User ID=sa;Password=123456;")
               .UseLazyLoading(true)
               .UseNoneCommandParameter(true)
               .Build();
                return Fsql;
            }
        }
    }
}
