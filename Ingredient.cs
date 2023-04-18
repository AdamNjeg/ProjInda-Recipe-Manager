using System;
using System.ComponentModel;
using System.Xml.Linq;

public class Ingredient : INotifyPropertyChanged
{
    private string _ingredientName;
    private decimal _amount;
    private string unit;

    public override string ToString()
    {
        return $"{_ingredientName} - {_amount} {unit}";
    }
    public string IngredientName
    {
        get { return _ingredientName; }
        set
        {
            _ingredientName = value;
            OnPropertyChanged("IngredientName");
        }
    }

    public decimal Amount
    {
        get { return _amount; }
        set
        {
            _amount = value;
            OnPropertyChanged("Amount");
        }
    }

    public string Unit
    {
        get { return unit; }
        set
        {
            unit = value;
            OnPropertyChanged("Unit");
        }
    }
    // Implement INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
