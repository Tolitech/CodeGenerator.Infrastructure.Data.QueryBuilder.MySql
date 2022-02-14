using System;
using Xunit;
using Tolitech.CodeGenerator.Infrastructure.Data.QueryBuilder.MySql.Tests.Domain.Entities;

namespace Tolitech.CodeGenerator.Infrastructure.Data.QueryBuilder.MySql.Tests
{
    public class SqlBuilderTest
    {
        public SqlBuilderTest()
        {
            SqlBuilderConfiguration.UseMySql();
        }

        [Fact(DisplayName = "SqlBuilder - Insert - Valid")]
        public void SqlBuilder_Insert_Valid()
        {
            var person = new PersonEntity()
            {
                PersonId = 1,
                Name = "Person 1",
                Age = 18
            };

            string sql = new SqlBuilder()
                .Insert("base", "Person")
                .AddColumns(person)
                .Build();

            string expected = "insert into base_Person (PersonId, Name, Age) values (@PersonId, @Name, @Age);";

            Assert.Equal(expected, sql);
        }

        [Fact(DisplayName = "SqlBuilder - InsertWithIdentity - Valid")]
        public void SqlBuilder_InsertWithIdentity_Valid()
        {
            var person = new PersonEntity()
            {
                PersonId = null,
                Name = "Person 1",
                Age = 18
            };

            string sql = new SqlBuilder()
                .Insert("Person")
                .AddColumns(person)
                .Identity("PersonId")
                .Build();

            string expected = "insert into Person (Name, Age) values (@Name, @Age); select LAST_INSERT_ID();";

            Assert.Equal(expected, sql);
        }

        [Fact(DisplayName = "SqlBuilder - Update - Valid")]
        public void SqlBuilder_Update_Valid()
        {
            var person = new PersonEntity()
            {
                PersonId = 1,
                Name = "Person 1",
                Age = 18
            };

            string sql = new SqlBuilder()
                .Update("base", "Person")
                .AddColumns(person)
                .RemoveColumn(nameof(person.PersonId))
                .Where()
                .AddColumn(nameof(person.PersonId))
                .Build();

            string expected = "update base_Person set Name = @Name, Age = @Age where PersonId = @PersonId;";

            Assert.Equal(expected, sql);
        }

        [Fact(DisplayName = "SqlBuilder - Delete - Valid")]
        public void SqlBuilder_Delete_Valid()
        {
            string sql = new SqlBuilder()
                .Delete("base", "Person")
                .Where()
                .AddColumn("PersonId")
                .AddCondition("and Age > @Age")
                .Build();

            string expected = "delete from base_Person where PersonId = @PersonId and Age > @Age;";

            Assert.Equal(expected, sql);
        }

        [Fact(DisplayName = "SqlBuilder - DeleteWithoutSchema - Valid")]
        public void SqlBuilder_DeleteWithoutSchema_Valid()
        {
            string sql = new SqlBuilder()
                .Delete("Person")
                .Where()
                .AddColumn("PersonId")
                .AddCondition("and Age > @Age")
                .Build();

            string expected = "delete from Person where PersonId = @PersonId and Age > @Age;";

            Assert.Equal(expected, sql);
        }

        [Fact(DisplayName = "SqlBuilder - Select - Valid")]
        public void SqlBuilder_Select_Valid()
        {
            string sql = new SqlBuilder()
                .Select("base", "Person")
                .AddColumns("PersonId", "Name")
                .Where()
                .AddColumn("PersonId")
                .AddCondition("and Age > @Age")
                .Build();

            string expected = "select PersonId, Name from base_Person where PersonId = @PersonId and Age > @Age;";

            Assert.Equal(expected, sql);
        }

        [Fact(DisplayName = "SqlBuilder - SelectAsterisk - Valid")]
        public void SqlBuilder_SelectAsterisk_Valid()
        {
            string sql = new SqlBuilder()
                .Select("Person")
                .Where()
                .AddColumn("PersonId")
                .AddCondition("and Age > @Age")
                .Build();

            string expected = "select * from Person where PersonId = @PersonId and Age > @Age;";

            Assert.Equal(expected, sql);
        }
    }
}
