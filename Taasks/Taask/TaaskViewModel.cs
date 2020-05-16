namespace Taasks
{
    using System;
    using Caliburn.Micro;

    public sealed class TaaskViewModel : Screen
    {
        private readonly Taask taask;
        private string title;
        private string time;

        public TaaskViewModel(Taask taask)
        {
            this.taask = taask ?? throw new ArgumentNullException(nameof(taask));

            title = taask.Title;

            _ = taask
                .Time
                .Subscribe(t => Time = t.Seconds.ToString());
        }

        public string Title
        {
            get => title;
            set
            {
                if (value != title)
                {
                    title = value;
                    NotifyOfPropertyChange(() => Title);
                }
            }
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

        public void StartStop() => taask.Stop();
    }
}
