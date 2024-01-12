# FileMatch v1.0
[![Build](https://github.com/ArtTorres/FileMatch/actions/workflows/build.yml/badge.svg)](https://github.com/ArtTorres/FileMatch/actions/workflows/build.yml)

Find duplicate files in directories. 

## Setup

1. Download on a location
2. Unzip
3. Execute

## Requirements
- .Net 6.0 or newer on Windows and Linux.

> [!WARNING]  
> This application could demand a significant amount of RAM under certain conditions like the analysis of millions of files and high incidence of duplicates. 

> [!TIP]
> Tested with 150,000 files, observed an average of 100 Mb of RAM consumption.

## Arguments
| Name | Alias | Description | Default |
| -------- | ------- | ------- | ------- |
| (no path) | | Evaluates the current directory |
| --path | -p | Evaluates the selected path | |
| --mode | -m | Sets the match mode | standard |
| --limit | -l | Limits the number of files evaluated | no-limits |
| --verbose | -v | Displays a resume of evaluated files and encountered errors. | |

## Match Modes
| Mode | Attributes |Description |
| -------- | ------- | ------- |
| basic | Filename | Compare files by name |
| standard | Filename, Size | Compare files by name and their sizes |
| advanced | Filename, Size, CreationDate | Compare files using the creation date [CT] as main attribute |
| strict | Filename, Size, ModifiedDate | Compare files using the last modification date [LT] as main attribute |

## Examples
``` shell
# - Windows Terminal

# Evaluates the current directory
C:\> fmatch.exe 

# Evaluates the path "D:\External"
C:\> fmatch.exe --path "D:\External"

# Evaluates the path "D:\External" using "strict" mode
C:\> fmatch.exe --path "D:\External" --mode "strict"

# Evaluates the path "D:\External" up to 5000 files
C:\> fmatch.exe --path "D:\External" --limit 5000

# Evaluates the path "D:\External" and displays the statistics report
C:\> fmatch.exe --path "D:\External" --verbose

# Export report to a file (Windows)
C:\> fmatch.exe > report.txt

# - Linux Terminal Supported

# Evaluates the path "D:\External"
$ fmatch --path "/home/External"

# Export report to a file (Linux)
$ fmatch | report.txt
```

### Example Output
``` shell
# Windows Terminal

C:\> fmatch.exe --path "D:\Data" --verbose
:: 000.testlog
[20.00 B]	D:\Data\DataStoreA\0\000.testlog
[20.00 B]	D:\Data\DataStoreB\v16\TestStore\0\000.testlog
:: testlog.manifest
[24.00 B]	D:\Data\DataStoreA\v16\TestStore\0\testlog.manifest
[24.00 B]	D:\Data\DataStoreB\v16\TestStore\0\testlog.manifest
:: NAudio.dll
[463.50 kB]	D:\Data\DataStoreA\packages\NAudio.1.8.2\lib\net35\NAudio.dll
[463.50 kB]	D:\Data\DataStoreB\packages\NAudio.1.8.2\lib\NAudio.dll
:: NAudio.XML
[956.03 kB]	D:\Data\DataStoreA\packages\NAudio.1.8.2\lib\net35\NAudio.XML
[956.03 kB]	D:\Data\DataStoreB\NAudio.1.8.2\lib\NAudio.XML
:: license.txt
[2.60 kB]	D:\Data\DataStoreA\packages\NAudio.1.8.2\license.txt
[2.60 kB]	D:\Data\DataStoreB\packages\NAudio.Wma.1.0.1\license.txt
.:
Files processed [572]
Matches [5]
Identified errors [0]
```

---
### Project References
- [Homepage](https://github.com/ArtTorres/FileMatch)
- [License](https://github.com/ArtTorres/FileMatch/blob/main/LICENSE)

### Issues or suggestions
- [Issues](https://github.com/ArtTorres/FileMatch/issues)
