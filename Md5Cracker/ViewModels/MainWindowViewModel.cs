using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BaseModels;
using Md5Cracker.Models;
using Md5Cracker.UserControls;

namespace Md5Cracker.ViewModels
{
    public class MainWindowViewModel : NotifyPropertyChangedBase
    {
        public Worker Worker { get; set; }

        public ManagedProgressBarViewModel ProgressBar01 { get; set; }

        public MainWindowViewModel()
        {
            Hash = Hasher.MD5("Word");
            ProgressBar01 = new ManagedProgressBarViewModel();

            var alphabet = "abcdefghijklmnopqrstuvwxyz";
            var digits = "1234567890";
            var allSymbols = alphabet + alphabet.ToUpper() + digits;

            int lenght = 4;
            var generator = new VariantGenerator(allSymbols, lenght);
            int threadsCount = 1;
            int countPerThread = generator.TotalVariantsCount / threadsCount;

            Worker = new Worker()
            {
                HashMethod = Hasher.MD5,
                Done = 0,
                AnswerFound = false,
                State = new WorkerState
                {
                    Hash = Hash,
                    IsFound = false,    
                    Lenght = lenght,
                    Symbols = allSymbols,
                    ThreadNum = 0,
                    VariantCount = countPerThread,
                }
            };
            Worker.Init();


            ProgressBar01.WorkerTask = async (progressBar) =>
            {
                Worker.Tick();
                if (Worker.AnswerFound)
                {
                    Answer = Worker.Answer;
                    progressBar.IsWorked = false;
                }
                
                progressBar.ProgressBarValue = Worker.Done;
            };
        }

        private string _hash;
        public string Hash
        {
            get => _hash; set
            {
                _hash = value;
                OnPropertyChanged(nameof(Hash));
            }
        }

        private string _answer;
        public string Answer
        {
            get => _answer; set
            {
                _answer = value;
                OnPropertyChanged(nameof(Answer));
            }
        }
    }
}
