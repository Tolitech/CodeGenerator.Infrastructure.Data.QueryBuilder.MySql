# Tolitech.CodeGenerator.Infrastructure.Data.QueryBuilder.MySql
Infrastructure Data QueryBuilder MySQL library used in projects created by the Code Generator tool. 

This project contains the implementation for using the Query Builder MySQL. 

Tolitech Code Generator Tool: [http://www.tolitech.com.br](https://www.tolitech.com.br/)

Examples:
```
SqlBuilderConfiguration.UseMySql();
```

```
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
```

```
var person = new PersonEntity()
{
    Name = "Person 1",
    Age = 18
};

string sql = new SqlBuilder()
    .Insert("Person")
    .AddColumns(person)
    .Identity("PersonId")
    .Build();

string expected = "insert into Person (Name, Age) values (@Name, @Age); select LAST_INSERT_ID();";
```

```
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
```

```
string sql = new SqlBuilder()
    .Delete("base", "Person")
    .Where()
    .AddColumn("PersonId")
    .AddCondition("and Age > @Age")
    .Build();

string expected = "delete from base_Person where PersonId = @PersonId and Age > @Age;";
```

```
string sql = new SqlBuilder()
    .Select("base", "Person")
    .AddColumns("PersonId", "Name")
    .Where()
    .AddColumn("PersonId")
    .AddCondition("and Age > @Age")
    .Build();

string expected = "select PersonId, Name from base_Person where PersonId = @PersonId and Age > @Age;";
```
