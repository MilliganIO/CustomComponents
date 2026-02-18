using FluentUI.Components.UI.Tables.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

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

        MessageService.Clear(App.MESSAGES_DIALOG);

        _dialog = await DialogService.ShowPanelAsync<TableDialogPanel<TItem>>(Columns, new DialogParameters()
        {
            Alignment = HorizontalAlignment.Right,
            Width = "600px",
            PreventDismissOnOverlayClick = true,
        });

        DialogResult result = await _dialog.Result;

        // Force re-render to show/hide columns based on visibility changes
        StateHasChanged();
    }
}
