using System;

namespace MicroOrm.QueryDefinitions
{
    public class Argument
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public Type Type { get; set; }

        public string GetValue()
        {
            if (Value.GetType() == typeof(String))
                return $"\"{Value.ToString()}\"";
            else
                return Value.ToString();
        }
    }
}
