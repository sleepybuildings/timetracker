# TimeTracker

Simple timetracking tool to track jobs and the time you spend on them.

Each job will the stored in a daily JSON file. 

## Options

| Argument       | What it does                       | 
|----------------|------------------------------------|
| start "name"   | Start a new job                    |
| end            | Stops the current job              |
| pop            | Stops and resumés the previous job |
| list           | Summary of jobs                    |
| list -d [date] | Summary of jobs from a specifc date|
| list prev      | Summary of jobs from yesterday     |

## Todo

- Extend the pop command (it acts like a switch atm...)
- Write more info

## Changelog

19 sept 2018

- Option to specify a date with the list command: `list -d 2018-01-01`
- Option to load yesterdays summary: `list prev`
- Files are now saved at `~/.config/timetracker`

## Disclaimer 

Use at your own risk

## Dependencies

- .NET Core 2.1
- CommandLineParser 2.3
- Newtonsoft.Json 11
- Colorful.Console 1.2.9

## .Net

Build release:

`dotnet publish -c Release`



