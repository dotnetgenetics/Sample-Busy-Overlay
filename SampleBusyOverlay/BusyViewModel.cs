using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;


namespace SampleBusyOverlay
{
    /// <summary>
    /// 
    /// </summary>
    public class BusyViewModel : INotifyPropertyChanged
    {
        private ICommand doSomethingCommand;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Item1 { get; set; }
        public string Item2 { get; set; }
        public List<String> Results { get; set; }
        //public bool IsBusy { get; set; }

        private bool _IsBusy;
        public bool IsBusy
        {
            get { return _IsBusy; }
            set
            {
                _IsBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        public ICommand DoSomething
        {
            get { return doSomethingCommand ?? (doSomethingCommand = new DelegateCommand(LongRunningTask)); }
        }

        private void LongRunningTask()
        {
            this.IsBusy = true;
            var task = new Task(ComputeResults);
            task.Start();
        }

        private void ComputeResults()
        {
            //this.IsBusy = true;
            var results = Enumerable.Range(0, 12).Select(x =>
            {
                Thread.Sleep(1000);
                return "Result " + x;
            }).ToList();

            this.Results = results;
            this.IsBusy = false;
        }
    }
}
