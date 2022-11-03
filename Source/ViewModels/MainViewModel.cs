using Source.Commands;
using Source.Models;
using Source.Repositories.Abstracts;
using Source.Repositories.Concretes;
using Source.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Source.ViewModels;

public class MainViewModel
{
    private readonly ICarRepository _carRepository;
    public ObservableCollection<Car> Cars { get; set; }
    public Car? SelectedCar { get; set; }

    public ICommand ShowCommand { get; set; }
    public ICommand EditCommand { get; set; }
    public ICommand AddCommand { get; set; }

    public MainViewModel(ICarRepository carRepository)
    {
        _carRepository = carRepository;
        Cars = new(_carRepository.GetList() ?? new());

        ShowCommand = new RelayCommand(ExecuteShowCommand, CanExecuteCommand);
        AddCommand = new RelayCommand(ExecuteAddCommand);
        EditCommand = new RelayCommand(ExecuteEditCommand);

    }


    bool CanExecuteCommand(object? parametr) => SelectedCar is not null;
    void ExecuteShowCommand(object? parametr) => MessageBox.Show(SelectedCar?.Make);

    void ExecuteEditCommand(object? parametr)
    {
        EditViewModel editViewModel = new(SelectedCar);

        EditView editView = new();
        editView.DataContext = editViewModel;

        editView.ShowDialog();
    }


    void ExecuteAddCommand(object? parametr)
    {
        AddViewModel addViewModel = new();

        AddView addView = new();
        addView.DataContext = addViewModel;

        addView.ShowDialog();

        if (addViewModel.MyDialogResult is true)
            Cars.Add(addViewModel.NewCar);


    }


}
