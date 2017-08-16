using System;
using System.Collections.Generic;

namespace MicroOrm.QueryDefinitions
{
    public class Entity
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public ICollection<Join> Joins { get; set; }
        public ICollection<Field> Fields { get; set; }
        public Where Where { get; set; }
        public int? Take { get; set; }
        public int? Skip { get; set; }
        public ICollection<Field> OrderBy { get; set; }

        public string FieldsToString()
        {
            if (Fields == null || Fields.Count == 0)
                return "*";
            var fields = GetRelationsFields(this);
            return fields;
        }

        public string JoinsToString()
        {
            return GetJoins(this);
        }

        public static string GetJoins(Entity entity)
        {
            string querySql = "";
            if (entity != null && entity.Joins != null && entity.Joins.Count > 0)
            {
                foreach (var join in entity.Joins)
                {
                    var type = join.Type.ToString().Replace("_", " ");
                    querySql += $"\n{type} JOIN {join.EntityJoin.Name} \nON {entity.Name}.{join.Key} = {join.EntityJoin.Name}.{join.ForeignKey}";
                    querySql += GetJoins(join.EntityJoin);
                }
            }
            return querySql;
        }

        private static string GetRelationsFields(Entity entity)
        {
            var fields = "";
            foreach (var field in entity.Fields)
            {
                fields += $"\n    {entity.Name}.{field.Name}, ";
            }

            if (entity.Joins != null && entity.Joins.Count > 0)
            {
                foreach (var join in entity.Joins)
                {
                    fields += join.EntityJoin.FieldsToString();
                }
            }

            return fields;
        }

        public string WhereToString()
        {
            if (Where == null || Where.Arguments == null || Where.Arguments.Count == 0)
                return "";
            var where = "\nWHERE \n";
            where = GetWheres(where);
            return where.Substring(0, where.Length - 5);
        }

        private string GetWheres(string where = "")
        {
            where += GetWheres(this);
            if (Joins?.Count > 0)
            {
                foreach (var item in Joins)
                {
                    where += item.EntityJoin.GetWheres();
                }
            }

            return where;
        }

        private static string GetWheres(Entity entity, string where = "")
        {
            if (entity.Where?.Arguments?.Count > 0)
            {
                foreach (var item in entity.Where.Arguments)
                {
                    where += $"{entity.Name}.{item.Name} = {item.GetValue()} AND \n";
                }
            }

            return where;
        }

        public string TakeToString()
        {
            if (Take.HasValue && Take.Value > 0)
            {
                return $" TOP {Take} ";
            }
            return "";
        }

        public string SkipToString()
        {
            if (Take.HasValue && Take.Value > 0)
            {
                return $" OFFSET {Skip}";
            }
            return "";
        }

        public string OrderByToString()
        {
            if (OrderBy?.Count > 0)
            {
                string orderByQuery = "ORDER BY ";
                foreach (var field in OrderBy)
                {
                    orderByQuery += $"{Name}.{field.Name}, ";
                }
                return orderByQuery.Substring(0, orderByQuery.Length - 3);
            }
            return "";
        }
    }
}
