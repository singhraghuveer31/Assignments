using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DomainModelEditor.Extension;
using Serilog;

namespace DomainModelEditor.Behaviour
{
    /// <summary>
    /// Behaviour to drag an entity accross the canvas while holding the left mousebutton down.
    /// </summary>
    public class DraggingBehaviour : IDisposable, IDraggingBehaviour
    {
        private static ILogger _logger = null;
        private static ILogger Logger
        {
            get
            {
                if (_logger == null)
                    _logger = Log.Logger.ForContext<DraggingBehaviour>();

                return _logger;
            }
        }
        
        private static UIElement CurrentlyMoving = null;
        private static Canvas CurrentlyMovingCanvas = null;
        private static UIElement CurrentlyMovingVisualParent = null;

        public double CurrentMovingOriginX;
        public double CurrentMovingOriginY;

        private static TranslateTransform MouseMovementTranslation = null;
        private static Point? TranslationOriginPoint = null;

        public event EventHandler<DragFinishedEventArgs> DragFinished;

        protected virtual void OnDragFinished(DragFinishedEventArgs e)
        {
            DragFinished?.Invoke(this, e);
        }

        public void ApplyToMovableElement(UIElement movableElement)
        {
            movableElement.MouseDown += MouseDown;
            movableElement.MouseMove += MouseMove;
            movableElement.MouseUp += MouseUp;
        }

        public void ApplyToContainer(UIElement container)
        {
            container.MouseMove += MouseMove;
            container.MouseUp += MouseUp;
        }

        private void MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                // Recorded to allow continuous movement while sliding under another entity.
                CurrentlyMoving = (UIElement)sender;
                CurrentlyMovingCanvas = CurrentlyMoving.FindParent<Canvas>();

                CurrentlyMovingVisualParent = VisualTreeHelper.GetParent(CurrentlyMoving) as UIElement;
                CurrentMovingOriginX = Canvas.GetLeft(CurrentlyMovingVisualParent);
                CurrentMovingOriginY = Canvas.GetTop(CurrentlyMovingVisualParent);

                var mouseLocation = e.GetPosition(CurrentlyMovingCanvas);

                TranslationOriginPoint = new Point(mouseLocation.X, mouseLocation.Y);

                MouseMovementTranslation = new TranslateTransform(TranslationOriginPoint.Value.X, TranslationOriginPoint.Value.Y);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in {handler} event handler.", nameof(MouseDown));
                ResetState();
            }
        }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && TranslationOriginPoint != null)
            {
                try { 
                    Mouse.OverrideCursor = Cursors.Hand;

                    var mouseLocation = e.GetPosition(CurrentlyMovingCanvas);

                    MouseMovementTranslation.X = mouseLocation.X - TranslationOriginPoint.Value.X;
                    MouseMovementTranslation.Y = mouseLocation.Y - TranslationOriginPoint.Value.Y;

                    Canvas.SetLeft(CurrentlyMovingVisualParent, MouseMovementTranslation.X + CurrentMovingOriginX);
                    Canvas.SetTop(CurrentlyMovingVisualParent, MouseMovementTranslation.Y + CurrentMovingOriginY);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, "Error in {handler} event handler.", nameof(MouseMove));
                }
            }
        }

        private void MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (CurrentlyMoving == null)
                return;
            
            try
            {
                var mouseLocation = e.GetPosition(CurrentlyMovingCanvas);

                MouseMovementTranslation.X = mouseLocation.X - TranslationOriginPoint.Value.X;
                MouseMovementTranslation.Y = mouseLocation.Y - TranslationOriginPoint.Value.Y;

                OnDragFinished(new DragFinishedEventArgs
                {
                    DraggedElement = CurrentlyMoving,
                    X = (int)(MouseMovementTranslation.X + CurrentMovingOriginX),
                    Y = (int)(MouseMovementTranslation.Y + CurrentMovingOriginY)
                });
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in {handler} event handler.", nameof(MouseUp));
            }

            ResetState();
        }

        private void ResetState()
        {
            CurrentlyMoving = null;
            CurrentlyMovingCanvas = null;
            CurrentlyMovingVisualParent = null;
            MouseMovementTranslation = null;
            TranslationOriginPoint = null;

            Mouse.OverrideCursor = null;
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (disposedValue)
                return;
            
            if (disposing)
            {
                ResetState();
            }

            disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }

    public class DragFinishedEventArgs : EventArgs
    {
        public int X { get; internal set; }
        public int Y { get; internal set; }
        public UIElement DraggedElement { get; internal set; }
    }
}
