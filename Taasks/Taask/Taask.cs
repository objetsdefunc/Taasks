namespace Taasks
{
    using System;
    using System.Reactive.Linq;
    using NodaTime;

    public sealed class Taask
    {
        private readonly string title;
        private readonly IObservable<Duration> timer;
        private readonly Instant start;
        private bool stopped;

        // What about checks?
        public Taask(IClock clock)
        {
            title = "Taask 1";
            _ = clock ?? throw new ArgumentNullException(nameof(clock));
            start = clock.GetCurrentInstant();

            timer =
                Observable.Interval(TimeSpan.FromSeconds(1))
                    .TakeWhile(_ => !stopped)
                    .Select(_ => clock.GetCurrentInstant() - start)
                    .Publish()
                    .RefCount();
        }

        public string Title => title;

        public IObservable<Duration> Time => timer.StartWith(Duration.Zero);

        public void Stop()
        {
            stopped = true;
        }
    }
}
