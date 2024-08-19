using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace cp_11.ViewModel.Base;

public class RelayCommand : ICommand
{
    #region Fields

    private readonly Action<object> _execute;
    private readonly Predicate<object> _canExecute;

    #endregion Fields

    #region Constructors

    public RelayCommand(Action<object> execute)
        : this(execute, null)
    {
    }

    public RelayCommand(Action<object> execute, Predicate<object> canExecute)
    {
        if (execute == null)
            throw new ArgumentNullException("execute");
        _execute = execute;
        _canExecute = canExecute;
    }

    #endregion Constructors

    #region ICommand Members

    [DebuggerStepThrough]
    public bool CanExecute(object parameter)
    {
        return _canExecute == null ? true : _canExecute(parameter);
    }

    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public void Execute(object parameter)
    {
        _execute(parameter);
    }

    #endregion ICommand Members
}

public class RelayCommand<T> : ICommand
{
    public RelayCommand(Func<T?, bool>? canExecute = null, Action<T?>? execute = null)
    {
        CanExecuteDelegate = canExecute;
        ExecuteDelegate = execute;
    }

    public Action<T?>? ExecuteDelegate { get; }

    public Func<T?, bool>? CanExecuteDelegate { get; }
#if NET5_0_OR_GREATER
#nullable enable
    public bool CanExecute(object? parameter)
#nullable disable
#else
        public bool CanExecute(object parameter)
#endif
    {
        var canExecute = CanExecuteDelegate;
#pragma warning disable CS8604 // Possible null reference argument.
        return canExecute is null || canExecute(parameter is T t ? t : default);
#pragma warning restore CS8604 // Possible null reference argument.
    }
#nullable enable
#if NET5_0_OR_GREATER
    public event EventHandler? CanExecuteChanged
#else
        public event EventHandler CanExecuteChanged
#endif
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
#if NET5_0_OR_GREATER
    public void Execute(object? parameter)
#else
        public void Execute(object parameter)
#endif
    {
#pragma warning disable CS8604 // Possible null reference argument.
        ExecuteDelegate?.Invoke(parameter is T t ? t : default);
#pragma warning restore CS8604 // Possible null reference argument.
    }
#nullable disable
#nullable enable
#nullable disable
}

public class RelayCommandAsync : ICommand
{
    private readonly Func<object, bool> _canExecute;
    private readonly Func<object, Task> _execute;
    private long _isExecuting;

    public RelayCommandAsync(Func<object, Task> execute, Func<object, bool> canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute ?? (_ => true);
    }

    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object parameter)
    {
        if (Interlocked.Read(ref _isExecuting) != 0)
            return false;
        return _canExecute(parameter);
    }

    public async void Execute(object parameter)
    {
        Interlocked.Exchange(ref _isExecuting, 1);
        RaiseCanExecuteChanged();
        try
        {
            await _execute(parameter);
        }
        finally
        {
            Interlocked.Exchange(ref _isExecuting, 0);
            RaiseCanExecuteChanged();
        }
    }

    public void RaiseCanExecuteChanged()
    {
        CommandManager.InvalidateRequerySuggested();
    }
}