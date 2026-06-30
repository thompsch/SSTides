using System.Windows.Input;
using Syncfusion.Maui.Maps;

namespace SalishSeaTides.Behaviors;

// Bridges the Syncfusion MapLayer.MarkerSelected event to an ICommand, since
// CommunityToolkit's EventToCommandBehavior does not support MapLayer.
public static class MarkerCommand
{
    public static readonly BindableProperty CommandProperty =
        BindableProperty.CreateAttached(
            "Command",
            typeof(ICommand),
            typeof(MarkerCommand),
            default(ICommand),
            propertyChanged: OnCommandChanged);

    public static ICommand? GetCommand(BindableObject view) =>
        (ICommand?)view.GetValue(CommandProperty);

    public static void SetCommand(BindableObject view, ICommand? value) =>
        view.SetValue(CommandProperty, value);

    static void OnCommandChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not MapLayer layer)
            return;

        layer.MarkerSelected -= OnMarkerSelected;
        if (newValue is ICommand)
            layer.MarkerSelected += OnMarkerSelected;
    }

    static void OnMarkerSelected(object? sender, MarkerSelectedEventArgs e)
    {
        if (sender is not MapLayer layer)
            return;

        var command = GetCommand(layer);
        var parameter = e.SelectedMarker;
        if (command?.CanExecute(parameter) == true)
            command.Execute(parameter);
    }
}
