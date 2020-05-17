namespace Taasks
{
    using System;
    using Caliburn.Micro;

    public sealed class TaaskViewModel : Screen
    {
        private readonly Taask taask;
        private readonly IDisposable subscription;

        private string title;
        private string time;
        private string label;

        public TaaskViewModel(Taask taask)
        {
            this.taask = taask ?? throw new ArgumentNullException(nameof(taask));

            title = taask.Title;
            label = taask.IsLogging ? "Stop" : "Start";

            subscription = taask
                .LoggedTime
                .Subscribe(t => Time = t.ToString("D:hh:mm:ss.f", null));
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

        public string Label
        {
            get => label;
            set
            {
                if (value != label)
                {
                    label = value;
                    NotifyOfPropertyChange(() => Label);
                }
            }
        }

        public void Toggle()
        {
            taask.Toggle();
            Label = taask.IsLogging ? "Stop" : "Start";
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);

            subscription.Dispose();
        }
    }
}
