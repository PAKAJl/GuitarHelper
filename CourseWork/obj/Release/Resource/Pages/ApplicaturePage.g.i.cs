﻿#pragma checksum "..\..\..\..\Resource\Pages\ApplicaturePage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "B04C64B5CD356D538FEC1AF0A2ED10377345FED165696D14CF38A70227B60AAD"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using CourseWork.Resource.Pages;
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


namespace CourseWork.Resource.Pages {
    
    
    /// <summary>
    /// ApplicaturePage
    /// </summary>
    public partial class ApplicaturePage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 30 "..\..\..\..\Resource\Pages\ApplicaturePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button noteListUpButton;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\Resource\Pages\ApplicaturePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button noteListDownButton;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\Resource\Pages\ApplicaturePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView noteList;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\..\Resource\Pages\ApplicaturePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button typeListUpButton;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\..\Resource\Pages\ApplicaturePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button typeListDownButton;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\..\Resource\Pages\ApplicaturePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView typeChordList;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\..\Resource\Pages\ApplicaturePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label nameNoteLabel;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\..\Resource\Pages\ApplicaturePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image AplicatureImage;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\..\Resource\Pages\ApplicaturePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button listenButton;
        
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
            System.Uri resourceLocater = new System.Uri("/CourseWork;component/resource/pages/applicaturepage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Resource\Pages\ApplicaturePage.xaml"
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
            this.noteListUpButton = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\..\..\Resource\Pages\ApplicaturePage.xaml"
            this.noteListUpButton.Click += new System.Windows.RoutedEventHandler(this.noteListUpButton_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.noteListDownButton = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\..\Resource\Pages\ApplicaturePage.xaml"
            this.noteListDownButton.Click += new System.Windows.RoutedEventHandler(this.noteListDownButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.noteList = ((System.Windows.Controls.ListView)(target));
            
            #line 32 "..\..\..\..\Resource\Pages\ApplicaturePage.xaml"
            this.noteList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.noteList_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.typeListUpButton = ((System.Windows.Controls.Button)(target));
            
            #line 55 "..\..\..\..\Resource\Pages\ApplicaturePage.xaml"
            this.typeListUpButton.Click += new System.Windows.RoutedEventHandler(this.typeListUpButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.typeListDownButton = ((System.Windows.Controls.Button)(target));
            
            #line 56 "..\..\..\..\Resource\Pages\ApplicaturePage.xaml"
            this.typeListDownButton.Click += new System.Windows.RoutedEventHandler(this.typeListDownButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.typeChordList = ((System.Windows.Controls.ListView)(target));
            
            #line 57 "..\..\..\..\Resource\Pages\ApplicaturePage.xaml"
            this.typeChordList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.noteList_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.nameNoteLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.AplicatureImage = ((System.Windows.Controls.Image)(target));
            return;
            case 9:
            this.listenButton = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

