using System;

namespace Tolitech.CodeGenerator.Infrastructure.Data.QueryBuilder.MySql
{
    public class SqlBuilderInsert : QueryBuilder.SqlBuilderInsert
    {
        public SqlBuilderInsert(IBuilderFactory builderFactory, string schemaName, string tableName) : base(builderFactory, schemaName, tableName)
        {

        }

        public override string Build()
        {
            string sql = $"insert into {GetSchemaTable()} {GetSqlColumnsToInsert()};";

            if (isIdentity)
                sql += $" select LAST_INSERT_ID();";

            return sql;
        }

        private string GetSqlColumnsToInsert()
        {
            IList<string> columnsRenamed = new List<string>();
            foreach (var column in base.columns)
                columnsRenamed.Add($"{base.GetColumnName(column)}");

            string sql = "(" + string.Join(", ", columnsRenamed) + ")";
            sql += " values ";
            sql += "(@" + string.Join(", @", base.columns) + ")";
            return sql;
        }

        private string GetSchemaTable()
        {
            if (string.IsNullOrEmpty(schemaName))
                return $"{tableName}";

            return $"{schemaName}_{tableName}";
        }
    }
}
