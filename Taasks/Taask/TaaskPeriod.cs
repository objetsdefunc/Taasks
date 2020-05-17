namespace Taasks
{
    using System;
    using System.Collections.Concurrent;
    using NodaTime;

    public interface TaaskPeriod
    {
        IObservable<Duration> LoggedTime { get; }

        bool IsLogging { get; }

        TaaskPeriod ToggledAndLogged(ConcurrentStack<Duration> log);
    }
}
