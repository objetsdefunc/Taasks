namespace Taasks
{
    using System;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using NodaTime;

    public sealed class Taask
    {
        private readonly string title;
        private readonly TaaskPeriodLog log;
        private readonly ISubject<Duration> loggedTime;

        // What about checks?
        public Taask(string title, IClock clock)
        {
            this.title = title;
            log = new TaaskPeriodLog(clock);

            loggedTime = new BehaviorSubject<Duration>(Duration.Zero);
        }

        public string Title => title;

        public IObservable<Duration> LoggedTime => loggedTime.StartWith(Duration.Zero);

        public bool IsLogging => log.IsLogging;

        public void Toggle()
        {
            log.Toggle(
                duration => loggedTime.OnNext(duration));
        }
    }
}
