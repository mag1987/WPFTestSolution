using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using Prism.Commands;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, INotifyPropertyChanged
    {
        private readonly List<int> _myValues = new List<int>();
        public readonly List<int> MyPublicValues;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        public App()
        {
            MyPublicValues = new List<int>(_myValues);
        }
        //добавление в коллекцию числа и уведомление об изменении суммы
        public void AddValue(int value)
        {
            _myValues.Add(value);
            OnPropertyChanged("Sum");
        }
        //проверка на валидность, удаление из коллекции и уведомление об изменении суммы
        public void RemoveValue(int index)
        {
            //проверка на валидность удаления из коллекции - обязанность модели
            if (index >= 0 && index < _myValues.Count) _myValues.RemoveAt(index);
            OnPropertyChanged("Sum");
        }
        public int Sum => MyPublicValues.Sum(); //сумма
    }
    public class MainVM : INotifyPropertyChanged
    {
        readonly App _model = new App();
        public MainVM()
        {
            //таким нехитрым способом мы пробрасываем изменившиеся свойства модели во View
            _model.PropertyChanged += (s, e) => { OnPropertyChanged(e.PropertyName); };
            AddCommand = new DelegateCommand<string>(str => {
                //проверка на валидность ввода - обязанность VM
                int ival;
                if (int.TryParse(str, out ival)) _model.AddValue(ival);
            });
            RemoveCommand = new DelegateCommand<int?>(i => {
                if (i.HasValue) _model.RemoveValue(i.Value);
            });
        }
        public DelegateCommand<string> AddCommand { get; }
        public DelegateCommand<int?> RemoveCommand { get; }
        public int Sum => _model.Sum;
        public List<int> MyValues => _model.MyPublicValues;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
    public class TestVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public DelegateCommand TestDelegateCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    MessageBox.Show("Hallo");
                });
            }
        }
          
    }

}
