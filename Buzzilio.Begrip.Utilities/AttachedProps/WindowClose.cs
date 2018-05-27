// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WindowClose.cs" company="Reed Copsey, Jr.">
//   Copyright 2011, Reed Copsey, Jr.
// </copyright>
// <summary>
//   Simple behavior to prevent a window from being closable, based on a data bindable boolean property
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Buzzilio.Begrip.Utilities.AttachedProps
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// Class holding the Window Closed Attached Properties
    /// </summary>
    public static class WindowClose
    {
        /// <summary>
        /// Identifies the CloseCommand dependency property.
        /// </summary>
        [SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Public field used for Depedency Property.")]
        public static DependencyProperty CloseCommandProperty = DependencyProperty.RegisterAttached("CloseCommand", typeof(ICommand), typeof(WindowClose), new PropertyMetadata(null, CloseCommandPropertyChangedCallback));

        /// <summary>
        /// Identifies the CloseFailCommand dependency property.
        /// </summary>
        [SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Public field used for Depedency Property.")]
        public static DependencyProperty CloseFailCommandProperty = DependencyProperty.RegisterAttached("CloseFailCommand", typeof(ICommand), typeof(WindowClose), new PropertyMetadata(null));

        /// <summary>
        /// Gets the CloseCommand attached property.
        /// </summary>
        /// <param name="dependencyObject">The window.</param>
        /// <returns>The ICommand bound to this property</returns>
        public static ICommand GetCloseCommand(DependencyObject dependencyObject)
        {
            return (ICommand)dependencyObject.GetValue(CloseCommandProperty);
        }

        /// <summary>
        /// Sets the close command.
        /// </summary>
        /// <param name="dependencyObject">The window.</param>
        /// <param name="value">The ICommand to bind to this property.</param>
        public static void SetCloseCommand(DependencyObject dependencyObject, ICommand value)
        {
            dependencyObject.SetValue(CloseCommandProperty, value);
        }

        /// <summary>
        /// Gets the CloseFailCommand attached property.
        /// </summary>
        /// <param name="dependencyObject">The window.</param>
        /// <returns>The ICommand bound to this property</returns>
        public static ICommand GetCloseFailCommand(DependencyObject dependencyObject)
        {
            return (ICommand)dependencyObject.GetValue(CloseFailCommandProperty);
        }

        /// <summary>
        /// Sets the CloseFailCommand.
        /// </summary>
        /// <param name="dependencyObject">The window.</param>
        /// <param name="value">The ICommand to bind to this property.</param>
        public static void SetCloseFailCommand(DependencyObject dependencyObject, ICommand value)
        {
            dependencyObject.SetValue(CloseFailCommandProperty, value);
        }

        /// <summary>
        /// Callback for the PropertyChanged event of the CloseCommand Dependency Property
        /// </summary>
        /// <param name="d">The DependencyObject</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void CloseCommandPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Window instance = d as Window;
            if (instance != null)
            {
                ICommand newValue = e.NewValue as ICommand;
                ICommand oldValue = e.OldValue as ICommand;

                if (oldValue != null)
                {
                    instance.Closing -= OnWindowClosing;
                    instance.Closed -= OnWindowClosed;
                }

                if (newValue != null)
                {
                    instance.Closing += OnWindowClosing;
                    instance.Closed += OnWindowClosed;
                }
            }
        }

        /// <summary>
        /// Called when the window is closed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private static void OnWindowClosed(object sender, System.EventArgs e)
        {
            DependencyObject dependencyObject = sender as Window;
            if (dependencyObject == null)
            {
                return;
            }

            ICommand closeCommand = GetCloseCommand(dependencyObject);

            if (closeCommand != null)
            {
                closeCommand.Execute(null);
            }
        }

        /// <summary>
        /// Called when the window is closing.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private static void OnWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DependencyObject dependencyObject = sender as Window;
            if (dependencyObject == null)
            {
                return;
            }

            // Cancel if we can't close...
            bool cancel = false;
            ICommand closeCommand = GetCloseCommand(dependencyObject);
            ICommand closeFailCommand = GetCloseFailCommand(dependencyObject);

            if (closeCommand != null)
            {
                cancel = !closeCommand.CanExecute(null);
            }

            // If we're not allowed to close, execute the CloseFail command
            if (cancel && closeFailCommand != null)
            {
                closeFailCommand.Execute(null);
            }

            e.Cancel = cancel;
        }
    }
}
