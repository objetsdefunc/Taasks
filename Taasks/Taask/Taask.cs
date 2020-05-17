namespace Taasks
{
    using System;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using NodaTime;

    public sealed class Taask
    {
        private readonly string title;
        private readonly TaaskPeriods periods;
        private readonly ISubject<Duration> time;

        // What about checks?
        public Taask(IClock clock)
        {
            periods = new TaaskPeriods(clock);
            title = "Taask 1";

            time = new BehaviorSubject<Duration>(Duration.Zero);
        }

        public string Title => title;

        public bool Running => periods.Running;

        public IObservable<Duration> Time => time.StartWith(Duration.Zero);

        public void Toggle()
        {
            periods.Toggle(
                duration => time.OnNext(duration));
        }
    }
}
