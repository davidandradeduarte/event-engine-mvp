using System;
using System.Collections.Generic;
using System.Text;

namespace MT_Common
{
    public class CreatedFoo
    {
        public int Foo { get; set; }
        public string Bar { get; set; }
        public DateTime Timez { get; set; }

        public override string ToString()
        {
            return $"{Timez}: {Foo} {Bar}";
        }
    }
}
