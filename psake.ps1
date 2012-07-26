# Helper script for those who want to run
# psake without importing the module.
import-module .\Libs\psake\psake.psm1
try
{
	invoke-psake @args
} 
finally 
{
	remove-module psake
}