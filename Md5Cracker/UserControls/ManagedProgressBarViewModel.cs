using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BaseModels;

namespace Md5Cracker.UserControls
{
    public class ManagedProgressBarViewModel: NotifyPropertyChangedBase
    {
        private int _progressBarValue = 0;
        public int ProgressBarValue
        {
            get => _progressBarValue; set
            {
                _progressBarValue = value;
                OnPropertyChanged(nameof(ProgressBarValue));
            }
        }

        private bool _isWorked = false;
        public bool IsWorked
        {
            get => _isWorked; set
            {
                _isWorked = value;
                OnPropertyChanged(nameof(IsWorked));
                OnPropertyChanged(nameof(StartCommand));
                OnPropertyChanged(nameof(StopCommand));
                OnPropertyChanged(nameof(PauseCommand));
            }
        }

        public string PauseButtonText => IsPaused ? "Resume" : "Pause";

        private bool _isPaused = false;
        public bool IsPaused
        {
            get => _isPaused; set
            {
                _isPaused = value;
                OnPropertyChanged(nameof(IsPaused));
                OnPropertyChanged(nameof(PauseButtonText));
            }
        }

        private Task _workerTask;

        public delegate Task WorkerTaskDelegate(ManagedProgressBarViewModel progressBar);

        public WorkerTaskDelegate WorkerTask;


        public ICommand StartCommand => new RelayCommand(
            x =>
            {
                IsWorked = true;
                ProgressBarValue = 0;
                _workerTask = Task.Run (async () =>
                {
                    while (IsWorked && ProgressBarValue < 100)
                    {
                        if (!IsPaused)
                        {
                            await WorkerTask(this);
                        } else
                        {
                            await Task.Delay(100);
                        }
                        
                    }
                    IsWorked = false;
                });
            },
            x =>
            {
                return !IsWorked;
            });

        public ICommand PauseCommand => new RelayCommand(
            x =>
            {
                IsPaused = !IsPaused;
            },
            x =>
            {
                return IsWorked;
            });

        public ICommand StopCommand => new RelayCommand(
            x =>
            {
                IsWorked = false;
                IsPaused = false;
                ProgressBarValue = 0;
            },
            x =>
            {
                return IsWorked;
            });
    }
}
