namespace JConradOOPProject.Commands
{
    using System;
    using System.Windows.Input;

    public delegate void ExecuteDelegate(object obj);
    public delegate bool CanExecuteDelegate(object obj);

    public class RelayCommand : ICommand
    {
        private ExecuteDelegate execute;
        private CanExecuteDelegate canExecute;

        public RelayCommand(ExecuteDelegate execute, CanExecuteDelegate canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        public void Execute(object obj)
        {
            this.execute(obj);
        }
        public bool CanExecute(object obj)
        {
            return this.canExecute(obj);
        }
        public event EventHandler CanExecuteChanged;
    }
}
