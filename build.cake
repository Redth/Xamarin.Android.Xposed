#addin nuget:?package=Cake.Xamarin.Build
#addin nuget:?package=Cake.Xamarin

var TARGET = Argument ("t", Argument ("target", "Default"));

var NUGET_VERSION = "0.82";
var JAR_VERSION = "82";
var SDK_URL = $"https://bintray.com/rovo89/de.robv.android.xposed/download_file?file_path=de%2Frobv%2Fandroid%2Fxposed%2Fapi%2F82%2Fapi-{JAR_VERSION}.jar";

Task ("libs")
	.IsDependentOn ("externals")
	.Does (() => 
{
	MSBuild ("./Xamarin.Android.Xposed.sln");
});

Task ("externals")
	.Does (() => 
{
	var SDK_JAR = "./externals/xposed-api.jar";

	EnsureDirectoryExists ("./externals/");
	
	if (!FileExists (SDK_JAR))
		DownloadFile (SDK_URL, SDK_JAR);
});

Task ("clean")
	.Does (() => 
{	
	if (DirectoryExists ("./externals"))
		DeleteDirectory ("./externals", true);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);