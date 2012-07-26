. .\Libs\psake\teamcity.ps1

properties {
    $base_directory = resolve-path .
	$solution_name = "DomFx"
	$solution_file = "$base_directory\KSLog.sln"
    $libs_directory = "$base_directory\libs"
    $build_directory = "$base_directory\build"
    $test_runner = "$libs_directory\xunit\xunit.console.exe"
	$msbuild = "C:\windows\microsoft.net\Framework\v3.5\MSBuild.exe"
}

task Test { 
	&$test_runner "DomFx.Tests\bin\Release\DomFx.Tests.dll"
}