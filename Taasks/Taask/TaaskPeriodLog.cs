namespace Taasks
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Reactive.Disposables;
    using NodaTime;

    public sealed class TaaskPeriodLog
    {
        private readonly ConcurrentStack<Duration> log = new ConcurrentStack<Duration>();

        private IDisposable subscription = Disposable.Empty;
        private TaaskPeriod current;

        // What about checks?
        public TaaskPeriodLog(IClock clock) => current = new IdleTaaskPeriod(clock);

        public bool IsLogging => current.IsLogging;

        public void Toggle(Action<Duration> onNext)
        {
            current = current.ToggledAndLogged(log);

            var previous = log.Aggregate(Duration.Zero, (a, b) => a + b);

            subscription.Dispose();
            subscription = current.LoggedTime
                .Subscribe(current => onNext(current + previous));
        }
    }
}
