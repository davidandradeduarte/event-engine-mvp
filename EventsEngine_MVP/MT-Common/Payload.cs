using System;

namespace MT_Common
{
    public class Payload
    {
        public int Foo { get; set; }

        public string Bar { get; set; }

        public DateTime Timez { get; set; }

        public byte[] Data;

        public Payload()
        {
            Data = new byte[100000];
            new Random().NextBytes(Data);
        }

        public override string ToString()
        {
            return $"{Timez}: {Foo} {Bar} | Data: {Data.Length * 8 / 1000} KB";
        }
    }
}
