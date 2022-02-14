using System;

namespace Tolitech.CodeGenerator.Infrastructure.Data.QueryBuilder.MySql
{
    public class MySqlBuilderFactory : IBuilderFactory
    {
        public QueryBuilder.SqlBuilderDelete CreateSqlBuilderDelete(string schemaName, string tableName)
        {
            schemaName = CheckSchemaName(schemaName);
            return new SqlBuilderDelete(this, schemaName, tableName);
        }

        public QueryBuilder.SqlBuilderInsert CreateSqlBuilderInsert(string schemaName, string tableName)
        {
            schemaName = CheckSchemaName(schemaName);
            return new SqlBuilderInsert(this, schemaName, tableName);
        }

        public QueryBuilder.SqlBuilderSelect CreateSqlBuilderSelect(string schemaName, string tableName)
        {
            schemaName = CheckSchemaName(schemaName);
            return new SqlBuilderSelect(this, schemaName, tableName);
        }

        public QueryBuilder.SqlBuilderUpdate CreateSqlBuilderUpdate(string schemaName, string tableName)
        {
            schemaName = CheckSchemaName(schemaName);
            return new SqlBuilderUpdate(this, schemaName, tableName);
        }

        public QueryBuilder.SqlBuilderWhere CreateSqlBuilderWhere(string schemaName, string tableName, SqlBuilderCommand command)
        {
            schemaName = CheckSchemaName(schemaName);
            return new SqlBuilderWhere(this, schemaName, tableName, command);
        }

        private static string CheckSchemaName(string schemaName)
        {
            if (string.IsNullOrEmpty(schemaName))
                return "";

            return schemaName;
        }
    }
}
