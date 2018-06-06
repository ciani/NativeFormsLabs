// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace NativeFormsLabs.iOS.Features.Login
{
    [Register ("LoginView")]
    partial class LoginView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton BtnLogin { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LblPassword { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LblUsername { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIActivityIndicatorView LoadingIndicator { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField TxtPassword { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField TxtUsername { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (BtnLogin != null) {
                BtnLogin.Dispose ();
                BtnLogin = null;
            }

            if (LblPassword != null) {
                LblPassword.Dispose ();
                LblPassword = null;
            }

            if (LblUsername != null) {
                LblUsername.Dispose ();
                LblUsername = null;
            }

            if (LoadingIndicator != null) {
                LoadingIndicator.Dispose ();
                LoadingIndicator = null;
            }

            if (TxtPassword != null) {
                TxtPassword.Dispose ();
                TxtPassword = null;
            }

            if (TxtUsername != null) {
                TxtUsername.Dispose ();
                TxtUsername = null;
            }
        }
    }
}