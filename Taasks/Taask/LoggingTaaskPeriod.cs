namespace Taasks
{
    using System;
    using System.Collections.Concurrent;
    using System.Reactive.Linq;
    using NodaTime;

    internal sealed class LoggingTaaskPeriod : TaaskPeriod
    {
        private readonly IClock clock;
        private readonly IObservable<Duration> time;
        private readonly Instant startTime;

        private Instant endTime;
        private bool running = true;

        // What about checks?
        public LoggingTaaskPeriod(IClock clock)
        {
            this.clock = clock ?? throw new ArgumentNullException(nameof(clock));
            startTime = clock.GetCurrentInstant();

            // "Hot observable" - starts running straight away,
            // and subscribers only get values from the point they subscribe,
            // seeing only the last value if they subscribe after it's completed.
            time =
                Observable
                    .Interval(TimeSpan.FromMilliseconds(100))
                    .TakeWhile(_ => running)
                    .Select(_ => clock.GetCurrentInstant() - startTime)
                    .Replay(1)
                    .RefCount();
        }

        public bool IsLogging => true;

        public IObservable<Duration> LoggedTime => time.StartWith(Duration.Zero);

        public TaaskPeriod ToggledAndLogged(ConcurrentStack<Duration> log)
        {
            if (running)
            {
                endTime = clock.GetCurrentInstant();
                running = false;
            }

            log.Push(endTime - startTime);

            return new IdleTaaskPeriod(clock);
        }
    }
}
