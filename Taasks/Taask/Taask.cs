namespace Taasks
{
    using System;
    using System.Reactive.Linq;
    using NodaTime;

    public sealed class Taask
    {
        private readonly string title;
        private readonly IObservable<Duration> time;
        private readonly Instant start;

        public Taask(IClock clock)
        {
            title = "Dummy";
            _ = clock ?? throw new ArgumentNullException(nameof(clock));
            start = clock.GetCurrentInstant();
            time =
                Observable.Interval(TimeSpan.FromSeconds(1))
                    .Select(_ => clock.GetCurrentInstant() - start);
        }

        public string Title => title;

        public IObservable<Duration> Time => time;
    }
}
