﻿#pragma checksum "..\..\..\Windows\MetronomeWin.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "D8EDEF11D7FC95B2121DCD2A4D960DBB76C50962BF65E4726EEC73A3990FF79B"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using CourseWork.Windows;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace CourseWork.Windows {
    
    
    /// <summary>
    /// MetronomeWin
    /// </summary>
    public partial class MetronomeWin : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 29 "..\..\..\Windows\MetronomeWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button minusTickButton;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\Windows\MetronomeWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button plusTickButton;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\Windows\MetronomeWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox beatsInMinBox;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\Windows\MetronomeWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button minusTackButton;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\Windows\MetronomeWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button plusTackButton;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\Windows\MetronomeWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ticksInTackBox;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\Windows\MetronomeWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel indicators;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\Windows\MetronomeWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button startButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/CourseWork;component/windows/metronomewin.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\MetronomeWin.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 9 "..\..\..\Windows\MetronomeWin.xaml"
            ((CourseWork.Windows.MetronomeWin)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.minusTickButton = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\..\Windows\MetronomeWin.xaml"
            this.minusTickButton.Click += new System.Windows.RoutedEventHandler(this.minusTickButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.plusTickButton = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\..\Windows\MetronomeWin.xaml"
            this.plusTickButton.Click += new System.Windows.RoutedEventHandler(this.plusTickButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.beatsInMinBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 31 "..\..\..\Windows\MetronomeWin.xaml"
            this.beatsInMinBox.LostFocus += new System.Windows.RoutedEventHandler(this.beatsInMinBox_LostFocus);
            
            #line default
            #line hidden
            return;
            case 5:
            this.minusTackButton = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\..\Windows\MetronomeWin.xaml"
            this.minusTackButton.Click += new System.Windows.RoutedEventHandler(this.minusTackButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.plusTackButton = ((System.Windows.Controls.Button)(target));
            
            #line 43 "..\..\..\Windows\MetronomeWin.xaml"
            this.plusTackButton.Click += new System.Windows.RoutedEventHandler(this.plusTackButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ticksInTackBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 44 "..\..\..\Windows\MetronomeWin.xaml"
            this.ticksInTackBox.LostFocus += new System.Windows.RoutedEventHandler(this.ticksInTackBox_LostFocus);
            
            #line default
            #line hidden
            return;
            case 8:
            this.indicators = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 9:
            this.startButton = ((System.Windows.Controls.Button)(target));
            
            #line 50 "..\..\..\Windows\MetronomeWin.xaml"
            this.startButton.Click += new System.Windows.RoutedEventHandler(this.startButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

