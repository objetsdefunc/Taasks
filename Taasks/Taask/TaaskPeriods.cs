namespace Taasks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Linq;
    using NodaTime;

    public sealed class TaaskPeriods
    {
        private readonly IClock clock;
        private readonly List<Duration> periods = new List<Duration>();

        private TaaskPeriod current;

        // What about checks?
        public TaaskPeriods(IClock clock)
        {
            this.clock = clock;
            current = new TaaskPeriod(clock);
        }

        public bool Running => current.Running;

        internal void Toggle(Action<Duration> onNext)
        {
            if (current.Running)
            {
                periods.Add(current.Stopped());
                current = new TaaskPeriod(clock);
            }
            else
            {
                current.Start(duration => onNext(duration + periods.Aggregate(Duration.Zero, (a, b) => a + b)));
            }
        }
    }
}
