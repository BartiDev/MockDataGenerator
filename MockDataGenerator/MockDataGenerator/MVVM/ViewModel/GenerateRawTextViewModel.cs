using AutoMapper;
using MockarooApiClient.Data;
using MockarooApiClient.Model;
using MockDataGenerator.Core;
using MockDataGenerator.MVVM.Model;
using MockDataGenerator.Store;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MockDataGenerator.MVVM.ViewModel
{
    public class GenerateRawTextViewModel : BaseViewModel
    {
        private readonly UserStore _userStore;
        private readonly DataTypeStore _dataTypeStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly IMapper _mapper;
        private ObservableCollection<DataFieldModel> _dataFields;
        private string _generatedData;

        public UserModel User { get; set; }
        public ObservableCollection<DataTypeModel> DataTypes => new ObservableCollection<DataTypeModel>(_dataTypeStore.DataTypes);
        public ObservableCollection<DataFieldModel> DataFields
        {
            get { return _dataFields; }
            set 
            { 
                _dataFields = value;
                OnPropertyChanged();
            }
        }
        public int SelectedDataFieldIndex { get; set; }
        public int RowsToGenerate { get; set; }
        public string GeneratedData
        {
            get { return _generatedData; }
            set 
            { 
                _generatedData = value;
                OnPropertyChanged();
            }
        }




        public RelayCommand AddNextFieldCommand { get; set; }
        public RelayCommand ChooseDataTypeCommand { get; set; }
        public RelayCommand DeleteFieldCommand { get; set; }
        public RelayCommand GenerateDataCommand { get; set; }
        public RelayCommand FormatToJsonCommand { get; set; }


        public GenerateRawTextViewModel(UserStore userStore, DataTypeStore dataTypeStore, ModalNavigationStore modalNavigationStore,
            IMapper mapper)
        {
            _userStore = userStore;
            _dataTypeStore = dataTypeStore;
            _modalNavigationStore = modalNavigationStore;
            _mapper = mapper;
            _userStore.UserLoggedInEvent += OnUserLoggedIn;
            _dataTypeStore.DataTypesChangedEvent += OnDataTypesChanged;

            AddNextFieldCommand = new RelayCommand(o => AddDataField());
            ChooseDataTypeCommand = new RelayCommand(o =>
                                                        {
                                                            _modalNavigationStore.CurrentModalView = new DataTypesViewModel(_dataTypeStore);
                                                            SelectedDataFieldIndex = (int)o;
                                                            _dataTypeStore.DataTypeSelectedEvent += OnDataTypeSelected;
                                                        });
            DeleteFieldCommand = new RelayCommand(o => DataFields.RemoveAt((int)o));
            GenerateDataCommand = new RelayCommand(o => GenerateData());
            FormatToJsonCommand = new RelayCommand(o => FormatToJson());
        }

        private void AddDataField()
        {
            if(DataFields == null)
                DataFields = new ObservableCollection<DataFieldModel>();
            
            DataFields.Add(new DataFieldModel()
            {
                Name = "first_name",
                DataType = DataTypes.FirstOrDefault(dt => dt.Name == "First Name"),
                Blank = 0
            });
        }
        private async void GenerateData()
        {
            GenericData data = new GenericData();
            GeneratedData = await data.GenerateData(User.ApiKey, RowsToGenerate, _mapper.Map<ObservableCollection<DataFieldModel> ,List<DataFieldDTO>>(DataFields));
        }

        private void FormatToJson()
        {
            StringBuilder toJson = new StringBuilder(GeneratedData);

            toJson.Replace("[", "[\n\t");
            toJson.Replace("]", "\n]");
            toJson.Replace("{", "{\n\t\t");
            toJson.Replace("}", "\n\t}");
            toJson.Replace(",\"", ",\n\t\t\"");
            toJson.Replace("\":", "\": ");

            GeneratedData = toJson.ToString();
        }

        private void OnUserLoggedIn(UserModel userModel)
        {
            User = userModel;
        }
        private void OnDataTypesChanged()
        {
            OnPropertyChanged(nameof(DataTypes));
            AddDataField();
        }
        private void OnDataTypeSelected(DataTypeModel dataType)
        {
            DataFields[SelectedDataFieldIndex].DataType = new DataTypeModel(dataType);

            _modalNavigationStore.CurrentModalView = null;
            _dataTypeStore.DataTypeSelectedEvent -= OnDataTypeSelected;
        }
    }
}
