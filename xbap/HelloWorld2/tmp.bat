
del App.exe.manifest
del App.xbap
del t\App.exe

copy App.exe t
copy App.exe.manifest.template App.exe.manifest
mage -cc

mage -Update App.exe.manifest -FromDirectory t -Name App -TrustLevel Internet -Version 1.0.0.0 -Processor msil
mage -Sign App.exe.manifest -CertFile Atlanta.pfx -Password Atlanta


mage -New Deployment -ToFile App.xbap -Name App -Install false -AppManifest App.exe.manifest
mage -Sign App.xbap -CertFile Atlanta.pfx -Password Atlanta

