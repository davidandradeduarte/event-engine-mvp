using System.Diagnostics;

namespace MT_Common
{
    public class PerformanceCounterAdapter : IPerfCounters
    {
        private readonly PerformanceCounter _pc;
        private readonly Stopwatch _sw;

        public PerformanceCounterAdapter(string instanceName)
        {
            _pc = new PerformanceCounter(
                Settings.PerfCounters.CategoryName,
                Settings.PerfCounters.CounterName,
                instanceName,
                false)
            {
                RawValue = 0
            };

            _sw = new Stopwatch();
        }

        public void Start()
        {
            _sw.Start();
        }

        public void Stop()
        {
            _sw.Stop();

            _pc.RawValue = _sw.ElapsedMilliseconds;

            _sw.Reset();
        }
    }
}
