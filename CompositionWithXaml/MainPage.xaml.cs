using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CompositionWithXaml
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.RotateTopLayer.IsEnabled = true;
            this.RotateBottomLayer.IsEnabled = true;
        }

        private void AnimateTopLayer_Click(object sender, RoutedEventArgs e)
        {
            this.RotateElement(this.TopLayer);
        }

        private void AnimateBottomLayer_Click(object sender, RoutedEventArgs e)
        {
            this.RotateElement(this.BottomLayer);
        }
        private static ContainerVisual GetVisual(FrameworkElement element)
        {
            return (ContainerVisual)ElementCompositionPreview.GetContainerVisual(element);
        }

        private void RotateElement(FrameworkElement element)
        {
            var visual = GetVisual(element);
            var compositor = visual.Compositor;

            visual.CenterPoint = new Vector3(Convert.ToSingle(element.Width / 2), Convert.ToSingle(element.Height / 2), 0.0f);

            var random = new Random();
            var angle = ((float)random.NextDouble()) * 180.0f - 90.0f;

            var rotationAnimation = compositor.CreateScalarKeyFrameAnimation();
            rotationAnimation.Duration = TimeSpan.FromMilliseconds(1000);
            rotationAnimation.InsertKeyFrame(1.0f, angle);

            var animator = visual.ConnectAnimation("RotationAngle", rotationAnimation);
            animator.Start();
        }
    }
}
