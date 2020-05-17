namespace Taasks
{
    using System;
    using System.Reactive.Linq;
    using NodaTime;

    public sealed class TaaskPeriod
    {
        private readonly IClock clock;
        private readonly IObservable<Duration> time;

        private Instant startTime;
        private bool running = false;

        // What about checks?
        public TaaskPeriod(IClock clock)
        {
            this.clock = clock ?? throw new ArgumentNullException(nameof(clock));
            startTime = clock.GetCurrentInstant();

            time =
                Observable
                    .Interval(TimeSpan.FromMilliseconds(100))
                    .SkipWhile(_ => !running)
                    .TakeWhile(_ => running)
                    .Select(_ => clock.GetCurrentInstant() - startTime)
                    .Publish()
                    .RefCount();
        }

        public bool Running => running;

        public void Start(Action<Duration> onNext)
        {
            running = true;
            startTime = clock.GetCurrentInstant();
            _ = time.Subscribe(onNext); // Don't need to dispose because of RefCount?
        }

        public Duration Stopped()
        {
            running = false;
            return clock.GetCurrentInstant() - startTime;
        }
    }
}
