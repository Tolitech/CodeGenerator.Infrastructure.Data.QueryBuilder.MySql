using System;

namespace Tolitech.CodeGenerator.Infrastructure.Data.QueryBuilder.MySql
{
    public class SqlBuilderConfiguration
    {
        public static void UseMySql()
        {
            AddQueryBuilder("default");
        }

        public static void AddQueryBuilder(string key)
        {
            var builderFactory = new MySqlBuilderFactory();
            SqlBuilder.AddQueryBuilder(key, builderFactory);
        }
    }
}
