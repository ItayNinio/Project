﻿#pragma checksum "C:\Users\itayn\OneDrive\Desktop\github\Project\Pixel_Rambo\Pixel_Rambo\Pages\DemoGame.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "5C22BA7AE786A99A30157F6CAED90AD0FB8B67791A7B8EEC9FFCC8C149A9414A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Pixel_Rambo.Pages
{
    partial class DemoGame : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Pages\DemoGame.xaml line 13
                {
                    this.BackgroundMusic = (global::Windows.UI.Xaml.Controls.MediaElement)(target);
                }
                break;
            case 3: // Pages\DemoGame.xaml line 14
                {
                    this.GunShot = (global::Windows.UI.Xaml.Controls.MediaElement)(target);
                }
                break;
            case 4: // Pages\DemoGame.xaml line 16
                {
                    this.GameCanvas = (global::Windows.UI.Xaml.Controls.Canvas)(target);
                }
                break;
            case 5: // Pages\DemoGame.xaml line 18
                {
                    this.YourObject = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 6: // Pages\DemoGame.xaml line 20
                {
                    this.obstacle1 = (global::Windows.UI.Xaml.Shapes.Rectangle)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

