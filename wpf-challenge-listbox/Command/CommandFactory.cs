using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace wpf_challenge_listbox.Command
{
    public class CommandFactory
    {
        private readonly Dictionary<string, ICommand> _factories;

        private CommandFactory()
        {
            _factories = new Dictionary<string, ICommand>();

            //foreach (Actions action in Enum.GetValues(typeof(Actions)))
            //{
            //    var factory = (AirConditionerFactory)Activator.CreateInstance(Type.GetType("FactoryMethod." + Enum.GetName(typeof(Actions), action) + "Factory"));
            //    _factories.Add(action, factory);
            //}
        }

        public static CommandFactory InitializeFactories() => new CommandFactory();

        static CommandFactory instance;
        static object initLock = new object();
        public static CommandFactory Curret
        {
            get
            {

                lock (initLock)
                {
                    if (instance == null)
                    {

                        instance = new CommandFactory();
                    }
                    return instance;
                }

            }
        }

        public ICommand Create(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            if (!_factories.ContainsKey(name))
            {
                Type type = Type.GetType($"wpf_challenge_listbox.Command.{name}Command");
                var instance = Activator.CreateInstance(type);
                _factories.Add(name, (ICommand)instance);
                return (ICommand)instance;
            }

            return _factories[name];
        }
    }
}
