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
            ChemShiftsPUblic = _chemShifts;
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
        public ObservableCollection<ChemShift> ChemShiftsPUblic { get; set; }
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
    public class ChemShiftProperty
    {
        public string Name { get; set; }
        public Object Value { get; set; }
        public ChemShiftProperty()
        {
            Name = "NewProperty";
            Value = null;
        }
        public ChemShiftProperty(string name, Object value)
        {
            Name = name;
            Value = value;
        }
        public ChemShiftProperty(string name) : this(name , null) { }
    }
    public class ChemShift1
    {
        public List<ChemShiftProperty> Properties { get; set; }
        public ChemShift1()
        {
            Properties = new List<ChemShiftProperty>();
        }
        public ChemShift1(List<ChemShiftProperty> properties)
        {
            Properties = new List<ChemShiftProperty>(properties);
        }
    }
    public class ChemShifts
    {
        private List<ChemShift1> _shifts { get; set; }
        private List<ChemShiftProperty> _shiftProperties { get; set; }

        public List<ChemShift1> Shifts => _shifts;
        public ChemShifts()
        {
            _shifts = new List<ChemShift1>();
            _shiftProperties = new List<ChemShiftProperty>();
        }
        public void AddProperty(ChemShiftProperty newProperty)
        {
            /*
            for (int i =0; i<Shifts.Count; i++)
            {
                Shifts[i].Properties.Add(newProperty);
            }
            */
            _shiftProperties.Add(newProperty);
            foreach (var shift in _shifts)
            {
                shift.Properties.Add(newProperty);
            }
        }
        public void DeleteProperty(ChemShiftProperty propertyToDelete)
        {
            _shiftProperties.Remove(propertyToDelete);
            foreach (var shift in _shifts)
            {
                shift.Properties.Remove(propertyToDelete);
            }
        }
        public void AddShift()
        {
            _shifts.Add(new ChemShift1(_shiftProperties));
        }
        public void DeleteShift(ChemShift1 shiftToDelete)
        {
            _shifts.Remove(shiftToDelete);
        }

    }
    public class NewClass : ObservableCollection<ChemShift1>
    {
        
        NewClass()
        {
            Add(new ChemShift1());
            
        }
        public void method()
        {
        }
    }
    
}
