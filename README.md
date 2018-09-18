# TimeTracker

Simple timetracking tool to track jobs and the time you spend on them.

Each job will the stored in a daily JSON file. 

## Options

| Argument     | What is does                       | 
|--------------|------------------------------------|
| start <name> | Start a new job                    |
| end          | Stops the current job              |
| pop          | Stops and resumés the previous job |
| list         | Summary of jobs                    |

## Todo

- Change the storage location of the JSON files
- Extend the pop command (it acts like a switch atm...)
- Allow opening of previous logging files
- Write more info

## Disclaimer 

Use at your own risk

## Dependencies

- .NET Core 2.1
- CommandLineParser 2.3
- Newtonsoft.Json 11

## .Net

Build release:

`dotnet publish -c Release`



