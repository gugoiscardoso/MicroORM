using MicroOrm.QueryDefinitions;
using System.Text;

namespace MicroOrm
{
    public class QueryBuilder
    {
        public static string BuiltQuerySql(Entity entity)
        {
            string querySql;

            querySql = $"SELECT {entity.TakeToString()}{entity.FieldsToString()}";
            querySql = querySql.Substring(0, querySql.Length - 2);
            querySql += $"\nFROM {entity.Name} {entity.JoinsToString()} {entity.WhereToString()} {entity.OrderByToString()} {entity.SkipToString()}";

            byte[] bytes = Encoding.UTF8.GetBytes(querySql);
            querySql = Encoding.UTF8.GetString(bytes);
            return querySql;
        }
    }
}
