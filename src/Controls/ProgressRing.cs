using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Xml.Linq;

namespace WYW.UI.Controls
{

    [TemplateVisualState(GroupName = GroupActiveStates, Name = StateInactive)]
    [TemplateVisualState(GroupName = GroupActiveStates, Name = StateActive)]
    public class ProgressRing: Control
    {
        private const string GroupActiveStates = "ActiveStates";
        private const string StateInactive = "Inactive";
        private const string StateActive = "Active";

        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register("IsActive", typeof(bool), typeof(ProgressRing), new PropertyMetadata(true, OnIsActiveChanged));
        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }
        private static void OnIsActiveChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((ProgressRing)o).GotoCurrentState(true);
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            GotoCurrentState(false);
        }
        private void GotoCurrentState(bool animate)
        {
            var state = this.IsActive ? StateActive : StateInactive;
            VisualStateManager.GoToState(this, state, animate);
        }


        public TemplateStyle RingStyle
        {
            get { return (TemplateStyle)GetValue(RingStyleProperty); }
            set { SetValue(RingStyleProperty, value); }
        }

        public static readonly DependencyProperty RingStyleProperty =
            DependencyProperty.Register("RingStyle", typeof(TemplateStyle), typeof(ProgressRing), new PropertyMetadata(default(TemplateStyle)));


        public enum TemplateStyle
        {
            Circle=0,
            /// <summary>
            /// 平面旋转
            /// </summary>
            RotatingPlane,
            DoubleBounce,
            ThreeBounce,
            Wave,
            WanderingCubes,
            Pulse,
            ChasingDots,
        }

    }
}
