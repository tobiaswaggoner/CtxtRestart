# CtxtRestart
This is a tool to be used with Contextor in order to restart the application.

It creates a single standalone executable **CtxtRestart.exe** which can be placed alongside **CtxtRun.exe** 
(normally in C:\Program Files (x86)\Contextor\Interactive\) or any other location of course.

### Called without paramters
```
CtxtRestart
```
If called without parameters, CtxtRestart.exe tries to kill all running CtxtRun.exe immediately.

### Called with parameters
If parameters are passed, it will also first try to kill all running CtxtRun.exe and afterwards start the CtxtRun.exe 
passing the parameters it received. This means it can basically **replace** CtxtRun.exe in links for the users.
```
CtxtRestart -w "%APPDATA%" -z "http://website/myproject"
```

### Called from within a Contextor project (Suicide)
There are occasions, when a Contextor project needs to "commit suicide" and restart itself because it got stuck 
and cannot unwind. This can be done by calling CtxtRestart from within Contextor like this:

```
ctx.exec("C:\\Program Files (x86)\\Contextor\\Interactive\\CtxtRestart.exe -w \"%APPDATA\" -z \"http://website/myproject\"");
```

### Binary downloads
In case you do not want to compile the sources yourself, you can find the binary within the githib archive here:
https://github.com/tobiaswaggoner/CtxtRestart/blob/master/CtxtRestart/Binaries/CtxtRestart%201.0.0.zip
