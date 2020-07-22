using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using DataTableSample.Annotations;

namespace DataTableSample
{
    internal class DataTableViewModel : INotifyPropertyChanged
    {
        public DataTableViewModel()
        {
            AddRowCommand = new AddRowCommand(this);
            AddColCommand = new AddColCommand(this);
            InitializeDataTable();
            AddRow(0, "Jhon");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly DataTable dataTable = new DataTable();

        public ICommand AddRowCommand { get; }
        public ICommand AddColCommand { get; }

        private DataRowView selectedRow;

        public DataRowView SelectedRow
        {
            get => selectedRow;
            set
            {
                selectedRow = value;
                OnPropertyChanged(nameof(SelectedRow));
            }
        }

        private int inputId;
        public int InputId
        {
            get => inputId;
            set
            {
                inputId = value;
                OnPropertyChanged(nameof(InputId));
            }
        }

        private string inputName;
        public string InputName
        {
            get => inputName;
            set
            {
                inputName = value;
                OnPropertyChanged(nameof(InputName));
            }
        }

        private string inputColName;
        public string InputColName
        {
            get => inputColName;
            set
            {
                inputColName = value;
                OnPropertyChanged(nameof(InputColName));
            }
        }

        public DataView DataTableView => new DataView(dataTable);

        private void NotifyTableUpdate()
        {
            OnPropertyChanged(nameof(DataTableView));
        }

        private void InitializeDataTable()
        {
            dataTable.Columns.Clear();
            dataTable.Rows.Clear();
            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("Name");
            NotifyTableUpdate();

        }

        public void AddRow(int id, string name)
        {
            var row = dataTable.NewRow();
            row[0] = id;
            row[1] = name;
            dataTable.Rows.Add(row);
            NotifyTableUpdate();
        }

        public void AddCol(string name)
        {
            dataTable.Columns.Add(name);
            NotifyTableUpdate();
        }
    }

    internal class AddRowCommand : ICommand
    {
        private readonly DataTableViewModel vm;

        public AddRowCommand(DataTableViewModel vm)
        {
            this.vm = vm;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            vm.AddRow(vm.InputId, vm.InputName);
        }

        public event EventHandler CanExecuteChanged;
    }

    internal class AddColCommand : ICommand
    {
        private readonly DataTableViewModel vm;

        public AddColCommand(DataTableViewModel vm)
        {
            this.vm = vm;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            vm.AddCol(vm.InputColName);
        }

        public event EventHandler CanExecuteChanged;
    }
}
