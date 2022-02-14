using System;

namespace Tolitech.CodeGenerator.Infrastructure.Data.QueryBuilder.MySql
{
    public class SqlBuilderWhere : QueryBuilder.SqlBuilderWhere
    {
        public SqlBuilderWhere(IBuilderFactory builderFactory, string schemaName, string tableName, SqlBuilderCommand command) : base(builderFactory, schemaName, tableName, command)
        {

        }

        public override string BuildWhere()
        {
            string sql = "";

            if (columns.Any() || conditions.Any())
                sql = $" where {GetSqlColumnsToWhere()}";

            return sql;
        }

        private string GetSqlColumnsToWhere()
        {
            string sql = "";
            bool firstTime = true;

            foreach (var whereColumn in columns)
            {
                sql += firstTime ? "" : " and ";
                sql += $"{base.GetColumnName(whereColumn)} = @{whereColumn}";
                firstTime = false;
            }

            foreach (var condition in conditions)
            {
                sql += firstTime ? "" : " ";
                sql += condition;
                firstTime = false;
            }

            return sql;
        }
    }
}
