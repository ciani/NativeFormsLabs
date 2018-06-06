// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace NativeFormsLabs.iOS.Features.MapDetail
{
    [Register ("MapDetailView")]
    partial class MapDetailView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIActivityIndicatorView LoadingRing { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        MapKit.MKMapView MapPosition { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (LoadingRing != null) {
                LoadingRing.Dispose ();
                LoadingRing = null;
            }

            if (MapPosition != null) {
                MapPosition.Dispose ();
                MapPosition = null;
            }
        }
    }
}