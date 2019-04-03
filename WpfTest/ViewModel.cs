using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Data;


namespace WpfTest
{
    public class TestVM : BindableBase
    {
        readonly Model _model = new Model();
        //public DelegateCommand<string> AddCommand { get; }
        public ObservableCollection<ChemShift> testGridSource => _model.ChemShifts;

        public DelegateCommand<DataGrid> AddNewColumn { get; }
        public DelegateCommand<DataGrid> DeleteColumn { get; }
        public DelegateCommand<DataGrid> GetData { get; }

        public TestVM()
        {
            _model.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };
            AddNewColumn = new DelegateCommand<DataGrid>(datagrid => {
                DataGridTextColumn cnew = new DataGridTextColumn();
                cnew.Header = "Some column";
                cnew.Width = 50;
                datagrid.Columns.Add(cnew);
            });
            DeleteColumn = new DelegateCommand<DataGrid>(datagrid=> {
                if (datagrid.SelectedCells.Count == 1)
                    datagrid.Columns.Remove(datagrid.SelectedCells.First().Column);
            });
        }
    }
}
