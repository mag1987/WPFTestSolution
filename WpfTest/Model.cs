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
        public void TestMethod() { }

        private readonly ObservableCollection<int> _myValues = new ObservableCollection<int>();
        public readonly ReadOnlyObservableCollection<int> MyPublicValues;
        public Model()
        {
            MyPublicValues = new ReadOnlyObservableCollection<int>(_myValues);
            _chemShifts = new ObservableCollection<ChemShift>()
            {
                new ChemShift(1.2,"ppm","sd" ),
                new ChemShift(2.0,"ppm","rer" ),
                new ChemShift(3.0,"ppm","wewe" )
            };
            ChemShifts = new ReadOnlyObservableCollection<ChemShift>(_chemShifts);
        }
        //добавление в коллекцию числа и уведомление об изменении суммы
        public void AddValue(int value)
        {
            _myValues.Add(value);
            RaisePropertyChanged("Sum");
        }
        //проверка на валидность, удаление из коллекции и уведомление об изменении суммы
        public void RemoveValue(int index)
        {
            //проверка на валидность удаления из коллекции - обязанность модели
            if (index >= 0 && index < _myValues.Count) _myValues.RemoveAt(index);
            RaisePropertyChanged("Sum");
        }
        public int Sum => MyPublicValues.Sum(); //сумма
        private readonly ObservableCollection<ChemShift> _chemShifts = new ObservableCollection<ChemShift>();
        public readonly ReadOnlyObservableCollection<ChemShift> ChemShifts;
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
