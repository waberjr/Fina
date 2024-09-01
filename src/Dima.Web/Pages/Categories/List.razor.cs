using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Categories;

public partial class ListCategoriesPage : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; }
    // protected List<Category> Categories { get; set; } = [];
    // public string SearchTerm { get; set; } = string.Empty;

    public MudDataGrid<Category>? CategoriesGrid;

    #endregion

    #region Services

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    public IDialogService DialogService { get; set; } = null!;

    [Inject]
    public ICategoryHandler CategoryHandler { get; set; } = null!;

    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        // IsBusy = true;
        // try
        // {
        //     var request = new GetAllCategoriesRequest();
        //     var result = await CategoryHandler.GetAllAsync(request);
        //     if (result.IsSuccess)
        //         Categories = result.Data ?? [];
        // }
        // catch (Exception ex)
        // {
        //     Snackbar.Add(ex.Message, Severity.Error);
        // }
        // finally
        // {
        //     IsBusy = false;
        // }
    }

    #endregion

    #region Methods

    protected async Task<GridData<Category>> LoadCategories(GridState<Category> state)
    {
        IsBusy = true;
        try
        {
            var request = new GetAllCategoriesRequest
            {
                PageNumber = state.Page + 1,
                PageSize = state.PageSize
            };
            var result = await CategoryHandler.GetAllAsync(request);

            return new GridData<Category>
            {
                Items = result.Data ?? [],
                TotalItems = result.TotalCount
            };
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }

        return new GridData<Category>
        {
            Items = [],
            TotalItems = 0
        };
    }

    public async void OnDeleteButtonClickedAsync(long id, string title)
    {
        var result = await DialogService.ShowMessageBox(
            "ATENÇÃO",
            $"Ao prosseguir a categoria {title} será excluída. Esta é uma ação irreversível! Deseja continuar?",
            yesText: "EXCLUIR",
            cancelText: "Cancelar");

        if (result is true)
            await OnDeleteAsync(id, title);

        StateHasChanged();
    }

    private async Task OnDeleteAsync(long id, string title)
    {
        try
        {
            var request = new DeleteCategoryRequest { Id = id };
            await CategoryHandler.DeleteAsync(request);
            // Categories.RemoveAll(x => x.Id == id);
            CategoriesGrid?.ReloadServerData();
            Snackbar.Add($"Categoria {title} excluída", Severity.Success);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }

    // public Func<Category, bool> Filter => category =>
    // {
    //     if (string.IsNullOrWhiteSpace(SearchTerm))
    //         return true;
    //
    //     if (category.Title.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
    //         return true;
    //
    //     if (category.Description is not null &&
    //         category.Description.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
    //         return true;
    //
    //     return false;
    // };

    #endregion
}