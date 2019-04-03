using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using System.Windows;

using Prism.Ioc;
using Prism.Modularity;
using System.Collections.ObjectModel;

namespace WpfTest
{
    public class Model : BindableBase
    {
        /*
        private readonly ObservableCollection<int> _myValues = new ObservableCollection<int>();
        public readonly ReadOnlyObservableCollection<int> MyPublicValues;
    */    
        public Model()
        {
            ChemShifts = new ObservableCollection<ChemShift>()
            {
                new ChemShift(1.2,"ppm","sd" ),
                new ChemShift(2.0,"ppm","rer" ),
                new ChemShift(3.0,"ppm","wewe" )
            };
        }
        /*
        public void RemoveValue(int index)
        {
            //проверка на валидность удаления из коллекции - обязанность модели
            if (index >= 0 && index < _myValues.Count) _myValues.RemoveAt(index);
            RaisePropertyChanged("Sum");
        }
        */
        public ObservableCollection<ChemShift> ChemShifts { get; set; }
    }
    public class ChemShift
    {
        public double Value { get; set; }
        public string Unit { get; set; }
        public string Assignment { get; set; }

        public ChemShift(double value, string assignment) : this(value, "ppm", assignment) { }
        public ChemShift(double value, string unit, string assignment)
        {
            Value = value;
            Unit = unit;
            Assignment = assignment;
        }
    }
    
    
}
