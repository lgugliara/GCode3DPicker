# GCode3DPicker
GCode file picker with 3D preview

## Based on
- WPF
- .NET 8.0
- Helix Toolkit
 (https://github.com/helix-toolkit/helix-toolkit)
- MahApps.Metro.IconPacks
  (https://github.com/MahApps/MahApps.Metro.IconPacks)

## Goals
- Display a file picker that updates whenever a file is created/modified/deleted/renamed within the current folder.
- Preview currently selected GCode program in a 3D controller.
- Design a WPF application through paradigms from front-end experience to improve maintenance.
- (TODO) Display the content of the program in a separate editor.

## Installation
- Clone the repo.
- Change `./GCode3D/Models/Picker/Picker.cs` DefaultPath with an existing path of yours.
- Run.
