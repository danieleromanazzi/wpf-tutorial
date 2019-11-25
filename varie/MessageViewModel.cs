using cgm.AIS.Windows.Controls.Commands;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace cgm.AIS.Windows.Controls.ViewModels
{
    public class MessageViewModel : DialogBaseViewModel
    {
        System.Windows.Threading.DispatcherTimer PreviewTimer = new System.Windows.Threading.DispatcherTimer();
        int? ElapsedTime;

        public MessageViewModel()
        {
            this.PropertyChanged += OnPropertyChanged;

            StackVisible = Visibility.Collapsed;
            Visibility = Visibility.Hidden;
            TypeMessage = TypeMessage.Progress;
            ButtonVisible = Visibility.Visible;

            Progression = new ProgressionViewModel()
            {
                IsIndeterminate = true,
                Visible = Visibility.Collapsed,
            };//Factories.ViewModelFactory.Single<ProgressionViewModel>();

            PreviewTimer.Interval = TimeSpan.FromSeconds(1);
            PreviewTimer.Tick += PreviewTimer_Tick;

            Close = new DelegateCommand
            {
                ExecuteDelegate = o =>
                {
                        if (StackVisible == Visibility.Visible)
                        {
                            StackVisible = Visibility.Collapsed;
                            return;
                        }
                        Visibility = Visibility.Hidden;
                        Details = "";
                        VisibleDetails = false;
                        Message = string.Empty;

                    if (Result != ResultViewModel.Abort)
                    {
                        ActionExecute?.Invoke(o);
                    }
                },
                CanExecuteDelegate = o => true
            };

            Abort = new DelegateCommand
            {
                ExecuteDelegate = o =>
                {
                    Result = ResultViewModel.Abort;
                    Close.Execute(o);
                },
                CanExecuteDelegate = o => true
            };

            Confirm = new DelegateCommand
            {
                ExecuteDelegate = o =>
                {
                    Result = ResultViewModel.Confirm;
                    //ActionExecute?.Invoke(o);
                    Close.Execute(o);
                },
                CanExecuteDelegate = o => true
            };

            ShowDetails = new DelegateCommand
            {
                ExecuteDelegate = o =>
                {
                    VisibleDetails = !VisibleDetails;
                },
                CanExecuteDelegate = o => true
            };

            VisualizzaStack = new DelegateCommand
            {
                ExecuteDelegate = o =>
                {
                    StackVisible = Visibility.Visible;
                },
                CanExecuteDelegate = o => true
            };
        }

        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Timeout))
            {
                PreviewTimer.Stop();
                if (Timeout.HasValue)
                {
                    Visibility = Visibility.Visible;
                    ElapsedTime = Timeout.Value / 1000;

                    if (TypeMessage != TypeMessage.Information)
                        CountDownText = $" ({ElapsedTime})";

                    PreviewTimer.Start();
                }
                else
                {
                    CountDownText = "";
                }
            }
            if (e.PropertyName == nameof(TypeMessage))
            {
                ButtonVisible = Visibility.Visible;
                QuestionVisibility = System.Windows.Visibility.Collapsed;

                StackTrace = "";
                if (TypeMessage == TypeMessage.Wait)
                {
                    Progression = new ProgressionViewModel()
                    {
                        IsIndeterminate = true,
                    };
                    ButtonVisible = Visibility.Hidden;
                }
                else if (TypeMessage == TypeMessage.Question)
                {
                    QuestionVisibility = System.Windows.Visibility.Visible;
                    ButtonVisible = System.Windows.Visibility.Visible;
                }
            }
            if (e.PropertyName == nameof(StackTrace))
            {
                StackDisplayable = !string.IsNullOrEmpty(StackTrace) ? Visibility.Visible : Visibility.Collapsed;
            }
            //if (e.PropertyName == nameof(Title))
            //{
            //    if (string.IsNullOrEmpty(Caption))
            //        Caption = IntegraContext.Current.ProjectDisplayName ?? IntegraContext.Current.ProjectIdentifier;
            //}
        }
        
        private void PreviewTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Timeout.HasValue)
                {
                    if (ElapsedTime == 0)
                    {
                        PreviewTimer.Stop();

                        Timeout = null;
                        Visibility = Visibility.Hidden;
                        CountDownText = "";

                        return;
                    }

                    ElapsedTime = ElapsedTime - 1;

                    if (TypeMessage != TypeMessage.Information)
                        CountDownText = $" ({ElapsedTime})";
                }
            }
            catch (Exception ex)
            {

            }
        }

        public System.Action<object> ActionExecute;

        public string Title
        {
            get { return GetValue<string>(nameof(Title)); }
            set { SetValue(nameof(Title), value); }
        }

        public string Caption
        {
            get { return GetValue<string>(nameof(Caption)); }
            set { SetValue(nameof(Caption), value); }
        }

        public string Message
        {
            get { return GetValue<string>(nameof(Message)); }
            set { SetValue(nameof(Message), value); }
        }

        public Visibility Visibility
        {
            get { return GetValue<Visibility>(nameof(Visibility)); }
            set { SetValue(nameof(Visibility), value); }
        }

        public int? Timeout
        {
            get { return GetValue<int?>(nameof(Timeout)); }
            set { SetValue(nameof(Timeout), value); }
        }

        public string CountDownText
        {
            get { return GetValue<string>(nameof(CountDownText)); }
            set { SetValue(nameof(CountDownText), value); }
        }
        public ResultViewModel Result
        {
            get { return GetValue<ResultViewModel>(nameof(Result)); }
            set { SetValue(nameof(Result), value); }
        }

        public ICommand Abort { get; set; }
        public ICommand Confirm { get; set; }
        public ICommand Close
        {
            get { return GetValue<ICommand>(nameof(Close)); }
            set { SetValue(nameof(Close), value); }
        }

        public ICommand ShowDetails
        {
            get { return GetValue<ICommand>(nameof(ShowDetails)); }
            set { SetValue(nameof(ShowDetails), value); }
        }

        public TypeMessage TypeMessage
        {
            get { return GetValue<TypeMessage>(nameof(TypeMessage)); }
            set { SetValue(nameof(TypeMessage), value); }
        }

        public ProgressionViewModel Progression
        {
            get { return GetValue<ProgressionViewModel>(nameof(Progression)); }
            set { SetValue(nameof(Progression), value); }
        }

        public string Details
        {
            get { return GetValue<string>(nameof(Details)); }
            set { SetValue(nameof(Details), value); }
        }

        public bool VisibleDetails
        {
            get { return GetValue<bool>(nameof(VisibleDetails)); }
            set { SetValue(nameof(VisibleDetails), value); }
        }

        public Visibility ButtonVisible
        {
            get { return GetValue<Visibility>(nameof(ButtonVisible)); }
            set { SetValue(nameof(ButtonVisible), value); }
        }

        public string StackTrace
        {
            get { return GetValue<string>(nameof(StackTrace)); }
            set { SetValue(nameof(StackTrace), value); }
        }

        public Visibility StackDisplayable
        {
            get { return GetValue<Visibility>(nameof(StackDisplayable)); }
            set { SetValue(nameof(StackDisplayable), value); }
        }

        public Visibility StackVisible
        {
            get { return GetValue<Visibility>(nameof(StackVisible)); }
            set { SetValue(nameof(StackVisible), value); }
        }

        public Visibility QuestionVisibility
        {
            get { return GetValue<Visibility>(nameof(QuestionVisibility)); }
            set { SetValue(nameof(QuestionVisibility), value); }
        }

        public ICommand VisualizzaStack
        {
            get { return GetValue<ICommand>(nameof(VisualizzaStack)); }
            set { SetValue(nameof(VisualizzaStack), value); }
        }
        
    }

    public class ProgressionViewModel : BaseViewModel
    {
        public ProgressionViewModel()
        {
            this.PropertyChanged += OnPropertyChanged;
            IsIndeterminate = false;
        }

        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }

        public bool IsIndeterminate
        {
            get { return GetValue<bool>(nameof(IsIndeterminate)); }
            set { SetValue(nameof(IsIndeterminate), value); }
        }

        public int MinValue
        {
            get { return GetValue<int>(nameof(MinValue)); }
            set { SetValue(nameof(MinValue), value); }
        }

        public int MaxValue
        {
            get { return GetValue<int>(nameof(MaxValue)); }
            set { SetValue(nameof(MaxValue), value); }
        }

        public int Value
        {
            get { return GetValue<int>(nameof(Value)); }
            set { SetValue(nameof(Value), value); }
        }

        public Visibility Visible
        {
            get { return GetValue<Visibility>(nameof(Visible)); }
            set { SetValue(nameof(Visible), value); }
        }
        
    }
}
