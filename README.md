# Demonstration of WinUI 3 ComboBox.SelectedItem programmatic binding failure

This app has two combo boxes, both having their `ComboBox.ItemsSource` bound to the same class instance DependencyProperty (`MainPage.ViewModel` in demo project) and both have their `ComboBox.SelectedItem` bound to the same class instance class DependencyProperty (`MainPage.Item` in demo project).   The only difference is that one is bound via XAML and the other is bound programmatically.

### FIRST COMBO BOX

```xaml
      <TextBlock AutomationProperties.AutomationId="HelloTextBlock"
          Text="{x:Bind Item, Mode=OneWay}"
          HorizontalAlignment="Center" />
      <ComboBox ItemsSource="{x:Bind ViewModel, Mode=OneWay}" SelectedItem="{x:Bind Item, Mode=TwoWay}" />
```

### SECOND COMBO BOX

```xaml
    <StackPanel
      Grid.Column="1"
      Spacing="10"
      HorizontalAlignment="Center"
      VerticalAlignment="Center"
      >
      <TextBlock Text="PROGRAMMATIC BIND" />
      <TextBlock x:Name="textBlockProg" />
      <ComboBox x:Name="comboBoxProg"  />
    </StackPanel>
```

```C#
        textBlockProg.Bind(TextBlock.TextProperty, this, nameof(Item));
        comboBoxProg.Bind(ComboBox.ItemsSourceProperty, this, nameof(ViewModel));
        comboBoxProg.Bind(ComboBox.SelectedItemProperty, this, nameof(Item));
```

where the `.Bind` extension method is:
```C#
    public static void Bind(this DependencyObject target, DependencyProperty targetProperty, object source, string path = null)
    {
        var binding = new Binding
        {
            Source = source,
            Mode = BindingMode.OneWay
        };
        if (!string.IsNullOrWhiteSpace(path))
            binding.Path = new PropertyPath(path);
        BindingOperations.SetBinding(target, targetProperty, binding);
    }

```


The first time `MainPage.Item` and `MainPage.ViewModel` are set, both **ComboBoxes** show the correct corresponding content for `ComboBox.ItemsSource` and `ComboBox.SelectedItem`.  

```C#
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
```
However, after setting the `MainPage.ViewModel` and `MainPage.Item` properties to null, _and then setting`MainPage.ViewModel` and `MainPage.Item` again_, only the ComboBox that has been bound via XAML will show the correct corresponding content for its SelectedItem.  The programmatically bound combo box does not show any content for its SelectedItem.  HOWEVER, BOTH COMBO BOXES SHOW ItemsSource = ViewModel!