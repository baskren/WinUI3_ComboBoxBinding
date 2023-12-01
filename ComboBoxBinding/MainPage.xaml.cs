using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace ComboBoxBinding;

public sealed partial class MainPage : Page
{
    #region ViewModel Property
    public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
        nameof(ViewModel),
        typeof(object),
        typeof(MainPage),
        new PropertyMetadata(default)
    );
    public object ViewModel
    {
        get => (object)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }
    #endregion ViewModel Property

    #region Item Property
    public static readonly DependencyProperty ItemProperty = DependencyProperty.Register(
        nameof(Item),
        typeof(object),
        typeof(MainPage),
        new PropertyMetadata(default)
    );
    public object Item
    {
        get => (object)GetValue(ItemProperty);
        set => SetValue(ItemProperty, value);
    }
    #endregion Item Property


    public MainPage()
    {

        this.InitializeComponent();

        textBlockProg.Bind(TextBlock.TextProperty, this, nameof(Item));
        comboBoxProg.Bind(ComboBox.ItemsSourceProperty, this, nameof(ViewModel));
        comboBoxProg.Bind(ComboBox.SelectedItemProperty, this, nameof(Item));
    }

    private void button_Click1(object sender, RoutedEventArgs e)
    {
        Item = "A";
    }

    private void button_Click2(object sender, RoutedEventArgs e)
    {
        ViewModel = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P" };
    }

    private void button_Click3(object sender, RoutedEventArgs e)
    {
        ViewModel = null;
        Item = null;
    }

}
