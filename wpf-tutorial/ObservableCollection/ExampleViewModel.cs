using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using wpf_tutorial.Commands;
using wpf_tutorial.ViewModels;

namespace wpf_tutorial.ObservableCollection
{
    public class ExampleViewModel : ViewModelBase
    {
        readonly List<string> elencoPersonaggi = new List<string>() { "Paolino Paperino", "Paperina", "Qui, Quo e Qua", "Paperon de' Paperoni", "Rockerduck", "Archimede Pitagorico", "Pico de Paperis", "Banda Bassotti", "Paperetta ye ye", "Battista", "Brigitta McBridge", "Amelia", "Gastone Paperone", "Paperoga", "Nonna Papera" };

        public ExampleViewModel()
        {
            Personaggi = new ObservableCollection<string>();
            elencoPersonaggi.ForEach(x => AddPersonaggio(x));

            Aggiungi = new DelegateCommand() {
                ExecuteDelegate = o => {
                    AddPersonaggio(o.ToString());
                }, 
                CanExecuteDelegate = o => o?.ToString().Length > 0,
            };

            Elimina = new DelegateCommand()
            {
                ExecuteDelegate = o => {
                    RemovePersonaggio(o.ToString());
                },
                CanExecuteDelegate = o => o?.ToString().Length > 0,
            };
        }

        private void AddPersonaggio(string item) { Personaggi.Add(item); }
        private void RemovePersonaggio(string item) { Personaggi.Remove(item); }

        public ObservableCollection<string> Personaggi
        {
            get { return GetValue<ObservableCollection<string>>(); }
            set { SetValue(value); }
        }

        public ICommand Aggiungi
        {
            get { return GetValue<ICommand>(); }
            set { SetValue(value); }
        }

        public ICommand Elimina
        {
            get { return GetValue<ICommand>(); }
            set { SetValue(value); }
        }

        //public string Personaggio
        //{
        //    get { return GetValue<string>(); }
        //    set { SetValue(value); }
        //}
    }
}
