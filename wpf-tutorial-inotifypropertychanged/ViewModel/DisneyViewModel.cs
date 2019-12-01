using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace wpf_tutorial_inotifyPropertyChanged
{
    public class DisneyViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string personaggio;

        public string Personaggio
        {
            get { return personaggio; }
            set { personaggio = value; RaisePropertyChanged("Personaggio"); }
        }



        string[] elencoPersonaggi = { "Paolino Paperino", "Paperina", "Qui, Quo e Qua", "Paperon de' Paperoni", "Rockerduck", "Archimede Pitagorico", "Pico de Paperis", "Banda Bassotti", "Paperetta ye ye", "Battista", "Brigitta McBridge", "Amelia", "Gastone Paperone", "Paperoga", "Nonna Papera" };

        DispatcherTimer dispatcherTimer;
        public DisneyViewModel()
        {
            //  DispatcherTimer setup
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 3);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
             Random rand = new Random();
            // Generate a random index less than the size of the array.  
            int value = rand.Next(elencoPersonaggi.Length);

            Personaggio = elencoPersonaggi[value];
        }


    }
}
