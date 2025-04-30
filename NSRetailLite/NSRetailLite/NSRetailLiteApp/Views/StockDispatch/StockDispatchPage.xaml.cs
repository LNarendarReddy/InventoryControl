using CommunityToolkit.Maui.Core.Extensions;
using DevExpress.Data.Extensions;
using DevExpress.Maui.Pdf.Internal;
using NSRetailLiteApp.Models;
using NSRetailLiteApp.ViewModels.StockDispatch;
using NSRetailLiteApp.ViewModels.StockDispatch.Indent;
using System.Collections.ObjectModel;
using System.Globalization;

namespace NSRetailLiteApp.Views.StockDispatch;

public partial class StockDispatchPage : TabbedPage
{
	public StockDispatchPage(StockDispatchViewModel stockDispatchViewModel)
	{
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        NavigationPage.SetHasBackButton(this, false);
        BindingContext = stockDispatchViewModel;
        StockDispatchViewModel = stockDispatchViewModel;
    }

    public StockDispatchViewModel StockDispatchViewModel { get; }

    private void TabbedPage_Loaded(object sender, EventArgs e)
    {
        if (StockDispatchViewModel.IsManual)
            this.CurrentPage = ManualDipstachContent;
    }

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbTrayWisePicker.SelectedItem == null) return;

        TrayWiseGroup findGroupToScroll = 
            StockDispatchViewModel.TrayWiseData.FirstOrDefault(x => x.Name == (cmbTrayWisePicker.SelectedItem as TrayInfo).TrayNumber.ToString());

        if (findGroupToScroll != null) cvTrayWiseData.ScrollTo(findGroupToScroll[0], findGroupToScroll, ScrollToPosition.Center);

    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        StockDispatchViewModel.BuildTrayWiseData();
    }

    private void IndentDipstachContent_Loaded(object sender, EventArgs e)
    {
        txtIndentSearchCode.Focus();
    }
}

public class ColorChangedConverter : IValueConverter
{
    public ColorChangedConverter()
    {
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        //return value as Color;
        if ((int)value == 1)
        {
            return System.Drawing.Color.Transparent.ToMauiColor();
        }
        else if ((int)value == 2)
        {
            return Colors.Orange;
        }
        else if ((int)value == 3)
        {
            return Colors.Green;
        }

        return System.Drawing.Color.Transparent.ToMauiColor();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
