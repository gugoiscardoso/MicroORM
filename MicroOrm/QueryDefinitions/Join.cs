using MicroOrm.Enum;

namespace MicroOrm.QueryDefinitions
{
    public class Join
    {
        public Entity EntityJoin { get; set; }
        public JoinType Type { get; set; }
        public string Key { get; set; }
        public string ForeignKey { get; set; }
    }
}
