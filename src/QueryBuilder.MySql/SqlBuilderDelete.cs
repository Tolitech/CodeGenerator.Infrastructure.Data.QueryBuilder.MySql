using System;

namespace Tolitech.CodeGenerator.Infrastructure.Data.QueryBuilder.MySql
{
    public class SqlBuilderDelete : QueryBuilder.SqlBuilderDelete
    {
        public SqlBuilderDelete(IBuilderFactory builderFactory, string schemaName, string tableName) : base(builderFactory, schemaName, tableName)
        {

        }

        public override string Build()
        {
            string sql = $"delete from {GetSchemaTable()}{where.BuildWhere()};";
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
