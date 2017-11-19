using System;
namespace Xposed
{
	public partial class XC_MethodHook
	{
		public static XC_MethodHook Create(Action<XC_MethodHook.MethodHookParam> beforeHookedMethodHandler = null,
											Action<XC_MethodHook.MethodHookParam> afterHookedMethodHandler = null)
		{
			return new XC_MethodHook_Impl
			{
				BeforeHookedMethodHandler = beforeHookedMethodHandler,
				AfterHookedMethodHandler = afterHookedMethodHandler
			};
		}
	}

	class XC_MethodHook_Impl : Xposed.XC_MethodHook
	{
		public Action<Xposed.XC_MethodHook.MethodHookParam> BeforeHookedMethodHandler;
		public Action<Xposed.XC_MethodHook.MethodHookParam> AfterHookedMethodHandler;

		protected override void BeforeHookedMethod(Xposed.XC_MethodHook.MethodHookParam param)
		{
			BeforeHookedMethodHandler?.Invoke(param);
			base.BeforeHookedMethod(param);
		}

		protected override void AfterHookedMethod(Xposed.XC_MethodHook.MethodHookParam param)
		{
			AfterHookedMethodHandler?.Invoke(param);
			base.AfterHookedMethod(param);
		}
	}
}
