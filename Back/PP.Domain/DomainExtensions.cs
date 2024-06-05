using Microsoft.Identity.Client;

namespace PP.Domain;

public static class DomainExtensions
{
    public static void Replace<T>(this IList<T> oldSet, IList<T> newSet, out IList<T> additional, out IList<T> removed)
    {
        additional = newSet.Except(oldSet).Intersect(newSet).ToList();
        removed = oldSet.Except(newSet).ToList();
        
        foreach (var r in removed)
        {
            oldSet.Remove(r);
        }
        foreach (var a in additional)
        {
            oldSet.Add(a);
        }
    }

    public static void Replace<T>(this IList<T> oldSet, IList<T> newSet)
    {
        Replace(oldSet, newSet, out _, out _);
    }
}