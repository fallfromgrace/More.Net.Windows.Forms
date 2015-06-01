using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace More.Net.Common.Forms.GraphicsOverlay
{
    public sealed class OverlayCollection : ObservableCollection<IOverlay>
    {
        internal GraphicalOverlay GraphicalOverlay
        {
            get;
            set;
        }

        internal OverlayCollection(GraphicalOverlay component)
            : base()
        {
            GraphicalOverlay = component;
            overlayForms = new ObservableCollection<OverlayForm>();
        }

        internal OverlayForm GetForm(IOverlay overlay)
        {
            return overlayForms.Single(f => f.Overlay == overlay);
        }

        internal IEnumerable<OverlayForm> GetForms()
        {
            return overlayForms;
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            OverlayForm form;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (IOverlay overlay in e.NewItems.Cast<IOverlay>())
                    {
                        form = new OverlayForm();
                        form.Overlay = overlay;
                        form.Owner = GraphicalOverlay.Owner;
                        overlayForms.Add(form);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (IOverlay overlay in e.OldItems.Cast<IOverlay>())
                    {
                        form = GetForm(overlay);
                        form.Dispose();
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    throw new NotSupportedException();
            }
            base.OnCollectionChanged(e);
        }

        private ObservableCollection<OverlayForm> overlayForms;
    }

    public sealed class GraphicalOverlay : Component
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets if this component has been attached.  Attaching occuring when this 
        /// component's parent has been attached to a form.
        /// </summary>
        public Boolean IsAttached
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the collection of overlays.
        /// </summary>
        public OverlayCollection Overlays
        {
            get { return overlayCollection; }
        }

        public Form Owner
        {
            get { return owner; }
        }

        /// <summary>
        /// Gets or sets the parent for this overlay component.
        /// </summary>
        public Control Parent
        {
            get { return parent; }
            set
            {
                if (value != null)
                {
                    OnParentChanging();
                    parent = value;
                    OnParentChanged();
                }
            }
        }

        #endregion

        #region Initialization

        public GraphicalOverlay()
        {
            InitializeComponent();
        }

        public GraphicalOverlay(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            components = new Container();
            controlHierachy = new List<Control>();
            overlayCollection = new OverlayCollection(this);
        }

        #endregion

        #region Property Changed Handlers

        private void OnParentChanging()
        {
            if (Parent != null)
            {
                DetachParentControls();
            }
        }

        private void OnParentChanged()
        {
            if (Parent != null)
            {
                AttachParentControls();
            }
        }

        private void AttachParentControls()
        {
            Control control;

            controlHierachy.Clear();
            control = Parent;
            while (control != null)
            {
                controlHierachy.Add(control);
                control.LocationChanged += Control_BoundsChanged;
                control.SizeChanged += Control_BoundsChanged;
                control.ParentChanged += Control_ParentChanged;
                control = control.Parent;
            }

            owner = controlHierachy.Last() as Form;
            if (owner == null)
            {
                IsAttached = false;
            }
            else
            {
                owner.Load += Owner_Load;
                IsAttached = true;
            }
        }

        private void Owner_Load(Object sender, EventArgs e)
        {
            Owner.Load -= Owner_Load;
            CanShowOverlays = true;
            foreach (OverlayForm form in overlayCollection.GetForms())
                form.ShowWithoutActivate();
        }
        private Boolean CanShowOverlays;

        private void DetachParentControls()
        {
            foreach (Control control in controlHierachy)
            {
                control.LocationChanged -= Control_BoundsChanged;
                control.SizeChanged -= Control_BoundsChanged;
                control.ParentChanged -= Control_ParentChanged;
            }
            controlHierachy.Clear();
        }

        private void Control_BoundsChanged(Object sender, EventArgs e)
        {
            if (IsAttached)
            {
                Rectangle newBounds = GetBounds();
                if (bounds != newBounds)
                {
                    bounds = newBounds;
                    foreach (OverlayForm form in overlayCollection.GetForms())
                    {
                        form.Bounds = bounds;
                        form.Invalidate();
                    }
                }
            }
        }

        private void Control_ParentChanged(Object sender, EventArgs e)
        {
            DetachParentControls();
            AttachParentControls();

            if (IsAttached)
            {
                bounds = GetBounds();
                foreach (OverlayForm form in overlayCollection.GetForms())
                {
                    form.Bounds = bounds;
                    form.Owner = owner;
                    if (CanShowOverlays)
                        form.ShowWithoutActivate();
                }
            }
        }

        #endregion

        #region Overrides

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(Boolean disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Gets the bounding box for the designated control in screen pixel coordinates.
        /// </summary>
        /// <returns>Bounding rectangle</returns>
        private Rectangle GetBounds()
        {
            Int32 x, y;

            if (Owner.FormBorderStyle == FormBorderStyle.None)
            {
                x = 0;
                y = 0;
            }
            else
            {
                x = 4;
                //y = 30;
                y = 0;
            }

            foreach (Control ctrl in controlHierachy)
            {
                if (ctrl.Left < 0)
                    x += -4;
                else
                    x += ctrl.Left;

                if (ctrl.Top > 0)
                    y += ctrl.Top;
            }

            return new Rectangle(x, y, Parent.Width, Parent.Height);
        }

        #endregion

        #region Private Fields

        private Rectangle bounds;
        private IContainer components;
        private List<Control> controlHierachy;
        private OverlayCollection overlayCollection;
        private Form owner;
        private Control parent;

        #endregion
    }
}
