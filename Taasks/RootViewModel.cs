namespace Taasks
{
    using System;
    using Caliburn.Micro;
    using NodaTime;

    public sealed class RootViewModel : Screen
    {
        private string time;

        public RootViewModel()
        {
            _ = new Taask(SystemClock.Instance).Time
                .Subscribe(t => Time = t.Seconds.ToString());
        }

        public string Time
        {
            get => time;
            set
            {
                if (value != time)
                {
                    time = value;
                    NotifyOfPropertyChange(() => Time);
                }
            }
        }
    }
}
