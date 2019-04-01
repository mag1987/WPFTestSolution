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
        public DelegateCommand TestDelegateCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    MessageBox.Show("Hallo from file");
                });
            }
        }
       
        readonly Model _model = new Model();
        public DelegateCommand<string> AddCommand { get; }
        public DelegateCommand<int?> RemoveCommand { get; }
        public int Sum => _model.Sum;
        public ReadOnlyObservableCollection<int> MyValues => _model.MyPublicValues;
        public ReadOnlyObservableCollection<ChemShift> testGridSource => _model.ChemShifts;
        public ChemShifts ChemShiftsPublicVM => _model.ChemShiftsPublic;


        public DelegateCommand<DataGrid> AddNewColumn { get; }
        public DelegateCommand<DataGrid> DeleteColumn { get; }
        public DelegateCommand<DataGrid> GetData { get; }

        public Binding binding { get; set; }
        public void testMethod()
        {
            binding = new Binding();
            binding.Source = _model.ChemShiftsPublic;
            
        }
        
        public TestVM()
        {
            //таким нехитрым способом мы пробрасываем изменившиеся свойства модели во View
            _model.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };
            AddCommand = new DelegateCommand<string>(str => {
                //проверка на валидность ввода - обязанность VM
                int ival;
                if (int.TryParse(str, out ival)) _model.AddValue(ival);
            });
            RemoveCommand = new DelegateCommand<int?>(i => {
                if (i.HasValue) _model.RemoveValue(i.Value);
            });
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
            GetData = new DelegateCommand<DataGrid>(datagrid =>{
                datagrid.Columns.Add(
                    new DataGridTextColumn
                    {
                        Header = "New column 1",
                        Width = 50
                    });
                
            });
        }
    }
}
