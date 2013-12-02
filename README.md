# mste-testat

Miniproject for passing the module Microsoft Technologies @ HSR - A car reservation system

Not intended for production use.

## Usage
Well, Visual Studio .. :)

But *first*: Run PowerShell as Administrator and enter:

```
> netsh http add urlacl url=http://+:7876/AutoReservationService/ user=YOURUSERNAME
```

By replacing `YOURUSERNAME` with, guess what, your own username on windows.

This *ahem* enables to run the Service without running Visual Studio as Administrator.