namespace Taasks
{
    using System;
    using System.Collections.Concurrent;
    using System.Reactive.Linq;
    using NodaTime;

    public sealed class IdleTaaskPeriod : TaaskPeriod
    {
        private readonly IClock clock;

        public IdleTaaskPeriod(IClock clock) => this.clock = clock;

        public bool IsLogging => false;

        public IObservable<Duration> LoggedTime => Observable.Empty<Duration>();

        public TaaskPeriod ToggledAndLogged(ConcurrentStack<Duration> log) => new LoggingTaaskPeriod(clock);
    }
}
