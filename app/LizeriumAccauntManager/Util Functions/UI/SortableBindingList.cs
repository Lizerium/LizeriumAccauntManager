/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 20 апреля 2026 03:07:02
 * Version: 1.0.15
 */

using System.Collections.Generic;
using System.ComponentModel;

public class SortableBindingList<T> : BindingList<T>
{
    private bool isSorted;
    private PropertyDescriptor sortProperty;
    private ListSortDirection sortDirection;

    public SortableBindingList() : base() { }

    public SortableBindingList(IList<T> list) : base(list) { }

    public SortableBindingList(IEnumerable<T> enumerable) : base(new List<T>(enumerable)) { }

    protected override bool SupportsSortingCore => true;
    protected override bool IsSortedCore => isSorted;
    protected override PropertyDescriptor SortPropertyCore => sortProperty;
    protected override ListSortDirection SortDirectionCore => sortDirection;

    protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
    {
        var itemsList = (List<T>)Items;
        itemsList.Sort((a, b) =>
        {
            var aValue = prop.GetValue(a);
            var bValue = prop.GetValue(b);
            return direction == ListSortDirection.Ascending
                ? Comparer<object>.Default.Compare(aValue, bValue)
                : Comparer<object>.Default.Compare(bValue, aValue);
        });

        sortProperty = prop;
        sortDirection = direction;
        isSorted = true;

        OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
    }

    protected override void RemoveSortCore() => isSorted = false;
}
