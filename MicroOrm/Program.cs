using MicroOrm.Enum;
using MicroOrm.QueryDefinitions;
using System;
using System.Collections.Generic;

namespace MicroOrm
{
    public class Program
    {
        static void Main(string[] args)
        {
            var pessoa = new Entity
            {
                Name = "Pessoa",
                Fields = new List<Field>() { new Field { Name = "name" }, new Field { Name = "rg" } },
                Where = new Where
                {
                    Arguments = new List<Argument>
                    {
                        new Argument { Name = "name", Type = typeof(String), Value = "Gustavo" }
                    }
                }
            };

            var endereco = new Entity
            {
                Name = "Endereco",
                Fields = new List<Field>() { new Field { Name = "logradouro" }, new Field { Name = "cep" } },
                Where = new Where
                {
                    Arguments = new List<Argument>
                    {
                        new Argument { Name = "logradouro", Type = typeof(String), Value = "Rua Marte" }
                    }
                }
            };

            var company = new Entity
            {
                Name = "Company",
                Fields = new List<Field>() { new Field { Name = "name" }, new Field { Name = "description" } },
                Where = new Where
                {
                    Arguments = new List<Argument>
                    {
                        new Argument { Name = "name", Value = "Microsoft", Type = typeof(string) },
                        new Argument { Name = "description", Value = "Microsoft", Type = typeof(string) },
                        new Argument { Name = "birthDate", Value = "01/01/1000", Type = typeof(string) }
                    }
                },
                Joins = new List<Join> {
                    new Join { EntityJoin = endereco, ForeignKey = "companyid", Key = "id" , Type = JoinType.INNER },
                    new Join { EntityJoin = pessoa, ForeignKey = "companyid", Key = "id" , Type = JoinType.INNER }
                }
            };

            var query = QueryBuilder.BuiltQuerySql(company);
            Console.WriteLine(query);

            Console.ReadLine();
        }
    }
}
