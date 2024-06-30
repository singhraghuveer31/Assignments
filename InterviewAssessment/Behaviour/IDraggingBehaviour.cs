using System;
using System.Windows;

namespace DomainModelEditor.Behaviour
{
    public interface IDraggingBehaviour
    {
        event EventHandler<DragFinishedEventArgs> DragFinished;

        void ApplyToMovableElement(UIElement movableElement);
        void ApplyToContainer(UIElement element);
    }
}
