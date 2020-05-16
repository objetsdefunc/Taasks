namespace Taasks
{
    using System;
    using Caliburn.Micro;
    using NodaTime;

    public sealed class RootViewModel : Screen
    {
        private readonly TaaskViewModel taask;
        private string time;

        public RootViewModel()
        {
            taask = new TaaskViewModel(new Taask(SystemClock.Instance));
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

        public TaaskViewModel Taask => taask;
    }
}
