buildmodifications
==================

my msbuild, tfs build, and other modification libraries

added the following to the end of the interesting .csproj file:

```
<Import Project="C:\projects\BCustomBuildTasks\BCustomBuildTasks\BMicrosoft.Common.targets" />
```

information for making this solution wide without editing the solution or any project files can be found here:

http://dotnet.geir-sorensen.net/2010/04/msbuild-custom-targets.html

which basically involves creating a custom.after.targets file in your solution folder I believe.

another approach is which can be done via tfs build, but probably not directly from visual studio

```
/p:CustomAfterMicrosoftCommonTargets=C:\projects\BCustomBuildTasks\BCustomBuildTasks\BMicrosoft.Common.targets
```
referenced from 
http://stackoverflow.com/questions/1682096/how-do-i-override-copylocal-private-setting-for-references-in-net-from-msbuil
