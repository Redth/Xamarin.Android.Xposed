using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Xposed.Callbacks;

[assembly: MetaData("xposedmodule", Value="true")]
[assembly: MetaData("xposeddescription", Value="This is an example xposed module from Xamarin")]
[assembly: MetaData("xposedminversion", Value = "82")]

namespace XposedSample
{
	public class SampleModule : Java.Lang.Object, Xposed.IXposedHookLoadPackage
	{
		public void HandleLoadPackage(XC_LoadPackage.LoadPackageParam package)
		{
			Xposed.XposedBridge.Log("Loaded app: " + package.PackageName);

			if (!package.PackageName.Equals("com.android.systemui"))
				return;

			Xposed.XposedHelpers.FindAndHookMethod(
				"com.android.systemui.statusbar.policy.Clock", 
				package.ClassLoader, "updateClock",
				Xposed.XC_MethodHook.Create(afterHookedMethodHandler: afterHookedParam => {
					TextView tv = (TextView)afterHookedParam.ThisObject;
					String text = tv.Text;
					tv.Text = text + " :)";
					tv.SetTextColor(Android.Graphics.Color.Red);
				})
			);
		}
	}
}

