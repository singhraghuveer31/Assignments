using System.Windows;
using System.Windows.Media;

namespace DomainModelEditor.Extension
{
    public static class DependencyObjectExtensions
    {
        public static T FindParent<T>(this DependencyObject currentElement) where T : DependencyObject
        {
            DependencyObject parent = currentElement;

            while (parent != null)
            {
                parent = VisualTreeHelper.GetParent(parent);

                if (parent is T typedParent) return typedParent;
            }

            return null;
        }
    }
}
