using FluentUI.Components.UI.Tables.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using System.Xml.XPath;

namespace FluentUI.Components.UI.Tables;

public partial class CustomizableTable<TItem>
{
    FluentDataGrid<TItem>? grid;
    PaginationState pagination = new PaginationState { ItemsPerPage = 20 };
    IDialogReference? _dialog;

    [Inject]
    public required IMessageService MessageService { get; set; }

    [Inject]
    public required IDialogService DialogService { get; set; }

    [Parameter]
    public IQueryable<TItem> Items { get; set; } = default!;

    [Parameter]
    public List<TableColumn<TItem>> Columns { get; set; } = new();
    private IEnumerable<TableColumn<TItem>> VisibleColumns => Columns.Where(c => c.IsVisible).OrderBy(m => m.SortOrder);

    private string GridTemplateColumns
    {
        get
        {
            var visibleCols = VisibleColumns.ToList();
            if (!visibleCols.Any()) return "1fr";
            return string.Join(" ", visibleCols.Select(c => c.Width ?? "1fr"));
        }
    }


    private async Task OpenPanel()
    {
        Console.WriteLine($"Open right panel");
        // store the original state of the columns to allow reverting changes if needed
        var originalState = Columns.Select((col, index) => (
        Column: col,
        Isvisible: col.IsVisible,
        SortOrder: col.SortOrder,
        OriginalIndex: index
        )).ToList();

        MessageService.Clear("MESSAGES_COLUMNEDIT");

        _dialog = await DialogService.ShowPanelAsync<TableDialogPanel<TItem>>(Columns, new DialogParameters()
        {
            Alignment = HorizontalAlignment.Right,
            Width = "600px",
            PreventDismissOnOverlayClick = true,
        });

        DialogResult result = await _dialog.Result;

        // if dialog was cancelled (dismissed), restore original state
        if(result.Cancelled)
        {
            foreach(var state in originalState)
            {
                state.Column.IsVisible = state.Isvisible;
                state.Column.SortOrder = state.SortOrder;
            }
            Columns.Clear();
            foreach(var state in originalState.OrderBy(s => s.OriginalIndex))
            {
                Columns.Add(state.Column);
            }
        }

        // Force re-render to show/hide columns based on visibility changes
        StateHasChanged();
    }
}
