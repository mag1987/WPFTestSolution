using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using System.Windows;
using System.Collections.ObjectModel;


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
        }
        // from here testing DataGrid

        /*
        static List<string> _listOne = new List<string>() {"1", "2", "3"};
        static List<string> _listTwo = new List<string>() { " one", "two", "three", "four", "five" };
        static List<string> _listThree = new List<string>() { "ein", "zwei", "drei", "vier" };

        static List<List<string>> testCollection = new List<List<string>>() {_listOne, _listTwo, _listThree };
        */    
        static List<ChemShift> testCollection = new List<ChemShift>()
        {
            new ChemShift("1.0","ppm","sd" ),
            new ChemShift("2.0","ppm","rer" ),
            new ChemShift("3.0","ppm","wewe" )
        };

        ObservableCollection<ChemShift> _testGridSource = new ObservableCollection<ChemShift>(testCollection);
        public ObservableCollection<ChemShift> testGridSource => _testGridSource;
        
        // end testing DataGrid
        
    }
    public class ChemShift
    {
        string _value;
        string _unit;
        string _assignment;
        List<string> _additionalParameters;
        public ChemShift(string value, string unit, string assignment)
        {
            Value = value;
            Unit = unit;
            Assignment = assignment;
            AdditionalParameters = new List<string>();
        }
        public string Value { get; set; }
        public string Unit { get; set; }
        public string Assignment { get; set; }
        public List<string> AdditionalParameters { get; set; }

    }

}
